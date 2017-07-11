namespace WotBlitzStatician.Logic
{
	using System;
	using System.Threading.Tasks;
	using WotBlitzStatician.Data;
	using WotBlitzStatician.WotApiClient;
	using WotBlitzStatician.Model;
	using System.Linq;
	
	public class BlitzStaticianLogic : IBlitzStaticianLogic
	{
		private readonly IBlitzStaticianDataAccessor _dataAccessor;
		private readonly IWargamingApiClient _wgApiClient;

		public BlitzStaticianLogic(
			IBlitzStaticianDataAccessor blitzStaticianDataAccessor,
			IWargamingApiClient wargamingApiClient)
		{
			_dataAccessor = blitzStaticianDataAccessor;
			_wgApiClient = wargamingApiClient;
		}

		public async Task<AccountInfo> GetAccount(string nick)
		{
			var account = _dataAccessor.GetAccountInfo(nick);
			if (account == null)
			{
				//First time
				var accountsfromWg = await _wgApiClient.FindAccountAsync(nick);
				if (accountsfromWg != null && accountsfromWg.Count > 0)
				{
					// Find exactly the same nick
					account = accountsfromWg.FirstOrDefault(a => a != null && a.NickName.Equals(nick, StringComparison.OrdinalIgnoreCase));
				}
				if (account == null)
					throw new ArgumentException($"Nick '{nick}' not found.", nameof(nick));
				// Loading all account statistics
				account = await _wgApiClient.GetAccountInfoAllStatisticsAsync(account.AccountId);
				await LoadStatisticsFromWgAndSaveToDb(account);
			}

			return account;
		}

        public async Task LoadStaticDataAndSaveToDb()
        {
            var encyclopedia = await _wgApiClient.GetStaticDictionariesAsync();
            var vehicles = await _wgApiClient.GetWotEncyclopediaVehiclesAsync();
            var achievements = await _wgApiClient.GetAchievementsDictionaryAsync();

            _dataAccessor.SaveLanguagesDictionary(encyclopedia.DictionaryLanguages);
            _dataAccessor.SaveNationsDictionary(encyclopedia.DictionaryNationses);
            _dataAccessor.SaveVehicleTypesDictionary(encyclopedia.DictionaryVehicleTypes);
            _dataAccessor.SaveVehicleEncyclopedia(vehicles);
            _dataAccessor.SaveAchievementsDictionary(achievements);
        }

		public AccountInfoPrivate GetAccountPrivateStatistics(long accountId)
		{
			return _dataAccessor.GetAccountPrivateStatistics(accountId);
		}

		public AccountInfoStatistics GetAccountStatistics(long accountId)
		{
			return _dataAccessor.GetAccountStatistics(accountId);
		}

		public AccountTankStatistics GetAllTanksByAccount(long accountId)
		{
			return _dataAccessor.GetAllTanksByAccount(accountId);
		}

		public AccountInfo GetLastLoggedAccount()
		{
			return _dataAccessor.GetLastLoggedAccount();
		}

		public async Task LoadStatisticsFromWgAsync(long accountId)
		{
			var account = await _wgApiClient.GetAccountInfoAllStatisticsAsync(accountId);
			await LoadStatisticsFromWgAndSaveToDb(account);
		}

		public async Task LoadStatisticsFromWgAndSaveToDb(AccountInfo accountInfo)
		{
			var tanksInfo = await _wgApiClient.GetTanksStatisticsAsync(accountInfo.AccountId);
			var clanInfo = await _wgApiClient.GetAccountClanInfoAsync(accountInfo.AccountId);
			var accountAchievements = await _wgApiClient.GetAccountAchievementsAsync(accountInfo.AccountId);
			var accountTankAchievements = await _wgApiClient.GetAccountTankAchievementsAsync(accountInfo.AccountId);

            		_dataAccessor.SaveAccountInfo(accountInfo);
			_dataAccessor.SaveAccountAchievements(accountAchievements);
            		_dataAccessor.SaveAccountTankAchievements(accountTankAchievements);
			_dataAccessor.SaveTanksStatistic(tanksInfo);
		    if(clanInfo != null && clanInfo.ClanId > 0)
		    {
			_dataAccessor.SaveClanInfo(clanInfo);
		    }
		}
    }
}
