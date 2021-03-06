﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WotBlitzStatician.Model;
using WotBlitzStatician.Model.Dto;
using WotBlitzStatician.Model.MapperLogic;

namespace WotBlitzStatician.Data.DataAccessors.Impl
{
  public class AccountDataAccessor : IAccountDataAccessor
  {
    private readonly BlitzStaticianDbContext _dbContext;

    #pragma warning disable CS0618
    [Obsolete("Using in tests only")]
    private readonly IDbContextTransaction _dummyTransaction;

    [Obsolete("For tests only! Becuse in-memory database doesn't support transactions, we used to use this constructor in tests.")]
    public AccountDataAccessor(BlitzStaticianDbContext dbContext,
        IDbContextTransaction tran)
    {
      _dbContext = dbContext;
      _dummyTransaction = tran;
    }
    #pragma warning restore CS0618

    public AccountDataAccessor(BlitzStaticianDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<List<AccountInfo>> GetAllAccountsAsync()
    {
      return await _dbContext.AccountInfo.AsNoTracking()
          .OrderByDescending(a => a.LastBattleTime)
          .ToListAsync();
    }

    public async Task<List<AccountTankStatistics>> GetActualTanksAsync(long accountId)
    {
      var tanks = await _dbContext.PresentAccountTanks.AsNoTracking()
          .Join(_dbContext.AccountTankStatistics.AsNoTracking(),
              p => p.AccountTankStatisticId,
              s => s.AccountTankStatisticId,
              (pt, st) => new { pt.AccountId, TankStatistics = st }
          )
          .Where(p => p.AccountId == accountId)
          .Select(q => q.TankStatistics)
          .ToListAsync();

      return tanks;
    }

    public async Task SaveProlongedAccountAsync(long accountId, string accessToken, DateTime accesTokenExpiration)
    {
      var accountInfo = await _dbContext.AccountInfo
          .SingleAsync(a => a.AccountId == accountId);
      accountInfo.AccessToken = accessToken;
      accountInfo.AccessTokenExpiration = accesTokenExpiration;
      await _dbContext.SaveChangesAsync();
    }

    public async Task InsertAccountAuthInfoAsync(AccountInfo accountInfo)
    {
      await _dbContext.AccountInfo.AddAsync(accountInfo);
      await _dbContext.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> OpenTransactionAsync()
    {
      #pragma warning disable CS0618
      if (_dummyTransaction != null)
      {
        return _dummyTransaction;
      }
      #pragma warning disable CS0618

      return await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task SaveLastBattleInfoAsync(AccountInfo accountInfo)
    {
      var accountFromDb = await _dbContext.AccountInfo
          .SingleAsync(a => a.AccountId == accountInfo.AccountId);
      accountFromDb.AccountCreatedAt = accountInfo.AccountCreatedAt;
      accountFromDb.LastBattleTime = accountInfo.LastBattleTime;
      await _dbContext.SaveChangesAsync();
    }

    public async Task SaveAccountStatisticsAsync(AccountInfoStatistics accountInfoStatistics)
    {
      await _dbContext.AccountInfoStatistics.AddAsync(accountInfoStatistics);
      await _dbContext.SaveChangesAsync();
    }

    public async Task MergeFragsAsync(List<FragListItem> frags)
    {
      if (frags == null || frags.Count == 0)
      {
        return;
      }

      var accountId = frags.First().AccountId;

      bool fragsByAccount = frags.All(f => f.TankId == null);

      List<FragListItem> existingFrags = null;
      if (fragsByAccount)
      {
        existingFrags = await _dbContext.Frags
            .Where(f => f.AccountId == accountId && f.TankId == null)
            .ToListAsync();
      }
      else
      {
        existingFrags = await _dbContext.Frags
            .Where(f => f.AccountId == accountId && f.TankId != null)
            .ToListAsync();
      }

      foreach (var frag in frags)
      {
        var existing = existingFrags.FirstOrDefault(f =>
                        f.KilledTankId == frag.KilledTankId &&
                        ((fragsByAccount) || (!fragsByAccount && f.TankId == frag.TankId)));
        if (existing == null)
        {
          await _dbContext.Frags.AddAsync(frag);
        }
        else
        {
          existing.FragsCount = frag.FragsCount;
        }
      }
      await _dbContext.SaveChangesAsync();
    }

    public async Task SaveAccountClanInfoAsync(long accountId, AccountClanInfo accountClanInfo)
    {
      var existingClanInfo = await _dbContext.AccountClanInfo
          .FirstOrDefaultAsync(c => c.AccountId == accountId);
      if (accountClanInfo == null || accountClanInfo.ClanId == 0)
      {
        // Account not in clan - delete clan info from DB
        if (existingClanInfo != null)
        {
          _dbContext.Entry(existingClanInfo).State = EntityState.Deleted;
          await _dbContext.SaveChangesAsync();
        }
        return;
      }

      if (existingClanInfo == null)
      {
        // Insert new clan info
        await _dbContext.AccountClanInfo.AddAsync(accountClanInfo);
        await _dbContext.SaveChangesAsync();
        return;
      }

      // Update clan info
      _dbContext.Entry(existingClanInfo).State = EntityState.Detached;
      accountClanInfo.AccountClanInfoId = existingClanInfo.AccountClanInfoId;
      _dbContext.AccountClanInfo.Attach(accountClanInfo);
      _dbContext.Entry(accountClanInfo).State = EntityState.Modified;
      await _dbContext.SaveChangesAsync();
    }

    public async Task SaveAccountClanHistoryAsync(AccountClanHistory accountClanHistory)
    {
      await _dbContext.AccountClanHistory.AddAsync(accountClanHistory);
      await _dbContext.SaveChangesAsync();
    }

    public async Task MergeAccountAchievementsAsync(long accountId, List<AccountInfoAchievement> achievements, List<AccountInfoAchievement> achievementsMaxSeries)
    {
      if (achievements == null || achievements.Count == 0)
      {
        return;
      }
      var existingAchevements = await _dbContext.AccountInfoAchievement
          .Where(a => a.AccountId == accountId)
          .ToListAsync();

      foreach (var achievement in achievements)
      {
        var existing = existingAchevements
            .FirstOrDefault(e => e.AchievementId == achievement.AchievementId && e.IsMaxSeries == false);
        if (existing == null)
        {
          await _dbContext.AccountInfoAchievement.AddAsync(achievement);
        }
        else
        {
          existing.Count = achievement.Count;
        }
      }
      if (achievementsMaxSeries != null)
      {
        foreach (var achievement in achievementsMaxSeries)
        {
          var existing = existingAchevements
              .FirstOrDefault(e => e.AchievementId == achievement.AchievementId && e.IsMaxSeries == true);
          if (existing == null)
          {
            await _dbContext.AccountInfoAchievement.AddAsync(achievement);
          }
          else
          {
            existing.Count = achievement.Count;
          }
        }
      }
      await _dbContext.SaveChangesAsync();
    }

    public async Task SaveTankStatisticsAsync(List<AccountTankStatistics> tankStatistics)
    {
      await _dbContext.AccountTankStatistics.AddRangeAsync(tankStatistics);
      await _dbContext.SaveChangesAsync();
    }

    public async Task MergeAccountInfoTankAchievementsAsync(List<AccountTankStatistics> tanks)
    {
      long accountId = tanks.First().AccountId;
      var tankIds = tanks.Select(t => t.TankId).ToList();
      var existingTankAchievements = await _dbContext.AccountInfoTankAchievement
          .Where(a => a.AccountId == accountId && tankIds.Contains(a.TankId))
          .ToListAsync();
      foreach (var tank in tanks)
      {
        foreach (var achievement in tank.Achievements)
        {
          var existing = existingTankAchievements
              .FirstOrDefault(e => e.AchievementId == achievement.AchievementId
                                      && e.TankId == tank.TankId
                                      && e.IsMaxSeries == false);
          if (existing == null)
          {
            await _dbContext.AccountInfoTankAchievement.AddAsync(achievement);
          }
          else
          {
            existing.Count = achievement.Count;
          }
        }
        if (tank.AchievementsMaxSeries != null)
        {
          foreach (var achievement in tank.AchievementsMaxSeries)
          {
            var existing = existingTankAchievements
                .FirstOrDefault(e => e.AchievementId == achievement.AchievementId
                                    && e.TankId == tank.TankId
                                    && e.IsMaxSeries == true);
            if (existing == null)
            {
              await _dbContext.AccountInfoTankAchievement.AddAsync(achievement);
            }
            else
            {
              existing.Count = achievement.Count;
            }
          }
        }
      }
      await _dbContext.SaveChangesAsync();
    }

    public async Task MergePresentAccountTanksInfoAsync(List<PresentAccountTanks> presentAccountTanks)
    {
      long accountId = presentAccountTanks.First().AccountId;
      var tankIds = presentAccountTanks.Select(pt => pt.TankId).ToList();
      var exitingList = await _dbContext.PresentAccountTanks.
          Where(pat => pat.AccountId == accountId
                      && tankIds.Contains(pat.TankId))
          .ToListAsync();
      foreach (var presentAccountTank in presentAccountTanks)
      {
        var existing = exitingList
                        .FirstOrDefault(e => e.TankId == presentAccountTank.TankId
                                            && e.AccountId == presentAccountTank.AccountId);

        if (existing == null)
        {
          _dbContext.PresentAccountTanks.Add(presentAccountTank);
        }
        else
        {
          existing.AccountTankStatisticId = presentAccountTank.AccountTankStatisticId;
        }
      }

      await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateInnGarageInfoAsync(List<AccountTankStatistics> tanks)
    {
      var tankStatIds = tanks.Select(pt => pt.AccountTankStatisticId).ToList();
      var exitingList = await _dbContext.AccountTankStatistics.
          Where(t => tankStatIds.Contains(t.AccountTankStatisticId))
          .ToListAsync();
      foreach (var existing in exitingList)
      {
        var tank = tanks.First(t => t.AccountTankStatisticId == existing.AccountTankStatisticId);
        existing.InGarage = tank.InGarage;
        existing.InGarageUpdated = tank.InGarageUpdated;
      }

      await _dbContext.SaveChangesAsync();
    }

    public async Task<AccountInfo> GetShortAccountInfo(long accountId)
    {
      return await _dbContext.AccountInfo.AsNoTracking()
          .Where(a => a.AccountId == accountId)
          .FirstOrDefaultAsync();
    }

  }
}
