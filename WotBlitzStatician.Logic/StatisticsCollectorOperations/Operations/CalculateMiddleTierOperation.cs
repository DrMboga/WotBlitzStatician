using System.Linq;
using System.Threading.Tasks;
using WotBlitzStatician.Data.DataAccessors;
using WotBlitzStatician.Logic.Calculations;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations
{
  public class CalculateMiddleTierOperation : IStatisticsCollectorOperation
  {
    private readonly IBlitzStaticianDictionary _blitzStaticianDictionary;

    public CalculateMiddleTierOperation(IBlitzStaticianDictionary blitzStaticianDictionary)
    {
      _blitzStaticianDictionary = blitzStaticianDictionary;
    }

    public async Task Execute(StatisticsCollectorOperationContext operationContext)
    {
      var tankTires = await _blitzStaticianDictionary.GetVehiclesTires();
      foreach (var account in operationContext.Accounts)
      {
        account.WargamingAccountInfo.AccountInfoStatistics.Single()
          .CalculateMiddleTier(account.AccountInfoTanks, tankTires);
      }
      operationContext.OperationStateMessage = $"Calculated middle tier for {operationContext.Accounts.Count} accounts";
    }
  }
}
