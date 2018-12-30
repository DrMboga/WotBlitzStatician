using System;
using System.Threading.Tasks;
using Autofac;
using WotBlitzStatician.Data;
using Xunit;

namespace WotBlitzStatician.Logic.Test.StatisticsCollector
{
    public class StatisticCollectorTest : IDisposable
    {
        private const long CurrentAccountId = 15;

        private readonly IContainer _container;
        private readonly BlitzStaticianDbContext _dbContext;
        private readonly IStatisticsCollectorEngine _statisticsCollectorEngine;
        private readonly IStatisticsCollectorFactory _statisticsCollectorFactory;

        public StatisticCollectorTest()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.AddInMemoryDataBase();

            _container = containerBuilder.Build();
            _dbContext = _container.Resolve<BlitzStaticianDbContext>();
        }

        [Fact]
        public async Task TestWholeCollectOperation()
        {
            await _statisticsCollectorEngine.Collect(
                _statisticsCollectorFactory.CreateCollector(CurrentAccountId));

        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

    }
}