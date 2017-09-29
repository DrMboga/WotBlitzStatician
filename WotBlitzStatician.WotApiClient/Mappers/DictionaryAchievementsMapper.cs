namespace WotBlitzStatician.WotApiClient.Mappers
{
	using AutoMapper;
	using WotBlitzStatician.Model;
	using WotBlitzStatician.Model.MapperLogic;
	using WotBlitzStatician.WotApiClient.InternalModel;

	internal class DictionaryAchievementsMapper : IMapper<WotEncyclopediaAchievementsResponse, Achievement>
	{
		private readonly IMapper _mapper;

		public DictionaryAchievementsMapper()
		{
			_mapper = new Mapper(new MapperConfiguration(m => m.CreateMap<WotEncyclopediaAchievementsResponse, Achievement>()
                              .ForMember(dest => dest.Options, op => op.Ignore())
                                                        ));
		}

		public Achievement Map(WotEncyclopediaAchievementsResponse source)
		{
			return _mapper.Map<WotEncyclopediaAchievementsResponse, Achievement>(source);
		}

		public Achievement Map(WotEncyclopediaAchievementsResponse source, Achievement destination)
		{
			return _mapper.Map(source, destination);
		}
	}
}