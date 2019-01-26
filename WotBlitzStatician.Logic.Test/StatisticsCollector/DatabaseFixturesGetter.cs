using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using WotBlitzStatician.Data;
using Xunit;

namespace WotBlitzStatician.Logic.Test.StatisticsCollector
{
  public class DatabaseFixturesGetter : IDisposable
  {
    private readonly BlitzStaticianDbContext _dbContext;

    public DatabaseFixturesGetter()
    {
      string connectionString = "Data Source=192.168.1.85,1438;Initial Catalog=BlitzStatician;Integrated Security=False;User Id=SA;Password=DevSql123!";
      var builder = new DbContextOptionsBuilder<BlitzStaticianDbContext>();
      builder.UseSqlServer(connectionString);
      _dbContext = new BlitzStaticianDbContext(builder.Options);
    }

    public void Dispose()
    {
      _dbContext.Dispose();
    }

    [Fact]
    public void GetVehicles()
    {
      var vehicles = _dbContext.VehicleEncyclopedia.AsNoTracking().ToList();
      string serialized = JsonConvert.SerializeObject(vehicles);
    }

    [Fact]
    public void GetAccountInfo()
    {
      var account = _dbContext.AccountInfo.AsNoTracking().Where(a => a.AccountId == 90277267).Single();
      string accountStr = JsonConvert.SerializeObject(account);
    }

    [Fact]
    public void GetAccountWithStatAndFrags()
    {
      var account = _dbContext.AccountInfo.AsNoTracking().Where(a => a.AccountId == 90277267).Single();
      account.AccountInfoStatistics = _dbContext.AccountInfoStatistics.AsNoTracking()
                  .Where(s => s.AccountInfoStatisticsId == 10005)
                  .ToList();
      account.AccountInfoStatistics.Single().FragsList =
          _dbContext.Frags.AsNoTracking()
              .Where(f => f.AccountId == 90277267 && f.TankId == null)
              .ToList();

      string accountStr = JsonConvert.SerializeObject(account);
    }

    [Fact]
    public void GetAccountClanInfo()
    {
      var clan = _dbContext.AccountClanInfo.AsNoTracking()
          .Where(c => c.AccountId == 90277267).SingleOrDefault();

      string clanStr = JsonConvert.SerializeObject(clan);

    }

    [Fact]
    public void GetAccountInfoAchievements()
    {
      var aAchievements = _dbContext.AccountInfoAchievement.AsNoTracking()
                              .Where(a => a.AccountId == 90277267)
                              .ToList();
      string str = JsonConvert.SerializeObject(aAchievements);
    }

    [Fact]
    public void GetAccountTanks()
    {
      var accountTanks = _dbContext.PresentAccountTanks.AsNoTracking()
                          .Join(_dbContext.AccountTankStatistics,
                              p => p.AccountTankStatisticId,
                              t => t.AccountTankStatisticId,
                              (p, t) => new { p.AccountId, AccountTankStatistic = t })
                          .Where(j => j.AccountId == 90277267)
                          .Select(j => j.AccountTankStatistic)
                          .ToList();
      var frags = _dbContext.Frags.AsNoTracking()
                      .Where(f => f.AccountId == 90277267 && f.TankId != null)
                      .ToList();

      accountTanks.ForEach(t => t.FragsList = frags.Where(f => f.TankId == t.TankId).ToList());
      string str = JsonConvert.SerializeObject(accountTanks);

    }
  }
}