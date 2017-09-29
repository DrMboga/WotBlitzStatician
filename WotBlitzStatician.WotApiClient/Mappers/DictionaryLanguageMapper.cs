namespace WotBlitzStatician.WotApiClient.Mappers
{
	using System.Collections.Generic;
	using AutoMapper;
	using WotBlitzStatician.Model;
	using WotBlitzStatician.Model.MapperLogic;

	internal class DictionaryLanguageMapper : IMapper<Dictionary<string, string>, List<DictionaryLanguage>>
	{
		private readonly IMapper _mapper;

		public DictionaryLanguageMapper()
		{
            _mapper = new Mapper(new MapperConfiguration(m => m.CreateMap<KeyValuePair<string, string>, DictionaryLanguage>()
                              .ForMember(dest => dest.LanguageId, op => op.MapFrom(s => s.Key))
                              .ForMember(dest => dest.LanguageName, op => op.MapFrom(svm => svm.Value))));		
		}

		public List<DictionaryLanguage> Map(Dictionary<string, string> source)
		{
			return _mapper.Map<Dictionary<string, string>, List<DictionaryLanguage>>(source);
		}

		public List<DictionaryLanguage> Map(Dictionary<string, string> source, List<DictionaryLanguage> destination)
		{
			return _mapper.Map(source, destination);
		}
	}
}