namespace WotBlitzStatician.WotApiClient
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using WotBlitzStatician.Model;
	using WotBlitzStatician.WotApiClient.DTO;

	public interface IWargamingApiClient
	{
		Task<List<VehicleEncyclopedia>> GetWotEncyclopediaVehiclesAsync();

		Task<List<AccountInfo>> FindAccountAsync(string nickName);

		Task<AccountInfo> GetAccountInfoAllStatisticsAsync(long accountId);

		Task<List<AccountTankStatistics>> GetTanksStatisticsAsync(long accountId);
		
		Task<WotEncyclopediaInfo> GetStaticDictionaries();
	}
}
