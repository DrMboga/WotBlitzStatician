namespace WotBlitzStatician.Logic
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using WotBlitzStatician.Model;
	 
	public interface IBlitzStaticianLogic
	{
		Task LoadStatisticsFromWgAsync(long accountId);

        Task LoadStaticDataAndSaveToDb();

		AccountInfo GetLastLoggedAccount();

		void SetLastLoggedAccount(long accountId);

		Task<AccountInfo> GetAccount(string nick);

		Task<List<AccountInfo>> FindAccounts(string nick);

		Task<AccountInfo> GetAccountStatistics(long accountId);

		AccountInfoPrivate GetAccountPrivateStatistics(long accountId);

		AccountTankStatistics GetAllTanksByAccount(long accountId);
	}
}
