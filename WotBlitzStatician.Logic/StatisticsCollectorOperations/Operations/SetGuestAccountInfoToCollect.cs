using System.Threading.Tasks;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;
using WotBlitzStatician.Model;
using WotBlitzStatician.WotApiClient;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations
{
  public class SetGuestAccountInfoToCollect : IStatisticsCollectorOperation
  {
    private readonly long _accountId;

    public SetGuestAccountInfoToCollect(long accountId)
    {
      _accountId = accountId;
    }

    public async Task Execute(StatisticsCollectorOperationContext operationContext)
    {
      operationContext.Accounts.Add(new SatisticsCollectorAccountOperationContext
      { CurrentAccountInfo = new AccountInfo { AccountId = _accountId } });
      operationContext.OperationStateMessage = $"Account {_accountId} was set as guest account.";
    }

  }
}