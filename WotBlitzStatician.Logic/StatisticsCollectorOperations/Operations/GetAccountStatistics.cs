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
			foreach (var accountInfo in operationContext.Accounts)
			{
				var accountInfoFromWg = await _wargamingApiClient.GetAccountInfoAllStatisticsAsync(
					accountInfo.CurrentAccountInfo.AccountId,
					accountInfo.CurrentAccountInfo.AccessToken,
					true);
				accountInfo.WargamingAccountInfo = accountInfoFromWg;
			}
		}
	}
}
