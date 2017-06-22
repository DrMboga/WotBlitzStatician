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
        private class TestWgApiConfiguration : IWgApiConfiguration
        {
            public string ApplicationId { get; set; } = "demo";
            public string BaseAddress { get; set; } = "https://api.wotblitz.ru/wotb/";
            public string Language { get; set; } = "ru";
        }

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
            var accountInfo = await wgApiClient.GetAccountInfoAllStatisticsAsync(46512100);

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
    }
}
