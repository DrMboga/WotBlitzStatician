using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WotBlitzStatician.Model.Dto;

namespace WotBlitzStatician.Data.DataAccessors
{
	public class AchievementsDataAccessor : IAchievementsDataAccessor
	{
		private readonly BlitzStaticianDbContext _dbContext;

		public AchievementsDataAccessor(BlitzStaticianDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<AchievementDto>> GetAccountAchievements(long accountId)
		{
			string achievementsSql =
@"SELECT 
	aa.AchievementId,
	s.Section,
	s.SectionName,
	a.[Order],
	a.[Name],
	[Description] = a.[Description] + CASE WHEN a.Condition IS NULL THEN '' ELSE CHAR(13) + a.Condition END,
	aa.[Count],
	[Image] = CASE WHEN ao.[Image] IS NOT NULL THEN ao.[Image] ELSE a.[Image] END
FROM wotb.AccountInfoAchievement aa
	INNER JOIN wotb.Achievement a ON aa.AchievementId = a.AchievementId
	INNER JOIN wotb.AchievementSection s ON a.Section = s.Section
	LEFT JOIN (SELECT wotb.AchievementOption.AchievementId,
						wotb.AchievementOption.[Image],
						CONVERT(INT, LEFT(RIGHT([Image],5), 1)) AS Grade
				FROM wotb.AchievementOption) ao ON ao.AchievementId = aa.AchievementId AND aa.[Count] = ao.Grade
WHERE aa.AccountId = @accountId and aa.TankId IS NULL AND aa.IsMaxSeries = 0
ORDER BY s.[Order], a.[Order] 
";
			/*
			var accountIdParameter = new SqlParameter("@accountId", accountId);
			
			return await _dbContext.Query<AchievementDto>()
				.FromSql(achievementsSql, accountIdParameter)
				.ToListAsync();
			*/	
			
			// ToDo: AccountInfoAchievement is a base class and query includes both discrimitators. That's why here is this hack with shadow property
			var achievements = await _dbContext.AccountInfoAchievement.AsNoTracking()
				.Where(a => EF.Property<string>(a, "Discriminator") == "AccountInfoAchievement" && a.AccountId == accountId && a.IsMaxSeries == false)
				.Join(_dbContext.Achievement, aa => aa.AchievementId, a => a.AchievementId,
					(aa, a) => new { AccountInfoAchievement = aa, Achievement = a })
				.Join(_dbContext.AchievementSection, j1 => j1.Achievement.Section, s => s.Section,
					(j1, s) => new { j1.AccountInfoAchievement, j1.Achievement, AchievementSection = s })
				.Select(j2 => new AchievementDto
				{
					AchievementId = j2.AccountInfoAchievement.AchievementId,
					Section = j2.AchievementSection.Section,
					SectionName = j2.AchievementSection.SectionName,
					Order = j2.Achievement.Order,
					Name = j2.Achievement.Name,
					Description = $"{j2.Achievement.Description}{(j2.Achievement.Condition != null ? Environment.NewLine + j2.Achievement.Condition : string.Empty)}",
					Count = j2.AccountInfoAchievement.Count,
					Image = j2.Achievement.Image
				})
				.ToListAsync();

			// ToDo: Too many database queries. Think about some kind of views or dictionary caches
			var ao = await _dbContext.AchievementOption.AsNoTracking()
				.Select(o => new { o.Achievement.AchievementId, o.Image })
				.ToListAsync();

			achievements.ForEach(a =>
			{
				if (ao.Exists(o => o.AchievementId == a.AchievementId))
				{
					// Count of achievement in this case means the level of achivement option.
					// And there is no other way to know the level of achievement option, only find the level number in image name
					a.Image = ao.FirstOrDefault(o => o.AchievementId == a.AchievementId && o.Image.Contains($"{a.Count}"))?.Image;
				}
			});

			return achievements;
		}
	}
}
