namespace WotBlitzStatician.Data
{
	using Autofac;
	using WotBlitzStatician.Data.DataAccessors;

	public static class BlitzStaticianDataAccessorInstaller
    {
		public static void ConfigureDataAccessor(this ContainerBuilder containerBuilder, string connectionString)
		{
			//containerBuilder.Register(c => new BlitzStaticianDataContextFactory(connectionString))
			//	.As<IBlitzStaticianDataContextFactory>();
            containerBuilder.RegisterType<AccountInfoDataAccessor>().As<IAccountInfoDataAccessor>();
            containerBuilder.RegisterType<AnalyseDataAccessor>().As<IAnalyseDataAccessor>();
            containerBuilder.RegisterType<StaticInfoDataAccessor>().As<IStaticInfoDataAccessor>();
            containerBuilder.RegisterType<TanksStatisticsDataAccessor>().As<ITanksStatisticsDataAccessor>();
		}
    }
}
