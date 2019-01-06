using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Moq;
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


			containerBuilder.RegisterInstance(wgApiClientMock.Object).As<IWargamingApiClient>();
        }

        public static void SetupLoggerMocks(this ContainerBuilder containerBuilder)
        {
            containerBuilder.AddLoggerMock<StatisticsCollectorEngine>();
            containerBuilder.AddLoggerMock<ProlongAccessTokenIfNeeded>();

        }
    }
}