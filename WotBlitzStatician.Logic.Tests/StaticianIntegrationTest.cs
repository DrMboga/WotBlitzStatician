namespace WotBlitzStatician.Logic.Tests
{
    using System.Linq;
    using System.Threading.Tasks;
    using Autofac;
    using log4net;
    using WotBlitzStatician.Data;
    using WotBlitzStatician.WotApiClient;
    using Xunit;

    public class StaticianIntegrationTest
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
			public string ApplicationId { get; set; } = "demo";
			public string BaseAddress { get; set; } = "https://api.wotblitz.ru/wotb/";
			public string Language { get; set; } = "ru";
			public int DictionariesUpdateFrequencyInDays { get; set; } = 7;

			public IProxySettings ProxySettings { get; set; } = new TestProxySettings();
		}

		//private const string ConnectionString = @"Data Source=..\..\..\BlitzStatician.db";
		private const string ConnectionString = @"Data Source=/Users/mike/Developer/WotBlitzStatician/WotBlitzStatician.Logic.Tests/BlitzStatician.db";

		private static readonly ILog _log = LogManager.GetLogger(typeof(TestWgApiConfiguration));
		private readonly IContainer _container;

        public StaticianIntegrationTest()
        {
			Log4NetHelper.ConfigureLog4Net();

			var containerBuilder = new ContainerBuilder();
			containerBuilder.RegisterType<TestWgApiConfiguration>().As<IWgApiConfiguration>();
			containerBuilder.ConfigureWargamingApi();
            containerBuilder.ConfigureDataAccessor();
            containerBuilder.RegisterType<BlitzStaticianLogic>().As<IBlitzStaticianLogic>();

			_container = containerBuilder.Build();
		}

        [Fact]
        public async Task TestGettingStaticData()
        {
            var logic = _container.Resolve<IBlitzStaticianLogic>();
            await logic.LoadStaticDataAndSaveToDb();
        }

        [Fact]
        public async Task TestGetAccountByNick()
        {
            var logic = _container.Resolve<IBlitzStaticianLogic>();
            var account = await logic.GetAccount("Dr_Mboga");
            _log.Debug($"Got account {account.AccountId}. Updated at '{account.AccountInfoStatistics.Single().UpdatedAt}'");
        }

		[Fact]
		public async Task TestGetAccountData()
		{
			var logic = _container.Resolve<IBlitzStaticianLogic>();
            var account = logic.GetLastLoggedAccount();

            Assert.NotNull(account);

			_log.Debug($"Got last logged account {account.AccountId}. Last battle time '{account.LastBattleTime}'");

            await logic.LoadStatisticsFromWgAsync(account.AccountId);

			account = logic.GetLastLoggedAccount();

			Assert.NotNull(account);

            _log.Debug($"Got last logged account again {account.AccountId}. Last battle time '{account.LastBattleTime}'");

		}


	}
}