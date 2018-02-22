using System;
using System.Linq;
using System.Threading.Tasks;
using WotBlitzStatician.Data.DataAccessors;
using WotBlitzStatician.WotApiClient;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations
{
	public class ProlongAccessTokenIfNeeded : IStatisticsCollectorOperation
	{
		// ToDo: appsettings
		private readonly int _daysBeforeExpire = 2;
		private readonly IWargamingApiClient _wargamingApiClient;
		private readonly IAccountDataAccessor _accountDataAccessor;

		public ProlongAccessTokenIfNeeded(
			IWargamingApiClient wargamingApiClient,
			IAccountDataAccessor accountDataAccessor)
		{
			_wargamingApiClient = wargamingApiClient;
			_accountDataAccessor = accountDataAccessor;
		}

		public async Task Execute(StatisticsCollectorOperationContext operationContext)
		{
			foreach(var accountInfo in operationContext
				.Accounts
				.Where(a => 
						a.AccessTokenExpiration.HasValue && 
						(a.AccessTokenExpiration.Value - DateTime.Now).TotalDays <= _daysBeforeExpire))
			{
				var prolongation = await _wargamingApiClient.ProlongateAccount(accountInfo.AccessToken);
				accountInfo.AccessToken = prolongation.AccessToken;
				accountInfo.AccessTokenExpiration = prolongation.AccessTokenExpiration;
				await _accountDataAccessor.SaveProlongedAccountAsync(
					accountInfo.AccountId,
					accountInfo.AccessToken,
					accountInfo.AccessTokenExpiration.Value);
			}
		}
	}
}
