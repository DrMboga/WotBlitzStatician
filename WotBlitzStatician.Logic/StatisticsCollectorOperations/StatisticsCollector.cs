using System.Threading.Tasks;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations
{
	public class StatisticsCollector : IStatisticsCollector
	{
		// ToDo: BeginLifeTymeScope

		public async Task CollectAllStatistics()
		{
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: Dispose LifeTymeScope
				}
				disposedValue = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
		}
		#endregion


	}
}
