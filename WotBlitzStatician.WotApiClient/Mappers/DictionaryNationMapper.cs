namespace WotBlitzStatician.WotApiClient.Mappers
{
    using System.Collections.Generic;
    using AutoMapper;
    using WotBlitzStatician.Model;
    using WotBlitzStatician.Model.MapperLogic;

	internal class DictionaryNationMapper : IMapper<Dictionary<string, string>, List<DictionaryNations>>
    {
		private readonly IMapper _mapper;

		public DictionaryNationMapper()
        {
			_mapper = new Mapper(new MapperConfiguration(m => m.CreateMap<KeyValuePair<string, string>, DictionaryNations>()
                              .ForMember(dest => dest.NationId, op => op.MapFrom(s => s.Key))
                                                         .ForMember(dest => dest.NationName, op => op.MapFrom(svm => svm.Value))));

		}

        public List<DictionaryNations> Map(Dictionary<string, string> source)
        {
            return _mapper.Map<Dictionary<string, string>, List<DictionaryNations>>(source);
        }

        public List<DictionaryNations> Map(Dictionary<string, string> source, List<DictionaryNations> destination)
        {
            return _mapper.Map(source, destination);
        }
    }
}