﻿namespace WotBlitzStatician.Logic
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using WotBlitzStatician.Data;
	using WotBlitzStatician.WotApiClient;
	using WotBlitzStatician.Model;
	using System.Linq;
	using WotBlitzStatician.Logic.Calculations;
    using log4net;

    public class BlitzStaticianLogic : IBlitzStaticianLogic
	{
        private static readonly ILog _log = LogManager.GetLogger(typeof(BlitzStaticianLogic));
		private readonly IBlitzStaticianDataAccessor _dataAccessor;
		private readonly IWargamingApiClient _wgApiClient;
        private readonly IWgApiConfiguration _wgApiConfiguration;

		public BlitzStaticianLogic(
            IBlitzStaticianDataAccessor blitzStaticianDataAccessor,
            IWargamingApiClient wargamingApiClient,
            IWgApiConfiguration wargamingApiConfiguration)
		{
			_dataAccessor = blitzStaticianDataAccessor;
			_wgApiClient = wargamingApiClient;
            _wgApiConfiguration = wargamingApiConfiguration;
		}

		public async Task<AccountInfo> GetAccount(string nick)
		{
            await CheckAndUpdateStaticData();

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

		public async Task<List<AccountInfo>> FindAccounts(string nick)
		{
			return await _wgApiClient.FindAccountAsync(nick);
		}

		private async Task CheckAndUpdateStaticData()
		{
			var lastStaticDictionariesUpdateDate = _dataAccessor.GetStaticDataLastUpdateDate();

			if (Convert.ToInt32((DateTime.Now - lastStaticDictionariesUpdateDate).TotalDays) >=
			    _wgApiConfiguration.DictionariesUpdateFrequencyInDays)
			{
				await LoadStaticDataAndSaveToDb();
			}
		}

		public async Task LoadStaticDataAndSaveToDb()
        {
            var encyclopedia = await _wgApiClient.GetStaticDictionariesAsync();
            var vehicles = await _wgApiClient.GetWotEncyclopediaVehiclesAsync();
            var achievements = await _wgApiClient.GetAchievementsDictionaryAsync();

            encyclopedia.DictionaryLanguages.ForEach(l => l.LastUpdated = DateTime.Now);

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

		public async Task<AccountInfo> GetAccountStatistics(long accountId)
		{
			var accountInfo = _dataAccessor.GetAccountStatistics(accountId);
			if (accountInfo == null)
			{
				// First time
				// Loading all account statistics
				accountInfo = await _wgApiClient.GetAccountInfoAllStatisticsAsync(accountId);
				await LoadStatisticsFromWgAndSaveToDb(accountInfo);

			}
			return accountInfo;
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
			await CheckAndUpdateStaticData();
			var account = await _wgApiClient.GetAccountInfoAllStatisticsAsync(accountId);
			await LoadStatisticsFromWgAndSaveToDb(account);
		}

		public void SetLastLoggedAccount(long accountId)
		{
			_dataAccessor.SetLastSession(accountId);
		}

		public async Task LoadStatisticsFromWgAndSaveToDb(AccountInfo accountInfo)
        {
			var lastBattle = _dataAccessor.GetLastBattleTime(accountInfo.AccountId);
            _log.Debug($"Account id {accountInfo.AccountId}. LastBattle at '{lastBattle}'");
            if (lastBattle.HasValue && 
                (accountInfo.LastBattleTime.Value - lastBattle.Value).TotalMinutes <= 20) // ToDo: _config.RequestsDelayInMinutes
			{
                _log.Info(
			        $"Last update was at '{lastBattle:dd.MM.yyyy HH:mm}', so we don't need to update statistic again at the moment.");
		        return;
	        }

			var tanksInfo = await _wgApiClient.GetTanksStatisticsAsync(accountInfo.AccountId);
            var clanInfo = await _wgApiClient.GetAccountClanInfoAsync(accountInfo.AccountId);
            var accountAchievements = await _wgApiClient.GetAccountAchievementsAsync(accountInfo.AccountId);
            var accountTankAchievements = await _wgApiClient.GetAccountTankAchievementsAsync(accountInfo.AccountId);

            CalculateStatistitcs(accountInfo.AccountInfoStatistics, tanksInfo);

			// Filter tanksInfo by LastUpdateDate
	        tanksInfo = tanksInfo.Where(t => t.LastBattleTime >= (lastBattle ?? DateTime.MinValue)).ToList();
            _log.Debug($"Filtered {tanksInfo.Count()} tanks by last session");

			// Filter tank achievemnts
	        accountTankAchievements = accountTankAchievements.Where(a => tanksInfo.Any(t => t.TankId == a.TankId)).ToList();
			_log.Debug($"Filtered {accountTankAchievements.Count()} tank achievements by last session");

			_dataAccessor.SaveAccountInfo(accountInfo);
			_dataAccessor.SetLastSession(accountInfo.AccountId);
            _dataAccessor.SaveAccountAchievements(accountAchievements);
            _dataAccessor.SaveAccountTankAchievements(accountTankAchievements);
            _dataAccessor.SaveTanksStatistic(tanksInfo);
            if (clanInfo != null && clanInfo.ClanId > 0)
            {
                _dataAccessor.SaveClanInfo(clanInfo);
            }
        }

		private void CalculateStatistitcs(AccountInfoStatistics accountStat, List<AccountTankStatistics> tanksInfo)
		{
			var tankTires = _dataAccessor.GetVehicleTires();

			accountStat.AvgTier = accountStat.CalculateMiddleTier(tanksInfo, tankTires);
			accountStat.Wn7 = accountStat.CalculateWn7();
			accountStat.Effectivity = accountStat.CalculateEffectivity();

			foreach (var accountTankStatistic in tanksInfo)
			{
				if (!tankTires.ContainsKey(accountTankStatistic.TankId))
					continue;
				accountTankStatistic.Wn7 = accountTankStatistic.CalculateWn7(tankTires[accountTankStatistic.TankId]);
				accountTankStatistic.Effectivity = accountTankStatistic.CalculateEffectivity(tankTires[accountTankStatistic.TankId]);
			}
		}
	}
}
