namespace WotBlitzStatician.Logic
{
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
            // ToDo: Implement
            return null;
        }
    }
}
