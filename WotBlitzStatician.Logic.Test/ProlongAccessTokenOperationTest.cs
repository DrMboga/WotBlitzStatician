namespace WotBlitzStatician.Logic.Test
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using Autofac;
	using WotBlitzStatician.Data;
	using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;
	using WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations;
	using WotBlitzStatician.Model;
	using Xunit;

	public class ProlongAccessTokenOperationTest : IDisposable
    {
	    private readonly IContainer _container;
	    private readonly StatisticsCollectorOperationContext _operationContext;
	    private readonly AccountInfo _prolongatedAccountInfo;
	    private readonly BlitzStaticianDbContext _dbContext;

		public ProlongAccessTokenOperationTest()
	    {
		    _operationContext = PrepareOperationContextForProlongAccessToken();
		    _prolongatedAccountInfo = PrepareProlongatedAccountInfo();

			var containerBuilder = new ContainerBuilder();

		    containerBuilder.AddWargamingApiClientMock(_prolongatedAccountInfo)
							.AddInMemoryDataBase()
							.AddLoggerMock<ProlongAccessTokenIfNeeded>()
							.RegisterType<ProlongAccessTokenIfNeeded>().AsSelf();

		    _container = containerBuilder.Build();
		    _dbContext = _container.Resolve<BlitzStaticianDbContext>();
		    PopulateDatabase(_dbContext, _operationContext.Accounts.Single().CurrentAccountInfo);
	    }

	    [Fact]
	    public async Task TestProlongAccessTokenOperation()
	    {
		    var prolongAccessTokenOperation = _container.Resolve<ProlongAccessTokenIfNeeded>();
		    await prolongAccessTokenOperation.Execute(_operationContext);

		    var newAccountInfoFromOperationContext = _operationContext.Accounts.Single().CurrentAccountInfo;
		    var newAccountInfofromDb = _dbContext.AccountInfo.Single(i =>
			    i.AccountId == _operationContext.Accounts.Single().CurrentAccountInfo.AccountId);

			Assert.Equal(_prolongatedAccountInfo.AccessToken, newAccountInfoFromOperationContext.AccessToken);
			Assert.Equal(_prolongatedAccountInfo.AccessTokenExpiration, newAccountInfoFromOperationContext.AccessTokenExpiration);
			Assert.Equal(_prolongatedAccountInfo.AccessToken, newAccountInfofromDb.AccessToken);
			Assert.Equal(_prolongatedAccountInfo.AccessTokenExpiration, newAccountInfofromDb.AccessTokenExpiration);
	    }

		[Fact]
		public async Task TestProlongExpiredAccessToken()
		{
			var operationContextWithExpiredAccessToken = new StatisticsCollectorOperationContext();
			operationContextWithExpiredAccessToken.Accounts.Add(new SatisticsCollectorAccountOperationContext
			{
				CurrentAccountInfo = new AccountInfo
				{
					AccessToken = "ExpiredToken",
					AccessTokenExpiration = DateTime.Now.AddDays(-1)
				}
			});
			var prolongAccessTokenOperation = _container.Resolve<ProlongAccessTokenIfNeeded>();
			await prolongAccessTokenOperation.Execute(operationContextWithExpiredAccessToken);

			Assert.Equal(string.Empty,
				operationContextWithExpiredAccessToken.Accounts.Single().CurrentAccountInfo.AccessToken);
		}

		public void Dispose()
	    {
		    _dbContext.Dispose();
	    }

	    private static StatisticsCollectorOperationContext PrepareOperationContextForProlongAccessToken()
	    {
			var rnd = new Random();
			var operationContext = new StatisticsCollectorOperationContext();

			operationContext.Accounts.Add(new SatisticsCollectorAccountOperationContext
			{
				CurrentAccountInfo = new AccountInfo
				{
					AccountId = rnd.Next(),
					AccessToken = "OldAccessToken",
					AccessTokenExpiration = DateTime.Now.AddDays(1)
				}
			});

		    return operationContext;
	    }

	    private static AccountInfo PrepareProlongatedAccountInfo()
	    {
		    return new AccountInfo
		    {
			    AccessToken = "NewAccessToken",
			    AccessTokenExpiration = DateTime.Now.AddDays(7)
		    };
	    }

		private static void PopulateDatabase(BlitzStaticianDbContext dbContext, AccountInfo accountInfo)
		{
			dbContext.AccountInfo.Add(accountInfo);
			dbContext.SaveChanges();
		}

	}
}
