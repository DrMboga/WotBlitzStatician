using System.Collections.Generic;
using System.Threading.Tasks;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations
{
	public class FilterByLastBattleTimeOperation : IStatisticsCollectorOperation
	{
		#pragma warning disable CS1998
		public async Task Execute(StatisticsCollectorOperationContext operationContext)
		{
      operationContext.OperationStateMessage = string.Empty;
      var accountsToRemove = new List<SatisticsCollectorAccountOperationContext>();
			foreach (var accountOperationContext in operationContext.Accounts)
			{
				if(accountOperationContext.WargamingAccountInfo.LastBattleTime <= 
					accountOperationContext.CurrentAccountInfo.LastBattleTime)
				{
					accountsToRemove.Add(accountOperationContext);
				}
        operationContext.OperationStateMessage += $"Filtering account '{accountOperationContext.CurrentAccountInfo.AccountId}' by last battle '{accountOperationContext.CurrentAccountInfo.LastBattleTime}'; ";
      }
			foreach (var acc in accountsToRemove)
			{
				operationContext.Accounts.Remove(acc);
			}

			if(operationContext.Accounts.Count == 0)
			{
				operationContext.OperationState = OperationState.NoAccounts;
				operationContext.OperationStateMessage += "No accounts for process";
			}
			else
			{
        operationContext.OperationStateMessage += $"Remained {operationContext.Accounts.Count} accounts for process";
      }
		}
		#pragma warning restore CS1998
	}
}
