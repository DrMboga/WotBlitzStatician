using System;
using System.Linq;
using System.Threading.Tasks;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;
using WotBlitzStatician.WotApiClient;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations
{
	public class GetTanksAchievementsOperation : IStatisticsCollectorOperation
	{
		private readonly IWargamingApiClient _wargamingApiClient;

		public GetTanksAchievementsOperation(IWargamingApiClient wargamingApiClient)
		{
			_wargamingApiClient = wargamingApiClient;
		}

		public async Task Execute(StatisticsCollectorOperationContext operationContext)
		{
      operationContext.OperationStateMessage = string.Empty;
      foreach (var accountInfo in operationContext.Accounts)
			{
				var tankIds = accountInfo.AccountInfoTanks
					.Select(t => Convert.ToInt32(t.TankId))
					.ToList();
				var allAchievements = await _wargamingApiClient.GetAccountTankAchievementsAsync(
					accountInfo.CurrentAccountInfo.AccountId,
					accountInfo.CurrentAccountInfo.AccessToken,
					tankIds);
				foreach (var tank in accountInfo.AccountInfoTanks)
				{
					tank.Achievements = allAchievements
						.Where(a => a.TankId == tank.TankId && a.IsMaxSeries == false)
						.ToList();
					tank.AchievementsMaxSeries = allAchievements
						.Where(a => a.TankId == tank.TankId && a.IsMaxSeries == true)
						.ToList();
				}
        operationContext.OperationStateMessage += $"Got {allAchievements.Count} from WG for account {accountInfo.CurrentAccountInfo.AccountId}; ";
      }
		}
	}
}
