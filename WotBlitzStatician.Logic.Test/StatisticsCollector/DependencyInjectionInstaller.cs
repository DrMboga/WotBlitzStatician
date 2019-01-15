using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Moq;
using WotBlitzStatician.Data.DataAccessors.Impl;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations;
using WotBlitzStatician.Model;
using WotBlitzStatician.WotApiClient;

namespace WotBlitzStatician.Logic.Test.StatisticsCollector
{
    public static class DependencyInjectionInstaller
    {
        public static void SetupWargamingApiMockDependencies(this ContainerBuilder containerBuilder,
                                                            DataStubs dataStubs)
        {
			var wgApiClientMock = new Mock<IWargamingApiClient>();
            wgApiClientMock.Setup(c => c.ProlongateAccount(It.IsAny<string>()))
                .ReturnsAsync(dataStubs.AccountInfo);
                
            wgApiClientMock.Setup(c => c.GetAccountInfoAllStatisticsAsync(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(dataStubs.WargamingAccountInfo);

            wgApiClientMock.Setup(c => c.GetAccountClanInfoAsync(It.IsAny<long>()))
                .ReturnsAsync(dataStubs.AccountClanInfo);

            wgApiClientMock.Setup(c => c.GetAccountAchievementsAsync(It.IsAny<long>()))
                .ReturnsAsync(dataStubs.AccountInfoAchievements);

            wgApiClientMock.Setup(c => c.GetTanksStatisticsAsync(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(dataStubs.AccountTanksStatistics);

            wgApiClientMock.Setup(c => c.GetAccountTankAchievementsAsync(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<List<int>>()))
                .ReturnsAsync(dataStubs.AccountInfoTankAchievements);


			containerBuilder.RegisterInstance(wgApiClientMock.Object).As<IWargamingApiClient>();
        }

        public static void SetupLoggerMocks(this ContainerBuilder containerBuilder)
        {
            containerBuilder.AddLoggerMock<StatisticsCollectorEngine>();
            containerBuilder.AddLoggerMock<ProlongAccessTokenIfNeeded>();
            containerBuilder.AddLoggerMock<BlitzStaticianDictionary>();

        }
    }
}