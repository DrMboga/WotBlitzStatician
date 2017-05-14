using System.Collections.Generic;
using System.Threading.Tasks;
using WotBlitzStaticitian.Model;

namespace WotBlitzStatician.WotApiClient
{
	public interface IWargamingApiClient
	{
		Task<List<VehicleEncyclopedia>> GetWontEncyclopediaVehicles();

		Task<AccountInfo> FindAccount(string nickName);

		Task<AccountInfo> GetAccountInfoAllStatistics(long accountId);

		Task<List<AccountTankStatistics>> GetTanksStatisticks(long accountId);
	}
}
