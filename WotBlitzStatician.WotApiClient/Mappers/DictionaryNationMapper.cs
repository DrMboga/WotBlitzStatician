namespace WotBlitzStatician.WotApiClient.Mappers
{
    using System.Collections.Generic;
    using AutoMapper;
    using WotBlitzStatician.Model;

    internal class DictionaryNationMapper : IMapper<KeyValuePair<string, string>, DictionaryNations>
    {
		private readonly IMapper _mapper;

		public DictionaryNationMapper()
        {
			_mapper = new Mapper(new MapperConfiguration(m => m.CreateMap<KeyValuePair<string, string>, DictionaryNations>()
                              .ForMember(dest => dest.NationId, op => op.MapFrom(s => s.Key))
                                                         .ForMember(dest => dest.NationName, op => op.MapFrom(svm => svm.Value))));

		}

        public DictionaryNations Map(KeyValuePair<string, string> source)
        {
            return _mapper.Map<KeyValuePair<string, string>, DictionaryNations>(source);
        }

        public DictionaryNations Map(KeyValuePair<string, string> source, DictionaryNations destination)
        {
            return _mapper.Map(source, destination);
        }
    }
}