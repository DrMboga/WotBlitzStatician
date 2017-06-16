using System.Collections.Generic;
using System.Threading.Tasks;
using WotBlitzStaticitian.Model;

namespace WotBlitzStatician.WotApiClient
{
	public interface IWargamingApiClient
	{
		Task<List<VehicleEncyclopedia>> GetWotEncyclopediaVehiclesAsync();

		Task<List<AccountInfo>> FindAccountAsync(string nickName);

		Task<AccountInfo> GetAccountInfoAllStatisticsAsync(long accountId);

		Task<List<AccountTankStatistics>> GetTanksStatisticsAsync(long accountId);
	}
}
