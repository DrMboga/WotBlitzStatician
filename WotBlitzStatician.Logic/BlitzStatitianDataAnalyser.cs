namespace WotBlitzStatician.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WotBlitzStatician.Data.DataAccessors;
    using WotBlitzStatician.Logic.Calculations;
    using WotBlitzStatician.Logic.Dto;
    using WotBlitzStatician.Model;

    public class BlitzStatitianDataAnalyser : IBlitzStatitianDataAnalyser
    {
        private readonly IAnalyseDataAccessor _analyseDataAccessor;
	    private readonly IBlitzStaticianDictionary _blitzStaticianDictionary;

        public BlitzStatitianDataAnalyser(IAnalyseDataAccessor analyseDataAccessor, IBlitzStaticianDictionary blitzStaticianDictionary)
        {
	        _analyseDataAccessor = analyseDataAccessor;
	        _blitzStaticianDictionary = blitzStaticianDictionary;
        }

	    public DateTime GetPrelastStatisticsUpdateDate(long accountId)
	    {
		    return _analyseDataAccessor.GetPrelastStatisticsUpdateDate(accountId);
		}

	    public BlitzAccountInfoStatisticsDelta GetAccountDelta(long accountId, DateTime dateFrom)
        {
            var lastTwoSessions = _analyseDataAccessor.GetStatisticsForPeriod(accountId, dateFrom);
            var minSession = lastTwoSessions.FirstOrDefault(s => s.UpdatedAt == lastTwoSessions.Min(m => m.UpdatedAt));
			var maxSession = lastTwoSessions.FirstOrDefault(s => s.UpdatedAt == lastTwoSessions.Max(m => m.UpdatedAt));
            return GetAccountDelta(minSession, maxSession, dateFrom);
		}


	    private BlitzAccountInfoStatisticsDelta GetAccountDelta(AccountInfoStatistics min, AccountInfoStatistics max, DateTime dateFrom)
        {
            var delta = new BlitzAccountInfoStatisticsDelta { AccountId = min.AccountId };
	        var tankIdsForPeriod = _analyseDataAccessor.TankIdsForPeriod(min.AccountId, dateFrom);
			delta.TanksForPeriod = new List<BlitzTankInfoDelta>();
	        foreach (var tankId in tankIdsForPeriod)
	        {
				delta.TanksForPeriod.Add(GeTankInfoDelta(min.AccountId, tankId, dateFrom));
			}

	        decimal intervalTier = 0m;
			var battleLifeTimeDelta = new TimeSpan(0);
	        delta.TanksForPeriod.ForEach(t =>
	        {
		        intervalTier += t.BlitzTankInfo.Tier;
		        battleLifeTimeDelta += t.Statistics.BattleLifeTime.Delta;
	        });
	        intervalTier /= delta.TanksForPeriod.Count;

			delta.AvgTier = new ValueDelta<decimal, decimal>((decimal)max.AvgTier, (decimal)min.AvgTier, (decimal)Math.Abs(max.AvgTier - min.AvgTier), intervalTier, min.AvgTier > max.AvgTier);

			delta.FillStatistics(min, max, intervalTier);
			// ToDo: temporary solution
			delta.Statistics.BattleLifeTime = new ValueDelta<TimeSpan, TimeSpan>(new TimeSpan(0), new TimeSpan(0), battleLifeTimeDelta, battleLifeTimeDelta);

			return delta;
        }

	    private BlitzTankInfoDelta GeTankInfoDelta(long accountId, long tankId, DateTime dateFrom)
	    {
		    var prelastBattleDate = _analyseDataAccessor.GetPrelastTankBattleTimeBeforePeriod(accountId, tankId, dateFrom);
		    var tanksStatForPeriod = _analyseDataAccessor.GeTankInfoForPeriod(accountId, tankId, prelastBattleDate);
		    var minTankSession = tanksStatForPeriod.FirstOrDefault(t => t.LastBattleTime == tanksStatForPeriod.Min(m => m.LastBattleTime));
		    var maxTankSession = tanksStatForPeriod.FirstOrDefault(t => t.LastBattleTime == tanksStatForPeriod.Max(m => m.LastBattleTime));
		    return GetTankDelta(minTankSession, maxTankSession);
	    }


	    private BlitzTankInfoDelta GetTankDelta(AccountTankStatistics min, AccountTankStatistics max)
	    {
		    if (min == null || max == null)
			    return null;
		    var delta = new BlitzTankInfoDelta
		    {
                BlitzTankInfo = MapFromTankStatistics(max)
		    };
		    try
		    {
			    delta.FillStatistics(min, max);
		    }
		    catch (Exception e)
		    {
			    throw new ArgumentException($"Error int fill statistics, tankId {max.TankId} from '{min.LastBattleTime}' to '{max.LastBattleTime}'", e);
		    }
			return delta;
	    }

        private BlitzTankInfoDto MapFromTankStatistics(AccountTankStatistics tankStat)
        {
            return new BlitzTankInfoDto
            {
                AccountId = tankStat.AccountId,
                TankId = tankStat.TankId,
                Name = tankStat.VehicleInfo.Name,
                Tier = tankStat.VehicleInfo.Tier,
                Nation = tankStat.VehicleInfo.Nation,
                Type = tankStat.VehicleInfo.Type,
                IsPremium = tankStat.VehicleInfo.IsPremium,
                PreviewImageUrl = tankStat.VehicleInfo.PreviewImageUrl,
                NormalImageUrl = tankStat.VehicleInfo.NormalImageUrl,
                MarkOfMastery = tankStat.MarkOfMastery,
				// ToDo: MarksInfoHelper
                //MarkOfMasteryImageUrl = _blitzStaticianDictionary.GetMarkOfMasteryImageUrl(tankStat.MarkOfMastery)
            };
        }
	}
}
