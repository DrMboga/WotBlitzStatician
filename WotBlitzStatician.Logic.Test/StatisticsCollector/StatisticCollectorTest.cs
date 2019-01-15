using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.EntityFrameworkCore;
using WotBlitzStatician.Data;
using WotBlitzStatician.Model;
using Xunit;
using Xunit.Abstractions;

namespace WotBlitzStatician.Logic.Test.StatisticsCollector
{
  public class StatisticCollectorTest : IDisposable
  {
    private readonly ITestOutputHelper _output;
    private readonly DataStubs _dataStubs;
    private readonly IContainer _container;
    private readonly BlitzStaticianDbContext _dbContext;
    private readonly IStatisticsCollectorEngine _statisticsCollectorEngine;
    private readonly IStatisticsCollectorFactory _statisticsCollectorFactory;

    public StatisticCollectorTest(ITestOutputHelper output)
    {
      _output = output;
      _dataStubs = new DataStubs(
          accountCreatedAt: DateTime.Now.AddMonths(-6),
          dbLastBattleTime: DateTime.Now.AddHours(-2),
          dbAccessTokenExp: DateTime.Now.AddDays(1),
          wgLastBattleTime: DateTime.Now.AddHours(-1)
      );

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

      var accounts = _dbContext.AccountInfo.AsNoTracking().ToList();
      _output.WriteLine($"-Accounts count {accounts.Count}");

      await _statisticsCollectorEngine.Collect(
                _statisticsCollectorFactory.CreateCollector(_dataStubs.AccountInfo.AccountId));

        // ToDo: Asserts
    }

    [Fact]
    public void SetInitialData()
    {
      var vehicles = _dataStubs.Vehicles;
      _dbContext.VehicleEncyclopedia.AddRange(vehicles);

      _dbContext.AccountInfo.Add(_dataStubs.AccountInfo);

      _dbContext.SaveChanges();
    }

    public void Dispose()
    {
      _dbContext.Dispose();
    }

  }
}