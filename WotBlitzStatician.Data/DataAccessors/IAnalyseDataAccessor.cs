namespace WotBlitzStatician.Data.DataAccessors
{
    using System;
    using System.Collections.Generic;
    using WotBlitzStatician.Model;

	public interface IAnalyseDataAccessor
	{
        DateTime GetPrelastStatisticsUpdateDate(long accountId);
        List<AccountInfoStatistics> GetStatisticsForPeriod(long accountId, DateTime dateFrom);
		List<long> TankIdsForPeriod(long accountId, DateTime dateFrom);
		DateTime GetPrelastTankBattleTimeBeforePeriod(long accountId, long tankId, DateTime periodDateFrom);
		List<AccountTankStatistics> GeTankInfoForPeriod(long accountId, long tankId, DateTime dateFrom);
	}
}