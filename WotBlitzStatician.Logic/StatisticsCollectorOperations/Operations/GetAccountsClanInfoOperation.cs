using System.Threading.Tasks;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;
using WotBlitzStatician.WotApiClient;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations
{
	public class GetAccountsClanInfoOperation : IStatisticsCollectorOperation
	{
		private readonly IWargamingApiClient _wargamingApiClient;

		public GetAccountsClanInfoOperation(IWargamingApiClient wargamingApiClient)
		{
			_wargamingApiClient = wargamingApiClient;
		}

		public async Task Execute(StatisticsCollectorOperationContext operationContext)
		{
			foreach (var accountInfo in operationContext.Accounts)
			{
				accountInfo.WargamingAccountInfo.AccountClanInfo = await _wargamingApiClient.GetAccountClanInfoAsync(
					accountInfo.CurrentAccountInfo.AccountId);
			}
		}
	}
}
