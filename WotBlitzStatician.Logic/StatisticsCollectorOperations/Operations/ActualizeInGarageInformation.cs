using System.Threading.Tasks;
using WotBlitzStatician.Data.DataAccessors;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;

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
			foreach (var accountInfo in operationContext.Accounts)
			{
				if(string.IsNullOrEmpty(accountInfo.CurrentAccountInfo.AccessToken))
				{
					continue;
				}
				var allTanksByAccount = await _accountDataAccessor.GetActualTanksAsync(accountInfo.CurrentAccountInfo.AccountId);
			}
		}
	}
}
