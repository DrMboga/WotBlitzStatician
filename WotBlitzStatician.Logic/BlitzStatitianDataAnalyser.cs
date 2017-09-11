namespace WotBlitzStatician.Logic
{
    using System;
    using System.Linq;
    using WotBlitzStatician.Data.DataAccessors;
    using WotBlitzStatician.Logic.Dto;
    using WotBlitzStatician.Model;

    public class BlitzStatitianDataAnalyser : IBlitzStatitianDataAnalyser
    {
        private readonly IAnalyseDataAccessor _analyseDataAccessor;

        public BlitzStatitianDataAnalyser(IAnalyseDataAccessor analyseDataAccessor)
        {
            _analyseDataAccessor = analyseDataAccessor;
        }

        public BlitzAccountInfoStatisticsDelta GetAccountLastSessionDelta(long accountId)
        {
            var preLastDate = _analyseDataAccessor.GetPrelastStatisticsUpdateDate(accountId);
            var lastTwoSessions = _analyseDataAccessor.GetStatisticsForPeriod(accountId, preLastDate);
            var minSession = lastTwoSessions.FirstOrDefault(s => s.UpdatedAt == lastTwoSessions.Min(m => m.UpdatedAt));
			var maxSession = lastTwoSessions.FirstOrDefault(s => s.UpdatedAt == lastTwoSessions.Max(m => m.UpdatedAt));
            return GetDelta(minSession, maxSession);
		}

        private BlitzAccountInfoStatisticsDelta GetDelta(AccountInfoStatistics min, AccountInfoStatistics max)
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

			return delta;
        }
    }
}
