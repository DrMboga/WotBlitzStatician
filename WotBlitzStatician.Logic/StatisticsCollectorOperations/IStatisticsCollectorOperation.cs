using System.Threading.Tasks;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations
{
	public interface IStatisticsCollectorOperation
    {
		Task Execute(StatisticsCollectorOperationContext operationContext);
    }
}
