using System.Linq;
using System.Threading.Tasks;
using WotBlitzStatician.Data.DataAccessors;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;

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
					// Save account clan info
					// Save account acievements
					// Save tanks statistics
					// Save tanks achievements

					//transaction.Commit();
				}
			}
		}
	}
}
