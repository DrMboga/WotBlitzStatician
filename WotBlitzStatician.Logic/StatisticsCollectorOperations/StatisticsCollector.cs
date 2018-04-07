using System.Threading.Tasks;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations
{
	using Autofac;
	using Microsoft.Extensions.Logging;
	using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;
	using WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations;

	public class StatisticsCollector : IStatisticsCollector
	{
		private readonly ILogger<StatisticsCollector> _logger;
		private readonly ILifetimeScope _childScope;
		private readonly int _operationsCount;

		public StatisticsCollector(ILifetimeScope lifetimeScope, ILogger<StatisticsCollector> logger)
		{
			_logger = logger;
			(_childScope, _operationsCount) = CreateOperationSteps(lifetimeScope);
		}

		public async Task CollectAllStatistics()
		{
			var operationContext = new StatisticsCollectorOperationContext();
			for (int i = 0; i < _operationsCount; i++)
			{
				var operation = _childScope.ResolveKeyed<IStatisticsCollectorOperation>(i);
				await operation.Execute(operationContext);
				if(operationContext.OperationState != OperationState.Ok)
				{
					_logger.LogWarning(
						$"Operation '{operation.GetType()}' was terminate becaus of its state '{operationContext.OperationState}': {operationContext.OperationStateMessage}");
					break;
				}
			}
		}

		private static (ILifetimeScope, int) CreateOperationSteps(ILifetimeScope lifetimeScope)
		{
			var childScope = lifetimeScope.BeginLifetimeScope(builder =>
			{
				builder.RegisterType<ReadAccountInfosToCollect>()
					.Keyed<IStatisticsCollectorOperation>(0);
				builder.RegisterType<ProlongAccessTokenIfNeeded>()
					.Keyed<IStatisticsCollectorOperation>(1);
				builder.RegisterType<GetAccountStatistics>()
					.Keyed<IStatisticsCollectorOperation>(2);
				builder.RegisterType<FilterByLastBattleTimeOperation>()
					.Keyed<IStatisticsCollectorOperation>(3);
				builder.RegisterType<GetAccountsClanInfoOperation>()
					.Keyed<IStatisticsCollectorOperation>(4);
				builder.RegisterType<CreateAccountClanHistoryOperation>()
					.Keyed<IStatisticsCollectorOperation>(5);
				builder.RegisterType<GetAccountsAchievementsOperation>()
					.Keyed<IStatisticsCollectorOperation>(6);
				builder.RegisterType<GetAllTanksStatisticsOperation>()
					.Keyed<IStatisticsCollectorOperation>(7);
				builder.RegisterType<CalculateMiddleTierOperation>()
					.Keyed<IStatisticsCollectorOperation>(8);
				builder.RegisterType<ActualizeInGarageInformation>()
					.Keyed<IStatisticsCollectorOperation>(9);
				builder.RegisterType<FilterTanksByLastSessionOperation>()
					.Keyed<IStatisticsCollectorOperation>(10);
				builder.RegisterType<CalculateWn7Operation>()
					.Keyed<IStatisticsCollectorOperation>(11);
				builder.RegisterType<GetTanksAchievementsOperation>()
					.Keyed<IStatisticsCollectorOperation>(12);
				builder.RegisterType<CopyNewAccountDataOperation>()
					.Keyed<IStatisticsCollectorOperation>(13);
				builder.RegisterType<SaveAllChangesOperation>()
					.Keyed<IStatisticsCollectorOperation>(14);
			});

			// ToDo: Increase this number if adding new operation to scope.
			// It's an autofac restriction - We can't iterate through IIndex<IStatisticsCollectorOperation, int>
			int operationsCount = 15;

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
