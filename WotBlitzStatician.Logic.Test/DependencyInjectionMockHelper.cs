namespace WotBlitzStatician.Logic.Test
{
  using System.Threading.Tasks;
  using Autofac;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Storage;
  using Microsoft.Extensions.Logging;
  using Moq;
  using WotBlitzStatician.Data;
  using WotBlitzStatician.Data.DataAccessors;
  using WotBlitzStatician.Data.DataAccessors.Impl;
  using WotBlitzStatician.Model;
  using WotBlitzStatician.WotApiClient;
  using Xunit.Abstractions;

  public static class DependencyInjectionMockHelper
  {
    public static ContainerBuilder AddWargamingApiClientMock(this ContainerBuilder containerBuilder, AccountInfo prolongatedAccountInfo)
    {
      var wgApiClientMock = new Mock<IWargamingApiClient>();

      wgApiClientMock.Setup(c => c.ProlongateAccount(It.IsAny<string>()))
        .Returns(Task.FromResult(prolongatedAccountInfo));

      containerBuilder.RegisterInstance(wgApiClientMock.Object).As<IWargamingApiClient>();
      return containerBuilder;
    }

    public static ContainerBuilder AddInMemoryDataBase(this ContainerBuilder containerBuilder, ILoggerFactory loggerFactory = null)
    {
      var dbContextBuilder = new DbContextOptionsBuilder<BlitzStaticianDbContext>()
              .UseInMemoryDatabase("BlitzStatician");

      if (loggerFactory != null)
      {
        dbContextBuilder.UseLoggerFactory(loggerFactory);
      }

      var context = new BlitzStaticianDbContext(dbContextBuilder.Options);

#pragma warning disable CS0618
      var dummyTran = new Mock<IDbContextTransaction>();
      var accountDataAccessor = new AccountDataAccessor(context, dummyTran.Object);
#pragma warning restore CS0618

      containerBuilder.RegisterInstance(context).As<BlitzStaticianDbContext>().ExternallyOwned();
      containerBuilder.RegisterType<BlitzStaticianDictionary>().As<IBlitzStaticianDictionary>();
      containerBuilder.RegisterInstance(accountDataAccessor).As<IAccountDataAccessor>();
      containerBuilder.RegisterType<ClanInfoDataAccessor>().As<IClanInfoDataAccessor>();
      containerBuilder.RegisterType<AchievementsDataAccessor>().As<IAchievementsDataAccessor>();
      containerBuilder.RegisterType<AccountsTankInfoDataAccessor>().As<IAccountsTankInfoDataAccessor>();
      containerBuilder.RegisterType<AccountInfoViewDataAccessor>().As<IAccountInfoViewDataAccessor>();

      return containerBuilder;
    }

    public static ContainerBuilder AddLoggerMock<T>(this ContainerBuilder containerBuilder)
    {
      var logger = new Mock<ILogger<T>>();

      containerBuilder.RegisterInstance(logger.Object).
        As<ILogger<T>>();
      return containerBuilder;
    }
  }
}