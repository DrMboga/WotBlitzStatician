using System.Collections.Generic;
using WotBlitzStaticitian.Model;

namespace WotBlitzStatician.Data
{
	public interface IBlitzStaticianDataAccessor
	{
		AccountInfo GetAccountInfo(string nick);

		AccountInfo GetLastLoggedAccount();

		AccountTankStatistics GetAllTanksByAccount(long accountId);

		AccountInfoStatistics GetAccountStatistics(long accountId);

		AccountInfoPrivate GetAccountPrivateStatistics(long accountId);

		void SaveAccountInfo(AccountInfo accountInfo);

		void SaveTanksStatistic(List<AccountTankStatistics> taksStat);
	}
}
