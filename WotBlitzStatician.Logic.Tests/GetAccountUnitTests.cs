namespace WotBlitzStatician.Logic.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using log4net;
	using Moq;
	using WotBlitzStatician.Data;
	using WotBlitzStatician.WotApiClient;
	using WotBlitzStatician.Model;
	using Xunit;
	/*
	public class GetAccountUnitTests
	{
		private static readonly ILog _log = LogManager.GetLogger(typeof(GetAccountUnitTests));

		private readonly Random _random = new Random();

		private IBlitzStaticianLogic _blitzStaticianLogic;
		private Mock<IBlitzStaticianDataAccessor> _blitzStaticianDataAccessorMock;
		private Mock<IWargamingApiClient> _wgApiClientMock;
		private AccountInfo _expectedAccount;

        // ToDo: Actualize this
		public GetAccountUnitTests()
		{
			Log4NetHelper.ConfigureLog4Net();

			_blitzStaticianDataAccessorMock = new Mock<IBlitzStaticianDataAccessor>();
			_wgApiClientMock = new Mock<IWargamingApiClient>();
            _blitzStaticianLogic = new BlitzStaticianLogic(_blitzStaticianDataAccessorMock.Object, _wgApiClientMock.Object, null);

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

			var account = await _blitzStaticianLogic.GetAccount(string.Empty);

			Assert.NotNull(account); //, "Existing account wasn't found");
			Assert.Equal(_expectedAccount.NickName, account.NickName); //, "Account nicknames doesn't match");
			Assert.Equal(_expectedAccount.AccountId, account.AccountId); //, "Account ids doesn't match");

			_blitzStaticianDataAccessorMock
				.Verify(accessor => accessor.GetAccountInfo(It.IsAny<string>()), Times.Once());
			_wgApiClientMock
				.Verify(apiClient => apiClient.FindAccountAsync(It.IsAny<string>()), Times.Never());
			_wgApiClientMock
				.Verify(apiClient => apiClient.GetTanksStatisticsAsync(It.IsAny<long>()), Times.Never());
			_blitzStaticianDataAccessorMock
				.Verify(accessor => accessor.SaveAccountInfo(It.IsAny<AccountInfo>()), Times.Never());
			_blitzStaticianDataAccessorMock
				.Verify(accessor => accessor.SaveTanksStatistic(It.IsAny<List<AccountTankStatistics>>()), Times.Never());

			_log.Debug("GetExistingAccountFromDb passed");
		}


		[Fact]
		public async Task GetExistingAccountFirstTime()
		{
			_blitzStaticianDataAccessorMock.Setup(accessor => accessor.GetAccountInfo(It.IsAny<string>()))
				.Returns<AccountInfo>(null);
			_wgApiClientMock.Setup(client => client.FindAccountAsync(It.IsAny<string>()))
				.Returns(Task.FromResult(new List<AccountInfo>{_expectedAccount}));
			_wgApiClientMock.Setup(client => client.GetAccountInfoAllStatisticsAsync(It.IsAny<long>()))
				.Returns(Task.FromResult(_expectedAccount));

			var account = await _blitzStaticianLogic.GetAccount(_expectedAccount.NickName);

			Assert.NotNull(account); //, "Existing account wasn't found");
			Assert.Equal(_expectedAccount.NickName, account.NickName); //, "Account nicknames doesn't match");
			Assert.Equal(_expectedAccount.AccountId, account.AccountId); //, "Account ids doesn't match");

			_blitzStaticianDataAccessorMock
				.Verify(accessor => accessor.GetAccountInfo(It.IsAny<string>()), Times.Once());
			_wgApiClientMock
				.Verify(apiClient => apiClient.FindAccountAsync(It.IsAny<string>()), Times.Once());
			_wgApiClientMock
				.Verify(apiClient => apiClient.GetAccountInfoAllStatisticsAsync(It.IsAny<long>()), Times.Once());
			_wgApiClientMock
				.Verify(apiClient => apiClient.GetTanksStatisticsAsync(It.IsAny<long>()), Times.Once());
			_blitzStaticianDataAccessorMock
				.Verify(accessor => accessor.SaveAccountInfo(It.IsAny<AccountInfo>()), Times.Once());
			_blitzStaticianDataAccessorMock
				.Verify(accessor => accessor.SaveTanksStatistic(It.IsAny<List<AccountTankStatistics>>()), Times.Once());

			_log.Debug("GetExistingAccountFirstTime passed");
		}

		[Fact]
		public async Task GetExistingAccountFirstTimeNotFound()
		{
			_blitzStaticianDataAccessorMock.Setup(accessor => accessor.GetAccountInfo(It.IsAny<string>()))
				.Returns<AccountInfo>(null);
			_wgApiClientMock.Setup(client => client.FindAccountAsync(It.IsAny<string>()))
				.Returns(Task.FromResult(new List<AccountInfo> {null}));

			var ex = await Assert.ThrowsAsync<ArgumentException>(() => _blitzStaticianLogic.GetAccount(string.Empty));

			_log.Debug($"GetExistingAccountFirstTimeNotFound passed. Exception message: '{ex.Message}'");
		}
	}
	*/
}
