namespace WotBlitzStatician.Data
{
	using Autofac;

    public static class BlitzStaticianDataAccessorInstaller
    {
		public static void ConfigureDataAccessor(this ContainerBuilder containerBuilder)
		{
            // ToDo: Create context factory
            containerBuilder.RegisterType<BlitzStaticianDataAccessor>().As<IBlitzStaticianDataAccessor>();
		}
    }
}
