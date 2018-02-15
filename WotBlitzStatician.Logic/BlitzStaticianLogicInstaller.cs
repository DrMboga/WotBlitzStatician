using Autofac;
using WotBlitzStatician.Logic.StatisticsCollectorOperations;

namespace WotBlitzStatician.Logic
{
    public static class BlitzStaticianLogicInstaller
    {
		public static void ConfigureBlitzStaticianLogic(this ContainerBuilder containerBuilder)
		{
			containerBuilder.RegisterType<StatisticsCollector>()
				.As<IStatisticsCollector>();
		}

	}
}
