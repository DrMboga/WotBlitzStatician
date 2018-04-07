using System;
using System.Threading.Tasks;

namespace WotBlitzStatician.Logic
{
	public interface IStatisticsCollector : IDisposable
    {
		Task CollectAllStatistics();
    }
}
