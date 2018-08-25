using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WotBlitzStatician.Data.Mappers;
using WotBlitzStatician.Model;
using WotBlitzStatician.Model.Common;
using WotBlitzStatician.Model.Dto;

namespace WotBlitzStatician.Data.DataAccessors
{
	public class AccountsTankInfoDataAccessor : IAccountsTankInfoDataAccessor
	{
		private readonly BlitzStaticianDbContext _dbContext;

		public AccountsTankInfoDataAccessor(BlitzStaticianDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<(long tankId, string tankInfo)>> GetStringTankInfos(long[] tankIds)
		{
			var tanksInfo = await _dbContext.VehicleEncyclopedia.AsNoTracking()
				.Where(v => tankIds.Contains(v.TankId))
				.Join(_dbContext.DictionaryNation, v => v.Nation, n => n.NationId,
						(v, n) => new { Vehicle = v, n.NationName })
				.Join(_dbContext.DictionaryVehicleType, v => v.Vehicle.Type, t => t.VehicleTypeId,
						(v, t) => new { v.Vehicle, v.NationName, t.VehicleTypeName })
				.Select(j => new
				{
					j.Vehicle.TankId,
					TankInfo = $"{j.Vehicle.Name} ({((int)j.Vehicle.Tier).ToRomanNumeral()}; {j.NationName}; {j.VehicleTypeName})"
				})
				.ToListAsync();

			var result = new List<(long tankId, string tankInfo)>();
			tanksInfo.ForEach(t => result.Add((tankId: t.TankId, tankInfo: t.TankInfo)));
			return result;
		}

		public async Task<List<AccountMasteryInfoDto>> GetAccountMasteryInfo(long accountId)
		{
			int allTanksCount = await _dbContext.PresentAccountTanks.AsNoTracking()
										.Where(t => t.AccountId == accountId)
										.CountAsync();
			string accountMasteryInfosSql =
				@"
SELECT 
	ats.MarkOfMastery, 
	COUNT(ats.AccountTankStatisticId) AS TanksCount, 
	md.[Image], 
	md.[Description]
FROM wotb.PresentAccountTanks AS pat
	INNER JOIN wotb.AccountTankStatistics AS ats ON ats.AccountTankStatisticId = pat.AccountTankStatisticId
	INNER JOIN (SELECT CONVERT(INT, LEFT(RIGHT(ao.[Image],5), 1)) AS MarkOfMastery, ao.[Image], ao.[Description]
				FROM wotb.AchievementOption AS ao
				WHERE ao.AchievementId = 'markOfMastery') md ON md.MarkOfMastery = ats.MarkOfMastery
WHERE pat.AccountId = @accountId
GROUP BY ats.MarkOfMastery, md.[Image], md.[Description]
";
			var accountIdParameter = new SqlParameter("@accountId", accountId);
			var accountMasteryInfo = await _dbContext.Query<AccountMasteryInfoDto>()
				.FromSql(accountMasteryInfosSql, accountIdParameter)
				.ToListAsync();
			accountMasteryInfo.ForEach(m => m.AllTanksCount = allTanksCount);

			return accountMasteryInfo;
		}

		public IQueryable<AccountTankInfoDto> GetTanksInfoQuery(long accountId)
		{
			var masteryImages = GetMasteryImages();
			var mapper = new AccountTanksInfoDtoMapper(masteryImages);

			return mapper.ProjectTo(
			  _dbContext.PresentAccountTanks.AsNoTracking()
			 .Where(pt => pt.AccountId == accountId)
			 .Join(_dbContext.AccountTankStatistics.AsNoTracking(), pt => pt.AccountTankStatisticId,
			   ts => ts.AccountTankStatisticId,
			   (pt, st) => st)
			 .Join(_dbContext.VehicleEncyclopedia.AsNoTracking(), st => st.TankId, v => v.TankId,
			   (st, v) => new { AccountTankStatistic = st, VehicleInfo = v })
			 .Select(j => new AccountTanksStatisticsTuple { Tank = j.AccountTankStatistic, Vehicle = j.VehicleInfo })
			 );

		}

		private Dictionary<MarkOfMastery, string> GetMasteryImages()
		{
			var masteryImages = _dbContext.AchievementOption.AsNoTracking()
			  .Where(a => a.Achievement.AchievementId == "markOfMastery")
			  .Select(o => o.Image).ToList();
			var imagesDic = new Dictionary<MarkOfMastery, string>();

			foreach (var mastery in Enum.GetValues(typeof(MarkOfMastery)).Cast<MarkOfMastery>())
			{
				imagesDic[mastery] = masteryImages.FirstOrDefault(i => i.ToLower().EndsWith($"{(int)mastery}.png"));
			}

			return imagesDic;
		}

	}
}
