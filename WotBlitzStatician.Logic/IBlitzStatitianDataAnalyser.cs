namespace WotBlitzStatician.Logic
{
	using System;
	using WotBlitzStatician.Logic.Dto;

    public interface IBlitzStatitianDataAnalyser
    {
	    DateTime GetPrelastStatisticsUpdateDate(long accountId);
		BlitzAccountInfoStatisticsDelta GetAccountDelta(long accountId, DateTime dateFrom);
	}
}
