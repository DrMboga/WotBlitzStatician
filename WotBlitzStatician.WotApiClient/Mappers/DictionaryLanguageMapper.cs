namespace WotBlitzStatician.WotApiClient.Mappers
{
	using System.Collections.Generic;
	using AutoMapper;
	using WotBlitzStatician.Model;

    internal class DictionaryLanguageMapper : IMapper<KeyValuePair<string, string>, DictionaryLanguage>
	{
		private readonly IMapper _mapper;

		public DictionaryLanguageMapper()
		{
            _mapper = new Mapper(new MapperConfiguration(m => m.CreateMap<KeyValuePair<string, string>, DictionaryLanguage>()
                              .ForMember(dest => dest.LanguageId, op => op.MapFrom(s => s.Key))
                                                         .ForMember(dest => dest.LanguageName, op => op.MapFrom(svm => svm.Value))));		
		}

		public DictionaryLanguage Map(KeyValuePair<string, string> source)
		{
			return _mapper.Map<KeyValuePair<string, string>, DictionaryLanguage>(source);
		}

		public DictionaryLanguage Map(KeyValuePair<string, string> source, DictionaryLanguage destination)
		{
			return _mapper.Map(source, destination);
		}
	}
}