namespace WotBlitzStatician.Data.DataAccessors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using log4net;
    using WotBlitzStatician.Model;

    public class AnalyseDataAccessor : IAnalyseDataAccessor
    {
		private static readonly ILog _log = LogManager.GetLogger(typeof(AnalyseDataAccessor));
		private readonly IBlitzStaticianDataContextFactory _blitzStaticianDataContextFactory;

        public AnalyseDataAccessor(IBlitzStaticianDataContextFactory blitzStaticianDataContextFactory)
        {
            _blitzStaticianDataContextFactory = blitzStaticianDataContextFactory;
        }

        public List<AccountInfoStatistics> GetLastTwoSessions(long accountId)
        {
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
            {
                return context.AccountInfoStatistics
                            .Where(s => s.AccountId == accountId)
                            .OrderByDescending(s => s.UpdatedAt)
                              .Take(2).ToList();
            }
		}

        public DateTime GetPrelastStatisticsUpdateDate(long accountId)
        {
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
                // Take two last sessions and choose the minimum Updated date
                return context.AccountInfoStatistics
                            .Where(s => s.AccountId == accountId)
                            .OrderByDescending(s => s.UpdatedAt)
                              .Select(s => s.UpdatedAt)
                              .Take(2).ToList()
                              .Min();
			}

		}

        public List<AccountInfoStatistics> GetStatisticsForPeriod(long accountId, DateTime dateFrom)
        {
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
                return context.AccountInfoStatistics
                              .Where(s => s.AccountId == accountId & s.UpdatedAt >= dateFrom)
                              .ToList();
			}
		}
    }
}