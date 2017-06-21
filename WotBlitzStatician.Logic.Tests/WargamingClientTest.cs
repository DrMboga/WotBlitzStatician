﻿namespace WotBlitzStatician.Logic.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using log4net;
    using WotBlitzStatician.WotApiClient;
    using Xunit;

    public class WargamingClientTest
	{
		private class TestWgApiConfiguration : IWgApiConfiguration
		{
			public string ApplicationId { get; set; } = "demo";
			public string BaseAddress { get; set; } = "https://api.wotblitz.ru/wotb/";
			public string AccountListFinderAddressTemplate { get; set; } =
				"account/list/?application_id={0}&search={1}";
			public string AccountStatRequestAddressTemplate { get; set; } =
				"account/info/?application_id={0}&account_id={1}";
			public string AccountTanksStatisticRequestAddressTemplate { get; set; } =
				"tanks/stats/?application_id={0}&account_id={1}";
			public string VehiclesEncyclopediaRequestAddressTemplate { get; set; } = "encyclopedia/vehicles/?application_id={0}";
		}

		private static readonly ILog _log = LogManager.GetLogger(typeof(WargamingClientTest));
		private readonly IWgApiConfiguration _wgApiConfiguration = new TestWgApiConfiguration();

		public WargamingClientTest()
		{
			Log4NetHelper.ConfigureLog4Net();
		}

        //[Fact(Skip = "There is something wrong with http requests.")]
        [Fact]
	public async Task TestTankopedia()
	{
		var wgApiClient = new WargamingApiClient(_wgApiConfiguration);
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
			var wgApiClient = new WargamingApiClient(_wgApiConfiguration);
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
			var wgApiClient = new WargamingApiClient(_wgApiConfiguration);
            var accountInfo = await wgApiClient.GetAccountInfoAllStatisticsAsync(46512100);

			Assert.NotNull(accountInfo);
            Assert.NotNull(accountInfo.AccountInfoStatistics);

            _log.Debug($"Got Account '{accountInfo.NickName}'. LastBattleTime '{accountInfo.LastBattleTime}'; Battles count {accountInfo.AccountInfoStatistics.Battles}");
        }

        [Fact]
        public async Task TestAccountTankStat()
        {
			var wgApiClient = new WargamingApiClient(_wgApiConfiguration);
            var tanksStat = await wgApiClient.GetTanksStatisticsAsync(46512100);

			Assert.NotNull(tanksStat);
			Assert.True(tanksStat.Count > 0, "tanksStat count is 0");

			_log.Debug($"TestAccountTankStat got {tanksStat.Count} vehicles from WG API");
			var firstTank = tanksStat[0];
            _log.Debug($"TestAccountTankStat. First tank is {firstTank.TankId}; Battles: {firstTank.Battles}; Battle life time {firstTank.BattleLifeTime}");
		}
	}
}