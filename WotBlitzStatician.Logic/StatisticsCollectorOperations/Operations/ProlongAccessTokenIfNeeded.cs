using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WotBlitzStatician.Data.DataAccessors;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;
using WotBlitzStatician.WotApiClient;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations
{
	public class ProlongAccessTokenIfNeeded : IStatisticsCollectorOperation
	{
		// ToDo: appsettings
		private readonly int _daysBeforeExpire = 2;
		private readonly IWargamingApiClient _wargamingApiClient;
		private readonly IAccountDataAccessor _accountDataAccessor;
		private readonly ILogger<ProlongAccessTokenIfNeeded> _logger;

		public ProlongAccessTokenIfNeeded(
			IWargamingApiClient wargamingApiClient,
			IAccountDataAccessor accountDataAccessor,
			ILogger<ProlongAccessTokenIfNeeded> logger)
		{
			_wargamingApiClient = wargamingApiClient;
			_accountDataAccessor = accountDataAccessor;
			_logger = logger;
		}

		public async Task Execute(StatisticsCollectorOperationContext operationContext)
		{
			foreach(var accountInfo in operationContext
				.Accounts
				.Where(a => 
						a.CurrentAccountInfo.AccessTokenExpiration.HasValue && 
						(a.CurrentAccountInfo.AccessTokenExpiration.Value - DateTime.Now).TotalDays <= _daysBeforeExpire))
			{
				if(accountInfo.CurrentAccountInfo.AccessTokenExpiration.Value < DateTime.Now)
				{
					accountInfo.CurrentAccountInfo.AccessToken = string.Empty;
					_logger.LogWarning($"Account '{accountInfo.CurrentAccountInfo.AccountId}' has expired access token");
					return;
				}
				var prolongation = await _wargamingApiClient.ProlongateAccount(accountInfo.CurrentAccountInfo.AccessToken);
				accountInfo.CurrentAccountInfo.AccessToken = prolongation.AccessToken;
				accountInfo.CurrentAccountInfo.AccessTokenExpiration = prolongation.AccessTokenExpiration;
				await _accountDataAccessor.SaveProlongedAccountAsync(
					accountInfo.CurrentAccountInfo.AccountId,
					accountInfo.CurrentAccountInfo.AccessToken,
					accountInfo.CurrentAccountInfo.AccessTokenExpiration.Value);
			}
		}
	}
}
