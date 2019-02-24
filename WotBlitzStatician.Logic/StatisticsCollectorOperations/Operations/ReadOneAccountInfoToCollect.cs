using System.Threading.Tasks;
using WotBlitzStatician.Data.DataAccessors;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations
{
  public class ReadOneAccountInfoToCollect : IStatisticsCollectorOperation
  {
    private readonly IAccountDataAccessor _accountDataAccessor;
    private readonly long _accountId;

    public ReadOneAccountInfoToCollect(IAccountDataAccessor accountDataAccessor, long accountId)
    {
      _accountDataAccessor = accountDataAccessor;
      _accountId = accountId;
    }

    public async Task Execute(StatisticsCollectorOperationContext operationContext)
    {
      var accountForProcess = await _accountDataAccessor.GetShortAccountInfo(_accountId);

      if (accountForProcess == null)
      {
        operationContext.OperationState = OperationState.NoDataFound;
        operationContext.OperationStateMessage = "No accounts found";
      }
      operationContext.Accounts.Add(new SatisticsCollectorAccountOperationContext
      { CurrentAccountInfo = accountForProcess });
      operationContext.OperationStateMessage = $"Account {accountForProcess.AccountId} has read from DB.";
    }
  }
}