using System.Collections.Generic;
using System.Threading.Tasks;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations
{
	public class FilterByLastBattleTimeOperation : IStatisticsCollectorOperation
	{
		public async Task Execute(StatisticsCollectorOperationContext operationContext)
		{
			var accountsToRemove = new List<SatisticsCollectorAccountOperationContext>();
			foreach (var accountOperationContext in operationContext.Accounts)
			{
				if(accountOperationContext.WargamingAccountInfo.LastBattleTime <= 
					accountOperationContext.CurrentAccountInfo.LastBattleTime)
				{
					accountsToRemove.Add(accountOperationContext);
				}
			}
			foreach (var acc in accountsToRemove)
			{
				operationContext.Accounts.Remove(acc);
			}

			if(operationContext.Accounts.Count == 0)
			{
				operationContext.OperationState = OperationState.NoAccounts;
				operationContext.OperationStateMessage = "No accounts for process";
			}
		}
	}
}
