using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using log4net;
using log4net.Config;
using Moq;
using WotBlitzStatician.Data;
using WotBlitzStatician.WotApiClient;
using WotBlitzStaticitian.Model;
using Xunit;

namespace WotBlitzStatician.Logic.Tests
{
    public class GetAccountUnitTests
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(GetAccountUnitTests));

		private readonly Random _random = new Random();

		private IBlitzStaticianLogic _blitzStaticianLogicMock;
		private Mock<IBlitzStaticianDataAccessor> _blitzStaticianDataAccessorMock;
		private Mock<IWargamingApiClient> _wgApiClientMock;
		private AccountInfo _expectedAccount;


		public GetAccountUnitTests()
        {
			var log4netConfig = new XmlDocument();
			log4netConfig.Load(File.OpenRead("Log4net.xml"));

			var repo = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            XmlConfigurator.Configure(repo, log4netConfig["log4net"]);

            _blitzStaticianDataAccessorMock = new Mock<IBlitzStaticianDataAccessor>();
            _wgApiClientMock = new Mock<IWargamingApiClient>();
            _blitzStaticianLogicMock = new BlitzStaticianLogic(_blitzStaticianDataAccessorMock.Object, _wgApiClientMock.Object);

			_expectedAccount = new AccountInfo
			{
				AccountId = _random.Next(),
				NickName = Guid.NewGuid().ToString()
			};

		}

        [Fact]
		public async Task GetExistingAccountFromDb()
        {
            _blitzStaticianDataAccessorMock.Setup(accessor => accessor.GetAccountInfo(It.IsAny<string>()))
                                           .Returns(_expectedAccount);

			var account = await _blitzStaticianLogicMock.GetAccount(string.Empty);

            Assert.NotNull(account); //, "Existing account wasn't found");
            Assert.Equal(_expectedAccount.NickName, account.NickName); //, "Account nicknames doesn't match");
            Assert.Equal(_expectedAccount.AccountId, account.AccountId); //, "Account ids doesn't match");

            _blitzStaticianDataAccessorMock
                .Verify(accessor => accessor.GetAccountInfo(It.IsAny<string>()), Times.Once());
            _wgApiClientMock
                .Verify(apiClient => apiClient.FindAccount(It.IsAny<string>()), Times.Never());
            _wgApiClientMock
				.Verify(apiClient => apiClient.GetTanksStatisticks(It.IsAny<long>()), Times.Never());
            _blitzStaticianDataAccessorMock
                .Verify(accessor => accessor.SaveAccountInfo(It.IsAny<AccountInfo>()), Times.Never());
            _blitzStaticianDataAccessorMock
                .Verify(accessor => accessor.SaveTanksStatistic(It.IsAny<List<AccountTankStatistics>>()), Times.Never());

            _log.Debug("GetAccountUnitTests.GetExistingAccountFromDb passed");
        }

	}
}
