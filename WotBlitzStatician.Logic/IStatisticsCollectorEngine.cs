using System.Threading.Tasks;

namespace WotBlitzStatician.Logic
{
    public interface IStatisticsCollectorEngine
    {
        Task Collect(IStatisticsCollector collector);
    }
}