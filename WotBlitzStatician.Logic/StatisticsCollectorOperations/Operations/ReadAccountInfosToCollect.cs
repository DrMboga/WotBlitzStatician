using System.Threading.Tasks;
using WotBlitzStatician.Data.DataAccessors;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations
{
	public class ReadAccountInfosToCollect : IStatisticsCollectorOperation
	{
		private readonly IAccountDataAccessor _accountDataAccessor;

		public ReadAccountInfosToCollect(IAccountDataAccessor accountDataAccessor)
		{
			_accountDataAccessor = accountDataAccessor;
		}

		public async Task Execute(StatisticsCollectorOperationContext operationContext)
		{
			operationContext.Accounts = await _accountDataAccessor.GetAllAccountsAsync();
			if(operationContext.Accounts == null || operationContext.Accounts.Count == 0)
			{
				operationContext.OperationState = OperationState.NoDataFound;
				operationContext.OperationStateMessage = "No accounts found";
			}
		}
	}
}
