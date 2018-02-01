namespace WotBlitzStatician.Data
{
	using Autofac;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.DependencyInjection;
	using WotBlitzStatician.Data.DataAccessors;

	public static class BlitzStaticianDataAccessorInstaller
    {
		public static void AddBlitzStaticianDbContextPool(this IServiceCollection services, string connectionString)
		{
			services.AddDbContextPool<BlitzStaticianDbContext>(options =>
			{
				options.UseSqlServer(connectionString);
				// options.UseLoggerFactory(MyLoggerFactory);
			}
				);
		}

		public static void ConfigureDataAccessor(this ContainerBuilder containerBuilder)
		{

			//containerBuilder.Register(c => new BlitzStaticianDataContextFactory(connectionString))
			//	.As<IBlitzStaticianDataContextFactory>();
            containerBuilder.RegisterType<BlitzStaticianDictionary>().As<IBlitzStaticianDictionary>();
            containerBuilder.RegisterType<AccountInfoDataAccessor>().As<IAccountInfoDataAccessor>();
            containerBuilder.RegisterType<AnalyseDataAccessor>().As<IAnalyseDataAccessor>();
            containerBuilder.RegisterType<TanksStatisticsDataAccessor>().As<ITanksStatisticsDataAccessor>();
		}
    }
}
