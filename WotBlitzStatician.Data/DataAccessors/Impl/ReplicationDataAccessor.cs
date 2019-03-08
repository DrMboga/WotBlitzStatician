using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.DataAccessors.Impl
{
  public class ReplicationDataAccessor : IReplicationDataAccessor
  {
    private ILogger<ReplicationDataAccessor> _logger;
    private readonly BlitzStaticianDbContext _dbContext;

    public ReplicationDataAccessor(BlitzStaticianDbContext dbContext,
        ILogger<ReplicationDataAccessor> logger)
    {
      _logger = logger;
      _dbContext = dbContext;
    }

    public ReplicationData GetDatabase()
    {
      var replicationData = new ReplicationData();
      replicationData.AccountInfo = _dbContext.AccountInfo.AsNoTracking().ToList();
      replicationData.AccountInfo.ForEach(i =>
      {
        i.AccessToken = string.Empty;
        i.AccessTokenExpiration = null;
      });

      replicationData.AccountInfoStatistics = _dbContext.AccountInfoStatistics.AsNoTracking().ToList();
      replicationData.AccountClanHistory = _dbContext.AccountClanHistory.AsNoTracking().ToList();
      replicationData.AccountClanInfo = _dbContext.AccountClanInfo.AsNoTracking().ToList();
      replicationData.AccountInfoAcievements = _dbContext.AccountInfoAchievement.AsNoTracking().ToList();
      replicationData.AccountTanksStatistics = _dbContext.AccountTankStatistics.AsNoTracking().ToList();
      replicationData.PresentAccountTanks = _dbContext.PresentAccountTanks.AsNoTracking().ToList();
      replicationData.Frags = _dbContext.Frags.AsNoTracking().ToList();

      return replicationData;
    }

    public void SetDatabase(ReplicationData replicationData)
    {
      if (_dbContext.AccountInfo.AsNoTracking().Count() != 0)
      {
        throw new ApplicationException("Database is not empty");
      }

      var exPresentAccountTanks = new List<AccountTankStatistics>();

      replicationData.AccountInfoStatistics.ForEach(s => s.AccountInfoStatisticsId = 0);
      replicationData.AccountClanHistory.ForEach(h => h.AccountClanHistoryId = 0);
      replicationData.AccountClanInfo.ForEach(c => c.AccountClanInfoId = 0);
      replicationData.AccountInfoAcievements.ForEach(c => c.AccountInfoAchievementId = 0);
      replicationData.Frags.ForEach(f => f.FragListItemId = 0);
      replicationData.PresentAccountTanks.ForEach(t =>
      {
        t.PresentAccountTankId = 0;
        var stat = replicationData.AccountTanksStatistics.First(s => s.AccountTankStatisticId == t.AccountTankStatisticId);
        exPresentAccountTanks.Add(new AccountTankStatistics
        {
          AccountTankStatisticId = stat.AccountTankStatisticId,
          AccountId = stat.AccountId,
          TankId = stat.TankId,
          BattleLifeTimeInSeconds = stat.BattleLifeTimeInSeconds
        });
      });
      replicationData.AccountTanksStatistics.ForEach(s => s.AccountTankStatisticId = 0);



      try
      {
        _dbContext.Database.BeginTransaction();
        _dbContext.AccountInfo.AddRange(replicationData.AccountInfo);
        _dbContext.AccountInfoStatistics.AddRange(replicationData.AccountInfoStatistics);
        _dbContext.AccountClanHistory.AddRange(replicationData.AccountClanHistory);
        _dbContext.AccountClanInfo.AddRange(replicationData.AccountClanInfo);
        _dbContext.AccountInfoAchievement.AddRange(replicationData.AccountInfoAcievements);
        _dbContext.Frags.AddRange(replicationData.Frags);
        _dbContext.AccountTankStatistics.AddRange(replicationData.AccountTanksStatistics);

        _dbContext.SaveChanges();

        foreach (var presentTank in replicationData.PresentAccountTanks)
        {
          var exAccountTankStat = exPresentAccountTanks
                .First(t => t.AccountTankStatisticId == presentTank.AccountTankStatisticId);
          presentTank.AccountTankStatisticId = _dbContext.AccountTankStatistics
                .Where(t => t.AccountId == exAccountTankStat.AccountId && t.TankId == exAccountTankStat.TankId && t.BattleLifeTimeInSeconds == exAccountTankStat.BattleLifeTimeInSeconds)
                .Select(t => t.AccountTankStatisticId)
                .First();
          _dbContext.PresentAccountTanks.Add(presentTank);
        }

        _dbContext.SaveChanges();

        _dbContext.Database.CommitTransaction();
      }
      catch (Exception e)
      {
        _logger.LogError(e, "Cannot update database");
        _dbContext.Database.RollbackTransaction();
      }
    }
  }
}