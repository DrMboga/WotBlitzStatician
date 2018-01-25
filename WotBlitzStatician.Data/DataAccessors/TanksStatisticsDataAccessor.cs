namespace WotBlitzStatician.Data.DataAccessors
{
	using System;
	using System.Collections.Generic;
	using WotBlitzStatician.Model;

	public class TanksStatisticsDataAccessor : ITanksStatisticsDataAccessor
	{
		public AccountTankStatistics GetAllTanksByAccount(long accountId)
		{
			throw new System.NotImplementedException();
		}

		public void SaveTanksStatistic(List<AccountTankStatistics> taksStat)
		{
			throw new NotImplementedException();
		}

		public void SaveAccountTankAchievements(List<AccountInfoTankAchievement> accountInfoTankAchievments)
		{
			throw new NotImplementedException();
		}
	}
}