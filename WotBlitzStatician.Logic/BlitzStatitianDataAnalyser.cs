namespace WotBlitzStatician.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WotBlitzStatician.Data.DataAccessors;
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
            return GetAccountDelta(minSession, maxSession);
		}

	    public List<long> GetTankIdsByLastSession(long accountId, DateTime dateFrom)
	    {
		    return _analyseDataAccessor.TankIdsForPeriod(accountId, dateFrom);
	    }

	    public BlitzTankInfoDelta GeTankInfoDelta(long accountId, long tankId, DateTime dateFrom)
	    {
		    var prelastBattleDate = _analyseDataAccessor.GetPrelastTankBattleTimeBeforePeriod(accountId, tankId, dateFrom);
		    var tanksStatForPeriod = _analyseDataAccessor.GeTankInfoForPeriod(accountId, tankId, prelastBattleDate);
		    var minTankSession = tanksStatForPeriod.FirstOrDefault(t => t.LastBattleTime == tanksStatForPeriod.Min(m => m.LastBattleTime));
		    var maxTankSession = tanksStatForPeriod.FirstOrDefault(t => t.LastBattleTime == tanksStatForPeriod.Max(m => m.LastBattleTime));
		    return GetTankDelta(minTankSession, maxTankSession);
	    }

	    private BlitzAccountInfoStatisticsDelta GetAccountDelta(AccountInfoStatistics min, AccountInfoStatistics max)
        {
            var delta = new BlitzAccountInfoStatisticsDelta { AccountId = min.AccountId };
            delta.Battles = new ValueDelta<long, long>(max.Battles, min.Battles, max.Battles - min.Battles);
            delta.UpdatedAt = new ValueDelta<DateTime, TimeSpan>(max.UpdatedAt, min.UpdatedAt, max.UpdatedAt - min.UpdatedAt);
            delta.Wins = new ValueDelta<long, long>(max.Wins, min.Wins, max.Wins - min.Wins);
            decimal winrateMax = Convert.ToDecimal(max.Wins) * 100 / max.Battles; 
            decimal winrateMin = Convert.ToDecimal(min.Wins) * 100 / min.Battles;
            delta.Winrate = new ValueDelta<decimal, decimal>(winrateMax, winrateMin, Math.Abs(winrateMax - winrateMin), winrateMin > winrateMax);
            decimal avgDapamgeMax = Convert.ToDecimal(max.DamageDealt) / max.Battles;
			decimal avgDapamgeMin = Convert.ToDecimal(min.DamageDealt) / min.Battles;
            delta.AvgDamage = new ValueDelta<decimal, decimal>(avgDapamgeMax, avgDapamgeMin, Math.Abs(avgDapamgeMax - avgDapamgeMin), avgDapamgeMin > avgDapamgeMax);
            decimal avgXpMax = Convert.ToDecimal(max.Xp) / max.Battles;
            decimal avgXpMin = Convert.ToDecimal(min.Xp) / min.Battles;
            delta.AvgXp = new ValueDelta<decimal, decimal>(avgXpMax, avgXpMin, Math.Abs(avgXpMax - avgXpMin), avgXpMin > avgXpMax);
            delta.Wn7 = new ValueDelta<decimal, decimal>((decimal)max.Wn7, (decimal)min.Wn7, (decimal)Math.Abs(max.Wn7 - min.Wn7), min.Wn7 > max.Wn7);
            delta.AvgTier = new ValueDelta<decimal, decimal>((decimal)max.AvgTier, (decimal)min.AvgTier, (decimal)Math.Abs(max.AvgTier - min.AvgTier), min.AvgTier > max.AvgTier);
            delta.Effectivity = new ValueDelta<decimal, decimal>((decimal)max.Effectivity, (decimal)min.Effectivity, (decimal)Math.Abs(max.Effectivity - min.Effectivity), min.Effectivity > max.Effectivity);

	        delta.IntervalWinrate = 100 * Convert.ToDecimal(delta.Wins.Delta) / delta.Battles.Delta;
	        delta.IntervalAvgDamage = Math.Abs(Convert.ToDecimal(max.DamageDealt) - Convert.ToDecimal(min.DamageDealt)) / delta.Battles.Delta;
	        delta.IntervalAvgXp = Math.Abs(Convert.ToDecimal(max.Xp) - Convert.ToDecimal(min.Xp)) / delta.Battles.Delta;

			return delta;
        }

	    private BlitzTankInfoDelta GetTankDelta(AccountTankStatistics min, AccountTankStatistics max)
	    {
		    if (min == null || max == null)
			    return null;
		    var delta = new BlitzTankInfoDelta
		    {
			    AccountId = max.AccountId,
			    TankId = max.TankId,
			    Name = max.VehicleInfo.Name,
			    Tier = max.VehicleInfo.Tier,
			    Nation = max.VehicleInfo.Nation,
			    Type = max.VehicleInfo.Type,
			    IsPremium = max.VehicleInfo.IsPremium,
			    PreviewImageUrl = max.VehicleInfo.PreviewImageUrl,
			    NormalImageUrl = max.VehicleInfo.NormalImageUrl,
			    MarkOfMastery = max.MarkOfMastery,
				MarkOfMasteryImageUrl = _blitzStaticianDictionary.GetMarkOfMasteryImageUrl(max.MarkOfMastery)
		    };
			delta.Battles = new ValueDelta<long, long>(max.Battles, min.Battles, max.Battles - min.Battles);
		    delta.LastBattle = new ValueDelta<DateTime, TimeSpan>(max.LastBattleTime, min.LastBattleTime, max.LastBattleTime - min.LastBattleTime);
		    delta.Wins = new ValueDelta<long, long>(max.Wins, min.Wins, max.Wins - min.Wins);
		    decimal winrateMax = Convert.ToDecimal(max.Wins) * 100 / max.Battles;
		    decimal winrateMin = Convert.ToDecimal(min.Wins) * 100 / min.Battles;
		    delta.Winrate = new ValueDelta<decimal, decimal>(winrateMax, winrateMin, Math.Abs(winrateMax - winrateMin), winrateMin > winrateMax);
		    decimal avgDapamgeMax = Convert.ToDecimal(max.DamageDealt) / max.Battles;
		    decimal avgDapamgeMin = Convert.ToDecimal(min.DamageDealt) / min.Battles;
		    delta.AvgDamage = new ValueDelta<decimal, decimal>(avgDapamgeMax, avgDapamgeMin, Math.Abs(avgDapamgeMax - avgDapamgeMin), avgDapamgeMin > avgDapamgeMax);
		    decimal avgXpMax = Convert.ToDecimal(max.Xp) / max.Battles;
		    decimal avgXpMin = Convert.ToDecimal(min.Xp) / min.Battles;
		    delta.AvgXp = new ValueDelta<decimal, decimal>(avgXpMax, avgXpMin, Math.Abs(avgXpMax - avgXpMin), avgXpMin > avgXpMax);
		    delta.Wn7 = new ValueDelta<decimal, decimal>((decimal)max.Wn7, (decimal)min.Wn7, (decimal)Math.Abs(max.Wn7 - min.Wn7), min.Wn7 > max.Wn7);
		    delta.Effectivity = new ValueDelta<decimal, decimal>((decimal)max.Effectivity, (decimal)min.Effectivity, (decimal)Math.Abs(max.Effectivity - min.Effectivity), min.Effectivity > max.Effectivity);

		    delta.IntervalWinrate = 100 * Convert.ToDecimal(delta.Wins.Delta) / delta.Battles.Delta;
		    delta.IntervalAvgDamage = Math.Abs(Convert.ToDecimal(max.DamageDealt) - Convert.ToDecimal(min.DamageDealt)) / delta.Battles.Delta;
		    delta.IntervalAvgXp = Math.Abs(Convert.ToDecimal(max.Xp) - Convert.ToDecimal(min.Xp)) / delta.Battles.Delta;

			return delta;
	    }
	}
}
