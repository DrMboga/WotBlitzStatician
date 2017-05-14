using System.Threading.Tasks;
using WotBlitzStaticitian.Model;

namespace WotBlitzStatician.Logic
{
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
