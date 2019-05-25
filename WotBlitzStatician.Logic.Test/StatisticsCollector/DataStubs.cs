using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Logic.Test.StatisticsCollector
{
  public class DataStubs
  {
    private const long AccountId = 16;

    private readonly string _fixtureFolder;
    public DataStubs(
        DateTime accountCreatedAt,
        DateTime dbLastBattleTime,
        DateTime dbAccessTokenExp,
        DateTime wgLastBattleTime
    )
    {
      // ToDo: CreateFixtures, read fixtures here and change all accountId
      // VehiclesEncyclopedia, AccountInfo, WgAccountInf + AccountInfoStatistics + frags, 
      // clanInfo, AccountAchievements, TanksStat, TanksStatAchievements
      _fixtureFolder = Path.Combine(Directory.GetCurrentDirectory(), "StatisticsCollector\\Fixtures");
      Vehicles = ReadFixture<VehicleEncyclopedia[]>("VehicleEncyclopedia.json");

      var clan = ReadFixture<AccountClanInfo>("AccountClanInfo.json");
      clan.AccountId = AccountId;

      var (accountInfoAchievemnts, accountTanksAchievements) = ReadAchievementsStat();

      AccountInfo = ReadDbAccountInfo(dbLastBattleTime, dbAccessTokenExp);
      WargamingAccountInfo = ReadWgAccountInfo(accountCreatedAt, wgLastBattleTime);
      AccountClanInfo = clan;
      AccountInfoAchievements = accountInfoAchievemnts;
      AccountInfoTankAchievements = accountTanksAchievements;
      AccountTanksStatistics = ReadAccountInfoTanks(wgLastBattleTime);
    }

    public VehicleEncyclopedia[] Vehicles { get; }

    public AccountInfo AccountInfo { get; }

    public AccountInfo WargamingAccountInfo { get; }

    public AccountClanInfo AccountClanInfo { get; }

    public List<AccountInfoAchievement> AccountInfoAchievements { get; }

    public List<AccountInfoTankAchievement> AccountInfoTankAchievements { get; }

    public List<AccountTankStatistics> AccountTanksStatistics { get; }

    public List<DictionaryNations> Nations
    {
      get
      {
        return new List<DictionaryNations> {
            new DictionaryNations {NationId = "china", NationName = "Китай" },
            new DictionaryNations {NationId = "france",NationName = "Франция" },
            new DictionaryNations {NationId = "germany", NationName = "Германия" },
            new DictionaryNations {NationId = "japan", NationName = "Япония" },
            new DictionaryNations {NationId = "other", NationName = "Сборная нация" },
            new DictionaryNations {NationId = "uk",NationName = "Великобритания" },
            new DictionaryNations {NationId = "usa", NationName = "США" },
            new DictionaryNations {NationId = "ussr",NationName = "СССР" },
          };
      }
    }

    public List<DictionaryVehicleType> VehicleTypes
    {
      get
      {
        return new List<DictionaryVehicleType>
        {
            new DictionaryVehicleType {VehicleTypeId = "AT-SPG", VehicleTypeName = "ПТ-САУ"},
            new DictionaryVehicleType {VehicleTypeId = "heavyTank", VehicleTypeName = "Тяжёлый танк"},
            new DictionaryVehicleType {VehicleTypeId = "lightTank", VehicleTypeName = "Легкий танк"},
            new DictionaryVehicleType {VehicleTypeId = "mediumTank", VehicleTypeName = "Средний танк"}
        };
      }
    }


    private AccountInfo ReadDbAccountInfo(
        DateTime dbLastBattleTime,
        DateTime dbAccessTokenExp
    )
    {
      var accountInfo = ReadFixture<AccountInfo>("DbAccountInfo.json");
      accountInfo.AccountId = AccountId;
      accountInfo.LastBattleTime = dbLastBattleTime;
      accountInfo.AccessTokenExpiration = dbAccessTokenExp;
      return accountInfo;
    }

    private AccountInfo ReadWgAccountInfo(
        DateTime accountCreatedAt,
        DateTime wgLastBattleTime
    )
    {
      var wgAccountInfo = ReadFixture<AccountInfo>("WgAccountInfo.json");
      wgAccountInfo.AccountId = AccountId;
      wgAccountInfo.AccountCreatedAt = accountCreatedAt;
      wgAccountInfo.LastBattleTime = wgLastBattleTime;
      var stat = wgAccountInfo.AccountInfoStatistics.Single();
      stat.AccountId = AccountId;
      stat.UpdatedAt = wgLastBattleTime.AddMinutes(5);
      stat.FragsList.ForEach(f => f.AccountId = AccountId);
      return wgAccountInfo;
    }

    private (List<AccountInfoAchievement>, List<AccountInfoTankAchievement>) ReadAchievementsStat()
    {
      var allAchevements = ReadFixture<List<AccountInfoTankAchievement>>("AccountInfoAchievements.json");
      allAchevements.ForEach(a => a.AccountId = AccountId);
      int accountAchievementsCount = allAchevements.Where(a => a.TankId == 0).Count();
      var accountInfoAchievements = new List<AccountInfoAchievement>();
      for (int i = 0; i < accountAchievementsCount; i++)
      {
        accountInfoAchievements.Add(allAchevements[i] as AccountInfoAchievement);
      }
      var tanksInfoAchivements = allAchevements.Where(a => a.TankId != 0).ToList();
      return (accountInfoAchievements, tanksInfoAchivements);
    }

    private List<AccountTankStatistics> ReadAccountInfoTanks(DateTime wgLastBattleTime)
    {
      var accountTanks = ReadFixture<List<AccountTankStatistics>>("AccountTanksStatistics.json");
      foreach (var accountTank in accountTanks)
      {
        accountTank.AccountId = AccountId;
        accountTank.Wn7 = 0;
        accountTank.LastBattleTime = wgLastBattleTime;
      }
      return accountTanks;
    }

    private T ReadFixture<T>(string fixtureFileName) where T : class
    {
      string fixturePath = Path.Combine(_fixtureFolder, fixtureFileName);
      if (!File.Exists(fixturePath))
      {
        throw new ApplicationException($"Fixture doesn't exist '{fixtureFileName}'");
      }
      string fixtureJson = File.ReadAllText(fixturePath);
      return JsonConvert.DeserializeObject<T>(fixtureJson);
    }
  }
}