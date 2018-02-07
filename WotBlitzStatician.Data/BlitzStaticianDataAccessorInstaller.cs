namespace WotBlitzStatician.Data
{
	using Autofac;
	using Microsoft.Extensions.Logging;
	using WotBlitzStatician.Data.DataAccessors;

	public static class BlitzStaticianDataAccessorInstaller
    {
		public static void ConfigureDataAccessor(this ContainerBuilder containerBuilder, string connectionString)
		{
			containerBuilder.Register<BlitzStaticianDbContext>(c =>
				new BlitzStaticianDbContext(connectionString, c.Resolve<ILoggerFactory>()))
				.InstancePerDependency();

            containerBuilder.RegisterType<BlitzStaticianDictionary>().As<IBlitzStaticianDictionary>();
            containerBuilder.RegisterType<AccountInfoDataAccessor>().As<IAccountInfoDataAccessor>();
            containerBuilder.RegisterType<AnalyseDataAccessor>().As<IAnalyseDataAccessor>();
            containerBuilder.RegisterType<TanksStatisticsDataAccessor>().As<ITanksStatisticsDataAccessor>();
		}
    }
}
