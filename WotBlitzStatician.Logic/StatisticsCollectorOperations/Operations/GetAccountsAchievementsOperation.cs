using System.Linq;
using System.Threading.Tasks;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;
using WotBlitzStatician.WotApiClient;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations
{
	public class GetAccountsAchievementsOperation : IStatisticsCollectorOperation
	{
		private readonly IWargamingApiClient _wargamingApiClient;

		public GetAccountsAchievementsOperation(IWargamingApiClient wargamingApiClient)
		{
			_wargamingApiClient = wargamingApiClient;
		}


		public async Task Execute(StatisticsCollectorOperationContext operationContext)
		{
      operationContext.OperationStateMessage = string.Empty;
      foreach (var accountInfo in operationContext.Accounts)
			{
				var accountInfoAchievements = await _wargamingApiClient.GetAccountAchievementsAsync(
					accountInfo.CurrentAccountInfo.AccountId);
				accountInfo.WargamingAccountInfo.Achievements = accountInfoAchievements
					.Where(a => a.IsMaxSeries == false)
					.ToList();
				accountInfo.WargamingAccountInfo.AchievementsMaxSeries = accountInfoAchievements
					.Where(a => a.IsMaxSeries == true)
					.ToList();
        operationContext.OperationStateMessage += $"Got {accountInfoAchievements.Count} achievements for account {accountInfo.CurrentAccountInfo.AccountId}; ";
      }
		}
	}
}
