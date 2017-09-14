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

	    public List<long> TankIdsForPeriod(long accountId, DateTime dateFrom)
	    {
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				return context.AccountTankStatistics
					.Where(t => t.AccountId == accountId && t.LastBattleTime >= dateFrom)
					.Select(t => t.TankId)
					.Distinct()
					.ToList();
			}
		}

	    public DateTime GetPrelastTankBattleTimeBeforePeriod(long accountId, long tankId, DateTime periodDateFrom)
	    {
		    using (var context = _blitzStaticianDataContextFactory.CreateContext())
		    {
			    return context.AccountTankStatistics
				    .Where(t => t.AccountId == accountId & t.TankId == tankId & t.LastBattleTime < periodDateFrom)
				    .OrderByDescending(o => o.LastBattleTime)
				    .Select(s => s.LastBattleTime)
				    .FirstOrDefault();
		    }
	    }

	    public List<AccountTankStatistics> GeTankInfoForPeriod(long accountId, long tankId, DateTime dateFrom)
	    {
		    var tanks = new List<AccountTankStatistics>();
		    using (var context = _blitzStaticianDataContextFactory.CreateContext())
		    {
			    var tanksJoined = context.AccountTankStatistics
				    .Join(context.VehicleEncyclopedia, t => t.TankId, v => v.TankId, (t, v) => new {TankStat = t, VehicleInfo = v})
					.Where(t => t.TankStat.AccountId == accountId & t.TankStat.TankId == tankId & t.TankStat.LastBattleTime >= dateFrom)
				    .ToList();
			    tanksJoined.ForEach(t =>
			    {
				    t.TankStat.VehicleInfo = t.VehicleInfo;
					tanks.Add(t.TankStat);
			    });
			}
		    return tanks;
	    }
    }
}