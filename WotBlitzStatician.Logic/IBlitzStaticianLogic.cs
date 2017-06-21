namespace WotBlitzStatician.Logic
{
	using System.Threading.Tasks;
	using WotBlitzStatician.Model;
	 
	public interface IBlitzStaticianLogic
	{
		Task LoadStatisticsFromWgAsync(long accountId);

		AccountInfo GetLastLoggedAccount();

		Task<AccountInfo> GetAccount(string nick);

		AccountInfoStatistics GetAccountStatistics(long accountId);

		AccountInfoPrivate GetAccountPrivateStatistics(long accountId);

		AccountTankStatistics GetAllTanksByAccount(long accountId);
	}
}
