using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WotBlitzStatician.Model;
using WotBlitzStatician.Model.Dto;

namespace WotBlitzStatician.Data.DataAccessors.Impl
{
    public class AccountInfoViewDataAccessor : IAccountInfoViewDataAccessor
    {
        private readonly BlitzStaticianDbContext _dbContext;
        private readonly IQueryableMapper<AccountInfoStatistics, PlayerStatDto> _playerStatMapper;

        public AccountInfoViewDataAccessor(
                    BlitzStaticianDbContext dbContext,
                    IQueryableMapper<AccountInfoStatistics, PlayerStatDto> playerStatMapper

        )
        {
            _dbContext = dbContext;
            _playerStatMapper = playerStatMapper;
        }


        public async Task<AccountInfoDto> GetActualAccountInfo(long accountId)
        {
            var accountInfo = await _dbContext.AccountInfo.AsNoTracking()
                .Where(a => a.AccountId == accountId)
                .Select(a => new AccountInfoDto
                {
                    AccountId = a.AccountId,
                    NickName = a.NickName,
                    AccountCreatedAt = a.AccountCreatedAt.Value,
                    LastBattleTime = a.LastBattleTime.Value
                })
                .FirstOrDefaultAsync();

            if (accountInfo == null)
            {
                return null;
            }

            var statistics = await _playerStatMapper.ProjectTo(
             _dbContext.AccountInfoStatistics
                .OrderByDescending(s => s.UpdatedAt)
                .Where(s => s.AccountId == accountId)
                .Take(1))
                .FirstOrDefaultAsync();

            accountInfo.PlayerStatistics = statistics;

            return accountInfo;
        }

        public async Task<List<PlayerStatHistoryDto>> GetAccountStatHistory(long accountId, DateTime dateFrom)
        {
            string achievementsSql =
              @"-- Account Stat history v1
WITH AccountHistory AS
(
  SELECT ROW_NUMBER() OVER(ORDER BY ais.UpdatedAt DESC) AS RowNumber,
    ais.UpdatedAt, 
    ais.Battles, 
    ais.Wins, 
    ais.AvgTier, 
    ais.Wn7,
    CONVERT(decimal(19,2), ais.Wins)/CONVERT(decimal(19,2), ais.Battles)WinRate,
      ais.DamageDealt/ais.Battles AvgDamage,
      ais.Xp/ais.Battles AvgXp,
      CONVERT(decimal(19,2), ais.SurvivedBattles)/CONVERT(decimal(19,2), ais.Battles) SurvivalRate
  FROM wotb.AccountInfoStatistics AS ais
  WHERE ais.AccountId = @accountId
    AND ais.UpdatedAt >= @dateFrom
)

SELECT  
  cur.UpdatedAt, 
  cur.Battles,
  cur.Battles - prev.Battles BattlesDiff,
  cur.Wins,
  CONVERT(decimal(19,2), cur.AvgTier) AvgTier,
  CONVERT(decimal(19,2), cur.AvgTier - prev.AvgTier) AvgTierDiff,
  CONVERT(decimal(19,2), cur.Wn7) Wn7,
  CONVERT(decimal(19,2), cur.Wn7 - prev.Wn7) Wn7Diff,
  cur.WinRate,
  cur.WinRate - prev.WinRate WinRateDiff,
  cur.AvgDamage,
  cur.AvgDamage - prev.AvgDamage AvgDamageDiff,
  cur.AvgXp,
  cur.AvgXp - prev.AvgXp AvgXpDiff,
  cur.SurvivalRate,
  cur.SurvivalRate - prev.SurvivalRate SurvivalRateDiff
FROM AccountHistory AS cur
  LEFT JOIN AccountHistory AS prev ON cur.RowNumber = prev.RowNumber - 1
";

            var accountIdParameter = new SqlParameter("@accountId", accountId);
            var dateFromParameter = new SqlParameter("@dateFrom", dateFrom);
            return await _dbContext.Query<PlayerStatHistoryDto>()
              .FromSql(achievementsSql, accountIdParameter, dateFromParameter)
              .ToListAsync();
        }

        public async Task<string> GetAccountAccessToken(long accountId)
        {
            return await _dbContext.AccountInfo.AsNoTracking()
                    .Where(a => a.AccountId == accountId)
                    .Select(a => a.AccessToken)
                    .FirstOrDefaultAsync();
        }
    }
}