using System;
using System.Threading.Tasks;
using WotBlitzStatician.Data;
using WotBlitzStatician.WotApiClient;
using WotBlitzStaticitian.Model;

namespace WotBlitzStatician.Logic
{
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
				var accountfromWg = await _wgApiClient.FindAccount(nick);
				if (accountfromWg == null)
					throw new ArgumentException($"Nick '{nick}' not found.", nameof(nick));
				await LoadStatisticsFromWg(accountfromWg);
				return accountfromWg;
			}

			return account;
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
			var account = await _wgApiClient.GetAccountInfoAllStatistics(accountId);
			await LoadStatisticsFromWg(account);
		}

		public async Task LoadStatisticsFromWg(AccountInfo accountInfo)
		{
			var tanksInfo = await _wgApiClient.GetTanksStatisticks(accountInfo.AccountId);
			_dataAccessor.SaveAccountInfo(accountInfo);
			_dataAccessor.SaveTanksStatistic(tanksInfo);
		}
	}
}
