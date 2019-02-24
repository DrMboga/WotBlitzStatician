using System.Threading.Tasks;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;
using WotBlitzStatician.WotApiClient;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations
{
	public class GetAllTanksStatisticsOperation : IStatisticsCollectorOperation
	{
		private readonly IWargamingApiClient _wargamingApiClient;

		public GetAllTanksStatisticsOperation(IWargamingApiClient wargamingApiClient)
		{
			_wargamingApiClient = wargamingApiClient;
		}


		public async Task Execute(StatisticsCollectorOperationContext operationContext)
		{
      operationContext.OperationStateMessage = string.Empty;
      foreach (var accountInfo in operationContext.Accounts)
			{
				accountInfo.AccountInfoTanks =
					await _wargamingApiClient.GetTanksStatisticsAsync(
						accountInfo.CurrentAccountInfo.AccountId,
						accountInfo.CurrentAccountInfo.AccessToken);
        operationContext.OperationStateMessage += $"Got {accountInfo.AccountInfoTanks.Count} tanks info from WG for account {accountInfo.CurrentAccountInfo.AccountId}; ";
      }
		}
	}
}
