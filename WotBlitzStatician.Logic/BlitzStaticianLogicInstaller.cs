using Autofac;

namespace WotBlitzStatician.Logic
{
    public static class BlitzStaticianLogicInstaller
    {
		public static void ConfigureBlitzStaticianLogic(this ContainerBuilder containerBuilder)
		{
			containerBuilder.RegisterType<BlitzStaticianDictionary>()
				.As<IBlitzStaticianDictionary>();
		}

	}
}
