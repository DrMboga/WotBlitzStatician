namespace WotBlitzStatician.Data.DataAccessors
{
	using System.Collections.Generic;
	using WotBlitzStatician.Model;

	public interface ITanksStatisticsDataAccessor
	{
		AccountTankStatistics GetAllTanksByAccount(long accountId);

		void SaveTanksStatistic(List<AccountTankStatistics> taksStat);

		void SaveAccountTankAchievements(List<AccountInfoTankAchievment> accountInfoTankAchievments);
	}
}