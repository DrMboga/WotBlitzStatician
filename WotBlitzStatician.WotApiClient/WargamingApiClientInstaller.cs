namespace WotBlitzStatician.WotApiClient
{
	using System.Collections.Generic;
	using Autofac;
	using Microsoft.Extensions.Logging;
	using WotBlitzStatician.Model;
	using WotBlitzStatician.Model.MapperLogic;
	using WotBlitzStatician.WotApiClient.InternalModel;
	using WotBlitzStatician.WotApiClient.Mappers;
	using WotBlitzStatician.WotApiClient.RequestStringBuilder;

	public static class WargamingApiClientInstaller
	{
		public static void ConfigureWargamingApi(this ContainerBuilder containerBuilder)
		{
			containerBuilder.ConfigureMappers();

			containerBuilder.RegisterType<RequestBuilder>().As<IRequestBuilder>();
			containerBuilder.RegisterType<WebApiClient>();
			containerBuilder.RegisterType<WargamingApiClient>()
				.As<IWargamingApiClient>();

		}

		private static void ConfigureMappers(this ContainerBuilder containerBuilder)
		{
			containerBuilder.RegisterType<AccountInfoMapper>().As<IMapper<WotAccountInfoResponse, AccountInfo>>();
			containerBuilder.RegisterType<AccountInfoStatisticsMapper>().As<IMapper<WotAccountInfoResponse, AccountInfoStatistics>>();
			containerBuilder.RegisterType<AccountInfoPrivateMapper>().As<IMapper<WotAccountInfoResponse, AccountInfoPrivate>>();
			containerBuilder.RegisterType<AccounutFindResponseMapper>().As<IMapper<List<WotAccountListResponse>, List<AccountInfo>>>();
			containerBuilder.RegisterType<ClanAccountInfoMapper>().As<IMapper<WotClansAccountinfoResponse, AccountClanInfo>>();
			containerBuilder.RegisterType<ClanInfoResponseMapper>().As<IMapper<WotClanInfoResponse, AccountClanInfo>>();
			containerBuilder.RegisterType<DictionaryAchievementOptionMapper>().As<IMapper<WotEncyclopediaAchievementsOptions, AchievementOption>>();
			containerBuilder.RegisterType<DictionaryAchievementsMapper>().As<IMapper<WotEncyclopediaAchievementsResponse, Achievement>>();
			containerBuilder.RegisterType<DictionaryLanguageMapper>().As<IMapper<Dictionary<string, string>, List<DictionaryLanguage>>>();
			containerBuilder.RegisterType<DictionaryNationMapper>().As<IMapper<Dictionary<string, string>, List<DictionaryNations>>>();
			containerBuilder.RegisterType<DictionaryVehicleTypeMapper>().As<IMapper<Dictionary<string, string>, List<DictionaryVehicleType>>>();
			containerBuilder.RegisterType<DictionaryAchievementsSectionsMapper>().As<IMapper<Dictionary<string, WotEncyclopediaInfoAchievement_section>, List<AchievementSection>>>();
			containerBuilder.RegisterType<DictionaryClanRolesMapper>().As<IMapper<Dictionary<string, string>, List<DictionaryClanRole>>>();
			containerBuilder.RegisterType<TankopediaMapper>().As<IMapper<List<WotEncyclopediaVehiclesResponse>, List<VehicleEncyclopedia>>>();
			containerBuilder.RegisterType<TanksStatMapper>().As<IMapper<List<WotAccountTanksStatResponse>, List<AccountTankStatistics>>>();
			containerBuilder.RegisterType<WotAuthProlongateResponseMapper>().As<IMapper<WotAuthProlongateResponse, AccountInfo>>();
			containerBuilder.RegisterType<PlayerPrivateInfoMapper>().As<IMapper<WotAccountInfoPrivate, PlayerPrivateInfoDto>>();


			containerBuilder.RegisterType<MapperHelper>().As<IMapperHelper>();
		}

	}
}