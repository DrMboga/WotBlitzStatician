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
						(DateTime.Today - a.AccessTokenExpiration.Value).TotalDays <= _daysBeforeExpire))
			{
				var prolongation = await _wargamingApiClient.ProlongateAccount(accountInfo.AccessToken);
				// Update database
			}
		}
	}
}
