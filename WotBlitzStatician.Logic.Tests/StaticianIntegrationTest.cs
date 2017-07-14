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
		private class TestWgApiConfiguration : IWgApiConfiguration
		{
			public string ApplicationId { get; set; } = "demo";
			public string BaseAddress { get; set; } = "https://api.wotblitz.ru/wotb/";
			public string Language { get; set; } = "ru";
			public int DictionariesUpdateFrequencyInDays { get; set; } = 7;
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
            containerBuilder.ConfigureDataAccessor(ConnectionString);
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
            _log.Debug($"Got account {account.AccountId}. Updated at '{account.AccountInfoStatistics.UpdatedAt}'");
        }

        [Fact]
        public async Task TestAccountAchievementsSave()
        {
            var wgApi = _container.Resolve<IWargamingApiClient>();
            var dataAccessor = _container.Resolve<IBlitzStaticianDataAccessor>();
            var achievements = await wgApi.GetAccountAchievementsAsync(46512100);

            _log.Debug($"Got {achievements.Count} achievements.");

            var achievementsToRemove = achievements
                .Where(a => a.IsMaxSeries && achievements.Any(a2 => a2.AccountInfoAchievmentId == a.AccountInfoAchievmentId && !a2.IsMaxSeries))
                .ToList();

            achievementsToRemove.ForEach(r => achievements.Remove(r));

            _log.Debug($"{achievements.Count()} remains after deduplication.");

            dataAccessor.SaveAccountAchievements(achievements);
        }

	}
}