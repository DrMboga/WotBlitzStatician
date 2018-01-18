namespace WotBlitzStatician.Data.DataAccessors
{
    using System;
    using System.Collections.Generic;
    using WotBlitzStatician.Model;

    public class AnalyseDataAccessor : IAnalyseDataAccessor
    {
        public List<AccountInfoStatistics> GetLastTwoSessions(long accountId)
        {
			throw new NotImplementedException();
		}

		public DateTime GetPrelastStatisticsUpdateDate(long accountId)
        {
			throw new NotImplementedException();
		}

		public List<AccountInfoStatistics> GetStatisticsForPeriod(long accountId, DateTime dateFrom)
        {
			throw new NotImplementedException();
		}

		public List<long> TankIdsForPeriod(long accountId, DateTime dateFrom)
	    {
			throw new NotImplementedException();
		}

		public DateTime GetPrelastTankBattleTimeBeforePeriod(long accountId, long tankId, DateTime periodDateFrom)
	    {
			throw new NotImplementedException();
		}

		public List<AccountTankStatistics> GeTankInfoForPeriod(long accountId, long tankId, DateTime dateFrom)
	    {
			throw new NotImplementedException();
		}
	}
}