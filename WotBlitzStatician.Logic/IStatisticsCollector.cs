using System;
using System.Threading.Tasks;
using WotBlitzStatician.Logic.StatisticsCollectorOperations;

namespace WotBlitzStatician.Logic
{
    public interface IStatisticsCollector : IDisposable
    {
        int OperationsCount { get; }
        IStatisticsCollectorOperation GetOperation(int index);
    }
}
