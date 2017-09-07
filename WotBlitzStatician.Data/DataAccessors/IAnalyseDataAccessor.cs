namespace WotBlitzStatician.Data.DataAccessors
{
    using System;
    using System.Collections.Generic;
    using WotBlitzStatician.Model;

	public interface IAnalyseDataAccessor
	{
        DateTime GetPrelastStatisticsUpdateDate(long accountId);
        List<AccountInfoStatistics> GetStatisticsForPeriod(long accountId, DateTime dateFrom);
	}
}