namespace WotBlitzStatician.WotApiClient.Mappers
{
	using AutoMapper;
	using WotBlitzStatician.Model;
	using WotBlitzStatician.WotApiClient.InternalModel;

	internal class DictionaryAchievementOptionMapper : IMapper<WotEncyclopediaAchievementsOptions, AchievementOption>
	{
		public DictionaryAchievementOptionMapper()
		{
			Mapper.Initialize(m => m.CreateMap<WotEncyclopediaAchievementsOptions, AchievementOption>()
				.ForMember(dest => dest.Description, op => op.MapFrom(s => s.Description))
				.ForMember(dest => dest.Image, op => op.MapFrom(s => s.Image))
				.ForMember(dest => dest.ImageBig, op => op.MapFrom(s => s.ImageBig))
				.ForMember(dest => dest.Name, op => op.MapFrom(s => s.Name))
			);
		}

		public AchievementOption Map(WotEncyclopediaAchievementsOptions source)
		{
			return Mapper.Map<WotEncyclopediaAchievementsOptions, AchievementOption>(source);
		}

		public AchievementOption Map(WotEncyclopediaAchievementsOptions source, AchievementOption destination)
		{
			return Mapper.Map(source, destination);
		}
	}
}