using System.Collections.Generic;
using System.Threading.Tasks;
using WotBlitzStaticitian.Model;

namespace WotBlitzStatician.WotApiClient
{
	public interface IWargamingApiClient
	{
		Task<List<VehicleEncyclopedia>> GetWotEncyclopediaVehiclesAsync();

		Task<AccountInfo> FindAccountAsync(string nickName);

		Task<AccountInfo> GetAccountInfoAllStatisticsAsync(string accountId);

		Task<List<AccountTankStatistics>> GetTanksStatisticsAsync(string accountId);
	}
}
