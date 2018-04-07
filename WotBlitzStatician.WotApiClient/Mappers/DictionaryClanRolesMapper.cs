using System.Collections.Generic;
using AutoMapper;
using WotBlitzStatician.Model;
using WotBlitzStatician.Model.MapperLogic;

namespace WotBlitzStatician.WotApiClient.Mappers
{
	internal class DictionaryClanRolesMapper : IMapper<Dictionary<string, string>, List<DictionaryClanRole>>
	{
		private readonly IMapper _mapper;

		public DictionaryClanRolesMapper()
		{
			_mapper = new Mapper(new MapperConfiguration(m => m.CreateMap<KeyValuePair<string, string>, DictionaryClanRole>()
				  .ForMember(dest => dest.ClanRoleId, op => op.MapFrom(s => s.Key))
				  .ForMember(dest => dest.RoleName, op => op.MapFrom(svm => svm.Value))));
		}

		public List<DictionaryClanRole> Map(Dictionary<string, string> source)
		{
			return _mapper.Map<Dictionary<string, string>, List<DictionaryClanRole>>(source);
		}

		public List<DictionaryClanRole> Map(Dictionary<string, string> source, List<DictionaryClanRole> destination)
		{
			return _mapper.Map(source, destination);
		}
	}
}
