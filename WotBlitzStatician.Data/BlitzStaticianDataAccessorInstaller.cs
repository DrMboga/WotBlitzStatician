namespace WotBlitzStatician.Data
{
	using Autofac;
	using Microsoft.Extensions.Logging;
	using WotBlitzStatician.Data.DataAccessors;
	using WotBlitzStatician.Data.Mappers;
	using WotBlitzStatician.Model;
	using WotBlitzStatician.Model.Dto;
	using WotBlitzStatician.Model.MapperLogic;

	public static class BlitzStaticianDataAccessorInstaller
    {
		public static void ConfigureDataAccessor(this ContainerBuilder containerBuilder, string connectionString)
		{
			containerBuilder.ConfigureMappers();
			containerBuilder.Register<BlitzStaticianDbContext>(c =>
				new BlitzStaticianDbContext(connectionString, c.Resolve<ILoggerFactory>()))
				.InstancePerDependency();

            containerBuilder.RegisterType<BlitzStaticianDictionary>().As<IBlitzStaticianDictionary>();
            containerBuilder.RegisterType<AccountDataAccessor>().As<IAccountDataAccessor>();
			containerBuilder.RegisterType<ClanInfoDataAccessor>().As<IClanInfoDataAccessor>();
			containerBuilder.RegisterType<AchievementsDataAccessor>().As<IAchievementsDataAccessor>();
		}

		private static void ConfigureMappers(this ContainerBuilder containerBuilder)
		{
			containerBuilder.RegisterType<PlayerStatisticsDtoMapper>().As<IMapper<AccountInfoStatistics, PlayerStatDto>>();
		}

	}
}
