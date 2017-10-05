namespace WotBlitzStatician
{
	using System.IO;
	using Autofac;
	using Microsoft.Extensions.Configuration;
	using WotBlitzStatician.Data;
	using WotBlitzStatician.Logic;
	using WotBlitzStatician.Logic.Dto;
	using WotBlitzStatician.Mappers;
	using WotBlitzStatician.Model;
	using WotBlitzStatician.Model.MapperLogic;
	using WotBlitzStatician.ViewModel;
	using WotBlitzStatician.WotApiClient;

	public static class WotBlitzStaticianInstaller
	{
        //private const string ConnectionString = @"Data Source=.\Data\BlitzStatician.db";
//		private const string ConnectionString = @"Data Source=..\..\..\Data\BlitzStatician.db";

		public static void ConfigureWotBlitzStatician(this ContainerBuilder containerBuilder)
		{
            string connectionString = $"Data Source={Path.Combine(Directory.GetCurrentDirectory(), "Data\\BlitzStatician.db")}";

			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
			var configuration = builder.Build();

			var appsettings = new Appsettings();
			configuration.GetSection("WgApi").Bind(appsettings);
			appsettings.ProxySettings = new ProxySettings();
			configuration.GetSection("ProxySettings").Bind(appsettings.ProxySettings);

			containerBuilder.RegisterInstance(appsettings).As<IWgApiConfiguration>();
			containerBuilder.ConfigureWargamingApi();
			containerBuilder.ConfigureDataAccessor(connectionString);
			containerBuilder.RegisterType<BlitzStaticianDictionary>().As<IBlitzStaticianDictionary>().SingleInstance();
			containerBuilder.RegisterType<BlitzStaticianLogic>().As<IBlitzStaticianLogic>();
            containerBuilder.RegisterType<BlitzStatitianDataAnalyser>().As<IBlitzStatitianDataAnalyser>();

			ConfigureMappers(containerBuilder);
		}

		private static void ConfigureMappers(this ContainerBuilder containerBuilder)
		{
			containerBuilder.RegisterType<AccountInfoDeltaMapper>().As<IMapper<BlitzAccountInfoStatisticsDelta, AccountInfoDeltaViewModel>>();
			containerBuilder.RegisterType<AccountInfoMapper>().As<IMapper<AccountInfo, AccountInfoViewModel>>();
			containerBuilder.RegisterType<TankDeltaMapper>().As<IMapper<BlitzTankInfoDelta, TankDeltaViewModel>>();

			containerBuilder.RegisterType<MapperHelper>().As<IMapperHelper>();
		}

	}
}