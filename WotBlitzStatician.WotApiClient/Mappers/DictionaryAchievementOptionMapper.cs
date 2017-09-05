namespace WotBlitzStatician.WotApiClient.Mappers
{
	using AutoMapper;
	using WotBlitzStatician.Model;
	using WotBlitzStatician.WotApiClient.InternalModel;

	internal class DictionaryAchievementOptionMapper : IMapper<WotEncyclopediaAchievementsOptions, AchievementOption>
	{
		private readonly IMapper _mapper;

		public DictionaryAchievementOptionMapper()
		{
			_mapper = new Mapper(new MapperConfiguration(m => m.CreateMap<WotEncyclopediaAchievementsOptions, AchievementOption>()
				.ForMember(dest => dest.Description, op => op.MapFrom(s => s.Description))
				.ForMember(dest => dest.Image, op => op.MapFrom(s => s.Image))
				.ForMember(dest => dest.ImageBig, op => op.MapFrom(s => s.ImageBig))
				.ForMember(dest => dest.Name, op => op.MapFrom(s => s.Name))
                                                        ));
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