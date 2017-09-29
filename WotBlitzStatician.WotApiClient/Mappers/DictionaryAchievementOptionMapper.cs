namespace WotBlitzStatician.WotApiClient.Mappers
{
	using AutoMapper;
	using WotBlitzStatician.Model;
	using WotBlitzStatician.Model.MapperLogic;
	using WotBlitzStatician.WotApiClient.InternalModel;

	internal class DictionaryAchievementOptionMapper : IMapper<WotEncyclopediaAchievementsOptions, AchievementOption>
	{
		private readonly IMapper _mapper;

		public DictionaryAchievementOptionMapper()
		{
			_mapper = new Mapper(new MapperConfiguration(m => m.CreateMap<WotEncyclopediaAchievementsOptions, AchievementOption>()));
		}

		public AchievementOption Map(WotEncyclopediaAchievementsOptions source)
		{
			return _mapper.Map<WotEncyclopediaAchievementsOptions, AchievementOption>(source);
		}

		public AchievementOption Map(WotEncyclopediaAchievementsOptions source, AchievementOption destination)
		{
			return _mapper.Map(source, destination);
		}
	}
}