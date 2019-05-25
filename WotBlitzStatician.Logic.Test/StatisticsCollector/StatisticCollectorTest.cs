using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.EntityFrameworkCore;
using WotBlitzStatician.Data;
using WotBlitzStatician.Logic.Test.Logging;
using WotBlitzStatician.Model;
using WotBlitzStatician.Model.Dto;
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

      var loggerFactory = TestOutputLoggerFactory.CreateLoggerFactory(output);

      containerBuilder.SetupLogger(loggerFactory);
      containerBuilder.AddInMemoryDataBase(loggerFactory);
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

      var accounts = _dbContext.AccountInfo.AsNoTracking().ToList();
      Assert.NotNull(accounts);
      Assert.Single(accounts);
      Assert.Equal(_dataStubs.WargamingAccountInfo.AccountId, accounts.Single().AccountId);
      Assert.Equal(_dataStubs.WargamingAccountInfo.LastBattleTime, accounts.Single().LastBattleTime);
      Assert.Equal(_dataStubs.AccountInfo.AccessTokenExpiration, accounts.Single().AccessTokenExpiration);

      var accountStat = _dbContext.AccountInfoStatistics.AsNoTracking().ToList();
      Assert.NotNull(accountStat);
      Assert.Single(accountStat);
      var wgStat = _dataStubs.WargamingAccountInfo.AccountInfoStatistics.Single();
      Assert.Equal(wgStat.AccountId, accountStat.Single().AccountId);
      Assert.Equal(wgStat.Battles, accountStat.Single().Battles);
      Assert.Equal(wgStat.UpdatedAt, accountStat.Single().UpdatedAt);

      var accountFrags = _dbContext.Frags.AsNoTracking().Where(f => f.TankId == null).ToList();
      Assert.NotNull(accountFrags);
      Assert.Equal(wgStat.FragsList.Count, accountFrags.Count);

      var clanInfo = _dbContext.AccountClanInfo.AsNoTracking().ToList();
      Assert.NotNull(clanInfo);
      Assert.Single(clanInfo);
      Assert.Equal(_dataStubs.AccountClanInfo.AccountId, clanInfo.Single().AccountId);
      Assert.Equal(_dataStubs.AccountClanInfo.ClanTag, clanInfo.Single().ClanTag);

      var clanHistory = _dbContext.AccountClanHistory.AsNoTracking().ToList();
      Assert.NotNull(clanHistory);
      Assert.Single(clanHistory);
      Assert.Equal(_dataStubs.AccountClanInfo.AccountId, clanHistory.Single().AccountId);
      Assert.Equal(_dataStubs.AccountClanInfo.ClanTag, clanHistory.Single().ClanTag);

      var accounAchievemnts = _dbContext.AccountInfoTankAchievement.AsNoTracking().ToList();
      Assert.NotNull(accounAchievemnts);
      Assert.Equal(_dataStubs.AccountInfoAchievements.Count, accounAchievemnts.Where(a => a.TankId == 0).ToList().Count);
      Assert.Equal(_dataStubs.AccountInfoTankAchievements.Count, accounAchievemnts.Where(a => a.TankId > 0).ToList().Count);


      var tanksStat = _dbContext.PresentAccountTanks.AsNoTracking()
                  .Join(_dbContext.AccountTankStatistics,
                      p => p.AccountTankStatisticId,
                      t => t.AccountTankStatisticId,
                      (p, t) => new { AccountTankStatistics = t })
                  .ToList();
      Assert.NotNull(tanksStat);
      Assert.Equal(_dataStubs.AccountTanksStatistics.Count, tanksStat.Count);

    }

    [Fact]
    public async Task TestGuestAccountGetter()
    {
      SetInitialData();
      var guestAccountInfo = new GuestAccountInfo();
      await _statisticsCollectorEngine.Collect(
          _statisticsCollectorFactory.CreateCollector(_dataStubs.AccountInfo.AccountId, guestAccountInfo));

      Assert.NotNull(guestAccountInfo.AccountInfo);
      Assert.NotNull(guestAccountInfo.AccountInfo.PlayerStatistics);
      Assert.NotNull(guestAccountInfo.AccountInfo.AccountMasteryInfo);
      Assert.Equal(4, guestAccountInfo.AccountInfo.AccountMasteryInfo.Count());
      Assert.NotNull(guestAccountInfo.AccountInfo.PlayerClanInfo);
      Assert.NotNull(guestAccountInfo.Tanks);
      Assert.Equal(_dataStubs.AccountTanksStatistics.Count, guestAccountInfo.Tanks.Count);
      Assert.NotNull(guestAccountInfo.Achievements);
      Assert.NotNull(guestAccountInfo.AggregatedAccountInfo);
    }

    [Fact]
    public void SetInitialData()
    {
      var vehicles = _dataStubs.Vehicles;
      _dbContext.VehicleEncyclopedia.AddRange(vehicles);
      _dbContext.DictionaryNation.AddRange(_dataStubs.Nations);
      _dbContext.DictionaryVehicleType.AddRange(_dataStubs.VehicleTypes);

      _dbContext.AccountInfo.Add(_dataStubs.AccountInfo);

      _dbContext.SaveChanges();

      var accounts = _dbContext.AccountInfo.AsNoTracking().ToList();
      Assert.NotNull(accounts);
      Assert.Equal(1, accounts.Count);
      Assert.Equal(_dataStubs.AccountInfo.AccountId, accounts.Single().AccountId);
      Assert.Equal(_dataStubs.AccountInfo.LastBattleTime, accounts.Single().LastBattleTime);

    }

    public void Dispose()
    {
      _dbContext.Dispose();
    }
  }
}