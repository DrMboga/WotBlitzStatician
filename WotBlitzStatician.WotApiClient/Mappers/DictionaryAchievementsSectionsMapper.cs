using System.Collections.Generic;
using AutoMapper;
using WotBlitzStatician.Model;
using WotBlitzStatician.Model.MapperLogic;
using WotBlitzStatician.WotApiClient.InternalModel;

namespace WotBlitzStatician.WotApiClient.Mappers
{
	internal class DictionaryAchievementsSectionsMapper :
		IMapper<Dictionary<string, WotEncyclopediaInfoAchievement_section>, List<AchievementSection>>
	{
		private readonly IMapper _mapper;

		public DictionaryAchievementsSectionsMapper()
		{
			_mapper = new Mapper(new MapperConfiguration(
				m =>
				{
					m.CreateMap<KeyValuePair<string, WotEncyclopediaInfoAchievement_section>, AchievementSection>()
					.ForMember(dest => dest.Section, op => op.MapFrom(s => s.Key))
					.ForMember(dest => dest.SectionName, op => op.MapFrom(svm => svm.Value.Name))
					.ForMember(dest => dest.Order, op => op.MapFrom(svm => svm.Value.Order));
				  }));

		}

		public List<AchievementSection> Map(Dictionary<string, WotEncyclopediaInfoAchievement_section> source)
		{
			return _mapper.Map<Dictionary<string, WotEncyclopediaInfoAchievement_section>, List<AchievementSection>>(source);
		}

		public List<AchievementSection> Map(Dictionary<string, WotEncyclopediaInfoAchievement_section> source, List<AchievementSection> destination)
		{
			return _mapper.Map(source, destination);
		}
	}
}
