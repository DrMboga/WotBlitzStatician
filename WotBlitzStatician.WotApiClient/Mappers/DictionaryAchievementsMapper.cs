﻿namespace WotBlitzStatician.WotApiClient.Mappers
{
	using AutoMapper;
	using WotBlitzStatician.Model;
	using WotBlitzStatician.WotApiClient.InternalModel;

	internal class DictionaryAchievementsMapper : IMapper<WotEncyclopediaAchievementsResponse, Achievement>
	{
		private readonly IMapper _mapper;

		public DictionaryAchievementsMapper()
		{
			_mapper = new Mapper(new MapperConfiguration(m => m.CreateMap<WotEncyclopediaAchievementsResponse, Achievement>()
				.ForMember(dest => dest.AchievementId, op => op.MapFrom(s => s.AchievementId))
				.ForMember(dest => dest.Condition, op => op.MapFrom(s => s.Condition))
				.ForMember(dest => dest.Description, op => op.MapFrom(s => s.Description))
				.ForMember(dest => dest.Image, op => op.MapFrom(s => s.Image))
				.ForMember(dest => dest.ImageBig, op => op.MapFrom(s => s.ImageBig))
				.ForMember(dest => dest.Name, op => op.MapFrom(s => s.Name))
				.ForMember(dest => dest.Order, op => op.MapFrom(s => s.Order))
				.ForMember(dest => dest.Section, op => op.MapFrom(s => s.Section))
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