namespace WotBlitzStatician.Logic.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
	using Autofac;
    using log4net;
    using WotBlitzStatician.WotApiClient;
    using Xunit;

    public class WargamingClientTest
    {
		private class TestProxySettings : IProxySettings
		{
			public bool UseProxy { get; set; } = false;
			public string Domain { get; set; }
			public string User { get; set; }
			public string PwdHash { get; set; }
		}

        private class TestWgApiConfiguration : IWgApiConfiguration
        {
            public string ApplicationId { get; set; } = "adc1387489cf9fc8d9a1d85dbd27763d";
            public string BaseAddress { get; set; } = "https://api.wotblitz.ru/wotb/";
            public string Language { get; set; } = "ru";
            public Int32 DictionariesUpdateFrequencyInDays { get; set; } = 7;

	        public IProxySettings ProxySettings { get; set; } = new TestProxySettings();
        }

		private const string AccessToken = "c473efefe668be30dbca625562f637b9798aac43";
        private static readonly ILog _log = LogManager.GetLogger(typeof(WargamingClientTest));
        private readonly IContainer _container;

        public WargamingClientTest()
        {
            Log4NetHelper.ConfigureLog4Net();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<TestWgApiConfiguration>().As<IWgApiConfiguration>();
            containerBuilder.ConfigureWargamingApi();

            _container = containerBuilder.Build();
        }

        //[Fact(Skip = "There is something wrong with http requests.")]
        [Fact]
        public async Task TestTankopedia()
        {
            var wgApiClient = _container.Resolve<IWargamingApiClient>();
            var allVehicles = await wgApiClient.GetWotEncyclopediaVehiclesAsync();

            Assert.NotNull(allVehicles);
            Assert.True(allVehicles.Count > 0, "Vehicles count is 0");

            _log.Debug($"TestTankopedia got {allVehicles.Count} vehicles from WG API");
            var firstTank = allVehicles[0];
            _log.Debug($"TestTankopedia. First tank is {firstTank.TankId} - '{firstTank.Name}'; Tier {firstTank.Tier}; Nation '{firstTank.Nation}'; Type '{firstTank.Type}'");
        }

        [Fact]
        public async Task TestFindAccount()
        {
            string accountNick = "1Tortee1";
            var wgApiClient = _container.Resolve<IWargamingApiClient>();
            var accountsFound = await wgApiClient.FindAccountAsync(accountNick);

            Assert.NotNull(accountsFound);
            Assert.True(accountsFound.Count > 0, "Found 0 accounts");

            _log.Debug($"TestFindAccount found {accountsFound.Count} accounts like '{accountNick}'.");

            // Find exactly the same nick
            var account = accountsFound.FirstOrDefault(a => a != null && a.NickName.Equals(accountNick, StringComparison.OrdinalIgnoreCase));
            Assert.NotNull(account);

            _log.Debug($"Found account is '{account.NickName}'; Id: '{account.AccountId}'");

        }

        [Fact]
        public async Task TestAccountStat()
        {
            var wgApiClient = _container.Resolve<IWargamingApiClient>();
            var accountInfo = await wgApiClient.GetAccountInfoAllStatisticsAsync(46512100, AccessToken, true);

            Assert.NotNull(accountInfo);
            Assert.NotNull(accountInfo.AccountInfoStatistics);

            _log.Debug($"Got Account '{accountInfo.NickName}'. LastBattleTime '{accountInfo.LastBattleTime}'; Battles count {accountInfo.AccountInfoStatistics.Battles}");
        }

        [Fact]
        public async Task TestAccountTankStat()
        {
            var wgApiClient = _container.Resolve<IWargamingApiClient>();
            var tanksStat = await wgApiClient.GetTanksStatisticsAsync(46512100);

            Assert.NotNull(tanksStat);
            Assert.True(tanksStat.Count > 0, "tanksStat count is 0");

            _log.Debug($"TestAccountTankStat got {tanksStat.Count} vehicles from WG API");
            var firstTank = tanksStat[0];
            _log.Debug($"TestAccountTankStat. First tank is {firstTank.TankId}; Battles: {firstTank.Battles}; Battle life time {firstTank.BattleLifeTime}");
        }

        [Fact]
        public async Task TestStaticDictionary()
        {
            var wgApiClient = _container.Resolve<IWargamingApiClient>();
            var staticDictionaies = await wgApiClient.GetStaticDictionariesAsync();

            Assert.NotNull(staticDictionaies);
            Assert.NotNull(staticDictionaies.DictionaryLanguages);
            Assert.NotNull(staticDictionaies.DictionaryNationses);
            Assert.NotNull(staticDictionaies.DictionaryVehicleTypes);

            _log.Debug($"DictionaryLanguages count is {staticDictionaies.DictionaryLanguages.Count()}");
            _log.Debug($"DictionaryLanguages first Item '{staticDictionaies.DictionaryLanguages[0].LanguageId}' - '{staticDictionaies.DictionaryLanguages[0].LanguageName}'");

			_log.Debug($"DictionaryNationses count is {staticDictionaies.DictionaryNationses.Count()}");
            _log.Debug($"DictionaryNationses first Item '{staticDictionaies.DictionaryNationses[0].NationId}' - '{staticDictionaies.DictionaryNationses[0].NationName}'");

			_log.Debug($"DictionaryVehicleTypes count is {staticDictionaies.DictionaryVehicleTypes.Count()}");
            _log.Debug($"DictionaryVehicleTypes first Item '{staticDictionaies.DictionaryVehicleTypes[0].VehicleTypeId}' - '{staticDictionaies.DictionaryVehicleTypes[0].VehicleTypeName}'");
        }

		// dotnet test --filter FullyQualifiedName=WotBlitzStatician.Logic.Tests.WargamingClientTest.TestClanInfo
		[Fact]
        public async Task TestClanInfo()
        {
            var wgApiClient = _container.Resolve<IWargamingApiClient>();
            var accountClanInfo = await wgApiClient.GetAccountClanInfoAsync(46512100);

            Assert.NotNull(accountClanInfo);

            _log.Debug($"Got clan info for accountId {accountClanInfo.AccountId} - {accountClanInfo.PlayerRole};" 
                       + $" joined at {accountClanInfo.PlayerJoinedAt}; clan tag {accountClanInfo.ClanTag};" 
                       + $" members count {accountClanInfo.MembersCount}; leader is '{accountClanInfo.ClanLeaderName}'"
                       + $" clan id {accountClanInfo.ClanId}");
        }

        [Fact]
        public async Task TestAchievemntsDictionary()
        {
            var wgApiClient = _container.Resolve<IWargamingApiClient>();
            var achievements = await wgApiClient.GetAchievementsDictionaryAsync();

            Assert.NotNull(achievements);

            _log.Debug($"<TestAchievemntsDictionary> Got {achievements.Count} achievement Dictionaries");

            var firstWithNotNullOptions = achievements.First(a => a.Options != null);

            Assert.True(firstWithNotNullOptions.Options.All(o => !string.IsNullOrWhiteSpace(o.AchievementId)));

            _log.Debug($"First achievement with option is: '{firstWithNotNullOptions.AchievementId}' - "
                + $"'{firstWithNotNullOptions.Name}'; Description '{firstWithNotNullOptions.Description}';"
                + $"Order {firstWithNotNullOptions.Order}; Section: '{firstWithNotNullOptions.Section}'"
                + $"FirstOption: '{firstWithNotNullOptions.Options.First().Name}'");
        }	
	    

	[Fact]
	    public async Task TestAccountAchievements()
	    {
			var wgApiClient = _container.Resolve<IWargamingApiClient>();
		    var accountAchievements = await wgApiClient.GetAccountAchievementsAsync(46512100);

		    var firstAchievement = accountAchievements.First(a => !a.IsMaxSeries);
		    var firstMaxSeries = accountAchievements.First(a => a.IsMaxSeries);

			_log.Debug($"<TestAccountAchievements> Got {accountAchievements.Count} achievement for account. "
				+ $"First achievement is '{firstAchievement.AchievementId}'; Count {firstAchievement.Count}. "
				+ $"First max series is '{firstMaxSeries.AchievementId}'; Count {firstMaxSeries.Count}");
		}


		[Fact]
	    public async Task TestAccountTankAchievements()
	    {
			var wgApiClient = _container.Resolve<IWargamingApiClient>();
		    var accountTankAchievements = await wgApiClient.GetAccountTankAchievementsAsync(46512100);

		    var firstTank = accountTankAchievements.GroupBy(t => t.TankId, (key, a) => new {key, Achievements = a.ToList()}).First();

			var firstAchievement = firstTank.Achievements.First(a => !a.IsMaxSeries);
		    var firstMaxSeries = firstTank.Achievements.First(a => a.IsMaxSeries);

			_log.Debug($"<TestAccountTankAchievements> Got {accountTankAchievements.Count} achievement for all tanks in account. "
				+ $"First tank is {firstTank.key} has {firstTank.Achievements.Count} acievements. "
				+ $"First achievement for first tank is '{firstAchievement.AchievementId}'; Count {firstAchievement.Count}. "
				+ $"First max series for first tank is '{firstMaxSeries.AchievementId}'; Count {firstMaxSeries.Count}");
		}
    }
}
