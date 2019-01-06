using System;
using System.Threading.Tasks;
using Autofac;
using WotBlitzStatician.Data;
using WotBlitzStatician.Model;
using Xunit;

namespace WotBlitzStatician.Logic.Test.StatisticsCollector
{
    public class StatisticCollectorTest : IDisposable
    {
        private readonly DataStubs _dataStubs;
        private readonly IContainer _container;
        private readonly BlitzStaticianDbContext _dbContext;
        private readonly IStatisticsCollectorEngine _statisticsCollectorEngine;
        private readonly IStatisticsCollectorFactory _statisticsCollectorFactory;

        public StatisticCollectorTest()
        {
            _dataStubs = new DataStubs();

            var containerBuilder = new ContainerBuilder();

            containerBuilder.SetupLoggerMocks();
            containerBuilder.AddInMemoryDataBase();
            containerBuilder.ConfigureBlitzStaticianLogic();
            containerBuilder.SetupWargamingApiMockDependencies(_dataStubs);

            _container = containerBuilder.Build();

            _statisticsCollectorFactory = _container.Resolve<IStatisticsCollectorFactory>();
            _statisticsCollectorEngine = _container.Resolve<IStatisticsCollectorEngine>();
            _dbContext = _container.Resolve<BlitzStaticianDbContext>();
        }

        [Fact]
        public async Task TestWholeCollectOperation()
        {
            SetInitialData();
            await _statisticsCollectorEngine.Collect(
                _statisticsCollectorFactory.CreateCollector(_dataStubs.AccountInfo.AccountId));

        }

        public void SetInitialData()
        {
            _dbContext.AccountInfo.Add(_dataStubs.AccountInfo);

            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

    }
}