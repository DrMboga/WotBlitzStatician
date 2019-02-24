using System.Threading.Tasks;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;
using WotBlitzStatician.WotApiClient;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations
{
	public class GetAccountStatistics : IStatisticsCollectorOperation
	{
		private readonly IWargamingApiClient _wargamingApiClient;

		public GetAccountStatistics(IWargamingApiClient wargamingApiClient)
		{
			_wargamingApiClient = wargamingApiClient;
		}

		public async Task Execute(StatisticsCollectorOperationContext operationContext)
		{
      operationContext.OperationStateMessage = string.Empty;
      foreach (var accountInfo in operationContext.Accounts)
			{
				var accountInfoFromWg = await _wargamingApiClient.GetAccountInfoAllStatisticsAsync(
					accountInfo.CurrentAccountInfo.AccountId,
					accountInfo.CurrentAccountInfo.AccessToken);
				accountInfo.WargamingAccountInfo = accountInfoFromWg;
        operationContext.OperationStateMessage = $"Got account info from WG for account {accountInfo.CurrentAccountInfo.AccountId}; ";
      }
		}
	}
}
