using System;
using Autofac;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations
{
    public class AllAccountStatisticsCollector : IStatisticsCollector
    {
        private readonly ILifetimeScope _childScope;

        public AllAccountStatisticsCollector(ILifetimeScope lifetimeScope)
        {
            (_childScope, OperationsCount) = CreateOperationSteps(lifetimeScope);
        }

        public int OperationsCount { get; }
        public IStatisticsCollectorOperation GetOperation(int index)
        {
            if (index < 0 || index > OperationsCount)
                throw new IndexOutOfRangeException($"Index {index} must be between 0 and {OperationsCount}");

            return _childScope.ResolveKeyed<IStatisticsCollectorOperation>(index);
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
