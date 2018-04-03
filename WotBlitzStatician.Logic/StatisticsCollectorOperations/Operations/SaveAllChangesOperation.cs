using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WotBlitzStatician.Data.DataAccessors;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations
{
	public class SaveAllChangesOperation : IStatisticsCollectorOperation
	{
		private readonly IAccountDataAccessor _accountDataAccessor;

		public SaveAllChangesOperation(IAccountDataAccessor accountDataAccessor)
		{
			_accountDataAccessor = accountDataAccessor;
		}

		public async Task Execute(StatisticsCollectorOperationContext operationContext)
		{
			foreach (var accountInfo in operationContext.Accounts)
			{
				using (var transaction = await _accountDataAccessor.OpenTransactionAsync())
				{
					// Save account lastBattle
					await _accountDataAccessor.SaveLastBattleInfoAsync(accountInfo.CurrentAccountInfo);
					// Save account statistics + private data
					await _accountDataAccessor.SaveAccountPrivateInfoAndStatisticsAsync(accountInfo
																	.CurrentAccountInfo
																	.AccountInfoStatistics
																	.Single());
					// Merge AccountInfoStatistics.FragsList
					await _accountDataAccessor.MergeFragsAsync(accountInfo
																		.CurrentAccountInfo
																		.AccountInfoStatistics
																		.Single()
																		.FragsList);
					// Save account clan info
					await _accountDataAccessor.SaveAccountClanInfoAsync(
						accountInfo.CurrentAccountInfo.AccountId,
						accountInfo.CurrentAccountInfo.AccountClanInfo);
					// Save account clan history
					if(accountInfo.AccountClanHistory != null)
					{
						await _accountDataAccessor.SaveAccountClanHistoryAsync(accountInfo.AccountClanHistory);
					}
					// Save account acievements
					await _accountDataAccessor.MergeAccountAchievementsAsync(
						accountInfo.CurrentAccountInfo.AccountId,
						accountInfo.CurrentAccountInfo.Achievements,
						accountInfo.CurrentAccountInfo.AchievementsMaxSeries);
					// Save tanks statistics
					await _accountDataAccessor.SaveTankStatisticsAsync(accountInfo.AccountInfoTanks);
					// Save tanks achievements
					await _accountDataAccessor.MergeAccountInfoTankAchievementsAsync
						(accountInfo.AccountInfoTanks);

					// Save frags by allTanks using MergeFragsAsync
					var tankFrags = new List<FragListItem>();
					accountInfo.AccountInfoTanks.ForEach(t => tankFrags.AddRange(t.FragsList));
					await _accountDataAccessor.MergeFragsAsync(tankFrags);

					transaction.Commit();
				}
			}
		}
	}
}
