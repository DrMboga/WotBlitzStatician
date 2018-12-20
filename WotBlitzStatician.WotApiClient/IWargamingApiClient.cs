namespace WotBlitzStatician.WotApiClient
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using WotBlitzStatician.Model;

	public interface IWargamingApiClient
	{
		Task<List<VehicleEncyclopedia>> GetWotEncyclopediaVehiclesAsync();

		Task<List<AccountInfo>> FindAccountAsync(string nickName);

		Task<AccountInfo> GetAccountInfoAllStatisticsAsync(long accountId, string accessToken, bool contactsIncluded = false);

		Task<List<AccountTankStatistics>> GetTanksStatisticsAsync(long accountId, string accessToken);
		
		Task<(
			List<DictionaryLanguage>, 
			List<DictionaryNations>, 
			List<DictionaryVehicleType>,
			List<AchievementSection>,
			List<DictionaryClanRole>)> GetStaticDictionariesAsync();

		Task<AccountClanInfo> GetAccountClanInfoAsync(long accountId);

		Task<List<Achievement>> GetAchievementsDictionaryAsync();
		
		Task<List<AccountInfoAchievement>> GetAccountAchievementsAsync(long accountId);
		
		Task<List<AccountInfoTankAchievement>> GetAccountTankAchievementsAsync(long accountId, string accessToken, List<int> tankIds = null);

		Task<AccountInfo> ProlongateAccount(string accessToken);

		void DownloadFile(string uri, string fileName);

		Task<PlayerPrivateInfoDto> GetAccountPrivateInfo(long accountId, string accessToken);
	}
}
