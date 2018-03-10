using System.Threading.Tasks;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations
{
	public class CopyNewAccountDataOperation : IStatisticsCollectorOperation
	{
		public async Task Execute(StatisticsCollectorOperationContext operationContext)
		{
			foreach (var account in operationContext.Accounts)
			{
				account.CurrentAccountInfo.LastBattleTime = account.WargamingAccountInfo.LastBattleTime;
				account.CurrentAccountInfo.AccountInfoStatistics = account.WargamingAccountInfo.AccountInfoStatistics;
			}
		}
	}
}
