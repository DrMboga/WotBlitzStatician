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

namespace WotBlitzStatician.Data.DataAccessors.Impl
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

			var accountMasteryInfo = await _dbContext.PresentAccountTanks.AsNoTracking()
				.Where(p => p.AccountId == accountId)
				.Join(_dbContext.AccountTankStatistics.AsNoTracking(), p => p.AccountTankStatisticId,
					s => s.AccountTankStatisticId, (p, s) => s)
				.GroupBy(s => s.MarkOfMastery)
				.Select(g => new AccountMasteryInfoDto {MarkOfMastery = g.Key, TanksCount = g.Count(), AllTanksCount = allTanksCount})
				.ToListAsync();

			return accountMasteryInfo;
		}

        public async Task<AccountTankByAchievementDto[]> GetAllTanksByAchievement(long accountId, string achievementId)
        {
			var mapper = new AccountTanksInfoDtoMapper();

			var tanks = await _dbContext.PresentAccountTanks.AsNoTracking()
				.Join(_dbContext.AccountTankStatistics.AsNoTracking(), pt => pt.AccountTankStatisticId,
					ts => ts.AccountTankStatisticId, (pt, st) => st)
				.Join(_dbContext.VehicleEncyclopedia.AsNoTracking(), st => st.TankId, v => v.TankId,
					(st, v) => new { AccountTankStatistic = st, VehicleInfo = v })
				.Join(_dbContext.AccountInfoTankAchievement.AsNoTracking(), 
					j => new {j.AccountTankStatistic.AccountId, j.AccountTankStatistic.TankId},
					a => new {a.AccountId, a.TankId},
					(l, r) => new {l.AccountTankStatistic, l.VehicleInfo, r.AchievementId, r.Count})
				.Where(j => j.AccountTankStatistic.AccountId == accountId && j.AchievementId == achievementId)
				.Select(j => new { Tuple = new AccountTanksStatisticsTuple{Tank = j.AccountTankStatistic, Vehicle = j.VehicleInfo}, AchievementsCount = j.Count })
				.ToListAsync();
			
			var response = new AccountTankByAchievementDto[tanks.Count];

			for (int i = 0; i < tanks.Count; i++)
			{
				response[i] = new AccountTankByAchievementDto{
					TankInfo = mapper.Map(tanks[i].Tuple),
					AchievementId = achievementId,
					AchievementsCount = tanks[i].AchievementsCount
				};
			}
			return response;
        }

		public IQueryable<AccountTankInfoDto> GetTanksInfoQuery(long accountId)
		{
			var mapper = new AccountTanksInfoDtoMapper();

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

    public async Task<List<AccountTanksInfoAggregationDto>> GetAggregatedAccountTanksInfo(long accountId)
    {
      return await _dbContext.PresentAccountTanks.AsNoTracking()
              .Join(_dbContext.AccountTankStatistics.AsNoTracking(), p => p.AccountTankStatisticId, s => s.AccountTankStatisticId,
                      (p, s) => new { p.AccountId, s.TankId, Statistics = s })
              .Join(_dbContext.VehicleEncyclopedia.AsNoTracking(), j => j.TankId, v => v.TankId,
                  (j, v) => new { j.AccountId, j.Statistics, Vehicle = v })
              .Join(_dbContext.DictionaryNation.AsNoTracking(), j1 => j1.Vehicle.Nation, n => n.NationId,
                  (j1, n) => new { j1.AccountId, j1.Statistics, j1.Vehicle, n.NationName })
              .Join(_dbContext.DictionaryVehicleType.AsNoTracking(), j2 => j2.Vehicle.Type, t => t.VehicleTypeId,
                  (j2, t) => new {j2.AccountId, j2.Statistics, j2.Vehicle, j2.NationName, t.VehicleTypeName })
							.Where(j3 => j3.AccountId == accountId)
							.Select(j3 => new AccountTanksInfoAggregationDto
							{
								InGarage = j3.Statistics.InGarage,
								Battles = j3.Statistics.Battles,
								Wins = j3.Statistics.Wins,
								DamageDealt = j3.Statistics.DamageDealt,
								MarkOfMastery = j3.Statistics.MarkOfMastery,
								Wn7 = j3.Statistics.Wn7,
								Tier = j3.Vehicle.Tier,
								Nation = j3.Vehicle.Nation,
								NationName = j3.NationName,
								Type = j3.Vehicle.Type,
								TypeName = j3.VehicleTypeName,
								IsPremium = j3.Vehicle.IsPremium
							})
              .ToListAsync();


    }
  }
}
