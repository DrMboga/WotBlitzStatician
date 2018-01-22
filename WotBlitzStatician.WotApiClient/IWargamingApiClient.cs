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

		Task<AccountInfo> GetAccountInfoAllStatisticsAsync(long accountId, string accessToken, bool contactsIncluded = false);

		Task<List<AccountTankStatistics>> GetTanksStatisticsAsync(long accountId, string accessToken);
		
		Task<WotEncyclopediaInfo> GetStaticDictionariesAsync();

		Task<AccountClanInfo> GetAccountClanInfoAsync(long accountId);

		Task<List<Achievement>> GetAchievementsDictionaryAsync();
		
		Task<List<AccountInfoAchievment>> GetAccountAchievementsAsync(long accountId);
		
		Task<List<AccountInfoTankAchievment>> GetAccountTankAchievementsAsync(long accountId, string accessToken, List<int> tankIds = null);
	}
}
