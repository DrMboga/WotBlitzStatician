using System.Threading.Tasks;
using WotBlitzStatician.Data.DataAccessors;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;

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
			var accountsForProcess = await _accountDataAccessor.GetAllAccountsAsync();
			foreach(var accountForProcess in accountsForProcess)
			{
				operationContext.Accounts.Add(new SatisticsCollectorAccountOperationContext
				{ CurrentAccountInfo = accountForProcess });
			}
			if(operationContext.Accounts.Count == 0)
			{
				operationContext.OperationState = OperationState.NoDataFound;
				operationContext.OperationStateMessage = "No accounts found";
			}
			else
			{
        operationContext.OperationStateMessage = $"Found in DB {accountsForProcess.Count} acconts for process";
      }
		}
	}
}
