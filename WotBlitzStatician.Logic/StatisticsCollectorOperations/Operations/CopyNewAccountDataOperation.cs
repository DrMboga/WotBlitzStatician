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
				account.CurrentAccountInfo.AccountCreatedAt = account.WargamingAccountInfo.AccountCreatedAt;
				account.CurrentAccountInfo.LastBattleTime = account.WargamingAccountInfo.LastBattleTime;
				account.CurrentAccountInfo.AccountInfoStatistics = account.WargamingAccountInfo.AccountInfoStatistics;
				account.CurrentAccountInfo.AccountClanInfo = account.WargamingAccountInfo.AccountClanInfo;
				if (account.CurrentAccountInfo.AccountClanInfo != null)
				{
					account.CurrentAccountInfo.AccountClanInfo.AccountId = account.CurrentAccountInfo.AccountId;
				}
				account.CurrentAccountInfo.Achievements = account.WargamingAccountInfo.Achievements;
				account.CurrentAccountInfo.AchievementsMaxSeries = account.WargamingAccountInfo.AchievementsMaxSeries;
			}
		}
	}
}
