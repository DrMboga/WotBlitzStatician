using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WotBlitzStatician.Data.DataAccessors;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations
{
	public class ActualizeInGarageInformation : IStatisticsCollectorOperation
	{
		private readonly IAccountDataAccessor _accountDataAccessor;

		public ActualizeInGarageInformation(IAccountDataAccessor accountDataAccessor)
		{
			_accountDataAccessor = accountDataAccessor;
		}

		public async Task Execute(StatisticsCollectorOperationContext operationContext)
		{
      operationContext.OperationStateMessage = string.Empty;
      foreach (var accountInfo in operationContext.Accounts)
			{
				if(string.IsNullOrEmpty(accountInfo.CurrentAccountInfo.AccessToken))
				{
					continue;
				}
				var allTanksByAccount = await _accountDataAccessor.GetActualTanksAsync(
					accountInfo.CurrentAccountInfo.AccountId);

				var tanksBeforLastSession = accountInfo.AccountInfoTanks
					.Where(t => t.LastBattleTime <= accountInfo.CurrentAccountInfo.LastBattleTime)
					.ToList();

				var changedGarageInfoList = new List<AccountTankStatistics>();
				foreach (var tank in tanksBeforLastSession)
				{
					var tankWithChangedInGarage = allTanksByAccount
						.FirstOrDefault(t => t.TankId == tank.TankId
											&& t.AccountId == tank.AccountId
											&& t.InGarage != tank.InGarage);
					if(tankWithChangedInGarage != null)
					{
						tankWithChangedInGarage.InGarage = tank.InGarage;
						tankWithChangedInGarage.InGarageUpdated = tank.InGarageUpdated;
						changedGarageInfoList.Add(tankWithChangedInGarage);
					}
				}

				if (changedGarageInfoList.Count > 0)
				{
					await _accountDataAccessor.UpdateInnGarageInfoAsync(changedGarageInfoList);
          operationContext.OperationStateMessage += "Updated {changedGarageInfoList.Count} tanks in garage info for account {accountInfo.CurrentAccountInfo.AccountId}; ";
        }
			}
		}
	}
}
