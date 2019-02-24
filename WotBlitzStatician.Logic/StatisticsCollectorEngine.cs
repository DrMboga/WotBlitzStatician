using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WotBlitzStatician.Logic.StatisticsCollectorOperations;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;

namespace WotBlitzStatician.Logic
{
  public class StatisticsCollectorEngine : IStatisticsCollectorEngine
  {
    private readonly ILogger<StatisticsCollectorEngine> _logger;

    public StatisticsCollectorEngine(ILogger<StatisticsCollectorEngine> logger)
    {
      _logger = logger;
    }

    public async Task Collect(IStatisticsCollector collector)
    {
      var operationContext = new StatisticsCollectorOperationContext();
      using (collector)
      {
        for (int i = 0; i < collector.OperationsCount; i++)
        {
          var operation = collector.GetOperation(i);
          await operation.Execute(operationContext);
          if (operationContext.OperationState != OperationState.Ok)
          {
            _logger.LogWarning(
                $"Operation '{operation.GetType()}' was terminate because of its state '{operationContext.OperationState}': {operationContext.OperationStateMessage}");
            break;
          }
          else
          {
            _logger.LogInformation($"Operation '{operation.GetType()}'; done with message '{operationContext.OperationStateMessage}'");
          }
        }
      }
    }
  }

}