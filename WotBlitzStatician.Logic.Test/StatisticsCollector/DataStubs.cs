using System;
using System.Collections.Generic;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Logic.Test.StatisticsCollector
{
    public class DataStubs
    {
        private const long AccountId = 16;
        private readonly AccountInfo _accountInfo = new AccountInfo
        {
            AccountId = AccountId,
            NickName = "TestAccountNick",
            LastBattleTime = DateTime.Now.AddHours(-2),
            AccessToken = "TestAccessToken",
            AccessTokenExpiration = DateTime.Now.AddDays(1)
        };

        private readonly AccountInfo _wgAccountInfo = new AccountInfo
        {
            AccountId = AccountId,
            NickName = "TestAccountNick",
            LastBattleTime = DateTime.Now.AddHours(-1),
            AccountCreatedAt = DateTime.Now.AddMonths(-6)
        };

        private readonly AccountInfoStatistics _accountInfoStatistics = new AccountInfoStatistics
        {
            AccountId = AccountId,
            Battles = 5000,
            UpdatedAt = DateTime.Now.AddHours(-2),
            CapturePoints = 2100,
            DamageDealt = 6000000,
            DamageReceived = 5000000,
            DroppedCapturePoints = 4000,
            Frags = 4500,
            Frags8P = 1500,
            Hits = 40000,
            Losses = 2000,
            MaxFrags = 6,
            MaxFragsTankId = 58881,
            MaxXp = 2000,
            MaxXpTankId = 6657,
            Shots = 45000,
            Spotted = 1500,
            SurvivedBattles = 1000,
            WinAndSurvived = 800,
            Wins = 2950,
            Xp = 3500000,
            FragsList = new List<FragListItem>
            {
                new FragListItem {KilledTankId = 58881, FragsCount = 66, AccountId = AccountId},
                new FragListItem {KilledTankId = 6657, FragsCount = 62, AccountId = AccountId}
            }
        };

        public DataStubs()
        {
            AccountInfo = _accountInfo;
            WargamingAccountInfo = _wgAccountInfo;
            WargamingAccountInfo.AccountInfoStatistics = new List<AccountInfoStatistics> { _accountInfoStatistics };
        }
        public AccountInfo AccountInfo { get; }

        public AccountInfo WargamingAccountInfo { get; }

    }
}