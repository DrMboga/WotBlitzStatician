namespace WotBlitzStatician.Logic.Tests
{
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
	}
}