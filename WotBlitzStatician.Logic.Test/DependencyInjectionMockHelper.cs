namespace WotBlitzStatician.Logic.Test
{
	using System.Threading.Tasks;
	using Autofac;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;
	using Moq;
	using WotBlitzStatician.Data;
	using WotBlitzStatician.Data.DataAccessors;
	using WotBlitzStatician.Data.DataAccessors.Impl;
	using WotBlitzStatician.Model;
	using WotBlitzStatician.WotApiClient;

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

		public static ContainerBuilder AddInMemoryDataBase(this ContainerBuilder containerBuilder)
		{
			var options = new DbContextOptionsBuilder<BlitzStaticianDbContext>()
				.UseInMemoryDatabase("BlitzStatician")
				.Options;
			var context = new BlitzStaticianDbContext(options);

			containerBuilder.RegisterInstance(context).As<BlitzStaticianDbContext>().ExternallyOwned();
			containerBuilder.RegisterType<BlitzStaticianDictionary>().As<IBlitzStaticianDictionary>();
			containerBuilder.RegisterType<AccountDataAccessor>().As<IAccountDataAccessor>();

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