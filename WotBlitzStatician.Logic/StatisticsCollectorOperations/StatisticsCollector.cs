using System.Threading.Tasks;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations
{
	using Autofac;
	using WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations;

	public class StatisticsCollector : IStatisticsCollector
	{
		private readonly ILifetimeScope _childScope;
		private readonly int _operationsCount;

		public StatisticsCollector(ILifetimeScope lifetimeScope)
		{
			(_childScope, _operationsCount) = CreateOperationSteps(lifetimeScope);
		}

		public async Task CollectAllStatistics()
		{
			var operationContext = new StatisticsCollectorOperationContext();
			for (int i = 0; i < _operationsCount; i++)
			{
				var operation = _childScope.ResolveKeyed<IStatisticsCollectorOperation>(i);
				await operation.Execute(operationContext);
			}
		}

		private static (ILifetimeScope, int) CreateOperationSteps(ILifetimeScope lifetimeScope)
		{
			var childScope = lifetimeScope.BeginLifetimeScope(builder =>
			{
				builder.RegisterType<ReadAccountInfosToCollect>()
					.Keyed<IStatisticsCollectorOperation>(0);
			});

			// ToDo: Increase this number if adding new operation to scope.
			// It's an autofac restriction - We can't iterate through IIndex<IStatisticsCollectorOperation, int>
			int operationsCount = 1;

			return (childScope, operationsCount);
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					_childScope.Dispose();
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
