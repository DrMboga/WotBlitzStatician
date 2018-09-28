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
	[Image] = CASE WHEN ao.[Image] IS NOT NULL THEN ao.[Image] ELSE a.[Image] END,
	IsAchievementOption = CONVERT(BIT, CASE WHEN ao.[Image] IS NOT NULL THEN 1 ELSE 0 END)
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

			var accountIdParameter = new SqlParameter("@accountId", accountId);
			return await _dbContext.Query<AchievementDto>()
				.FromSql(achievementsSql, accountIdParameter)
				.ToListAsync();
		}
	}
}
