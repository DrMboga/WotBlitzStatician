using System.Threading.Tasks;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations
{
	public interface IStatisticsCollectorOperation
    {
		Task Execute(StatisticsCollectorOperationContext operationContext);
    }
}
