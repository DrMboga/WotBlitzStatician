namespace WotBlitzStatician.Data
{
	using Autofac;

    public static class BlitzStaticianDataAccessorInstaller
    {
		public static void ConfigureDataAccessor(this ContainerBuilder containerBuilder, string connectionString)
		{
			containerBuilder.Register(c => new BlitzStaticianDataContextFactory(connectionString))
				.As<IBlitzStaticianDataContextFactory>();
            containerBuilder.RegisterType<BlitzStaticianDataAccessor>().As<IBlitzStaticianDataAccessor>();
		}
    }
}
