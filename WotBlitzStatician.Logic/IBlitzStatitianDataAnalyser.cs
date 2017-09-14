namespace WotBlitzStatician.Logic
{
	using System;
	using System.Collections.Generic;
	using WotBlitzStatician.Logic.Dto;

    public interface IBlitzStatitianDataAnalyser
    {
	    DateTime GetPrelastStatisticsUpdateDate(long accountId);
		BlitzAccountInfoStatisticsDelta GetAccountDelta(long accountId, DateTime dateFrom);
	    List<long> GetTankIdsByLastSession(long accountId, DateTime dateFrom);
	    BlitzTankInfoDelta GeTankInfoDelta(long accountId, long tankId, DateTime dateFrom);
    }
}
