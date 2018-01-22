using AutoMapper;
using Newtonsoft.Json;
using WotBlitzStatician.Model;
using WotBlitzStatician.Model.MapperLogic;
using WotBlitzStatician.WotApiClient.InternalModel;

namespace WotBlitzStatician.WotApiClient.Mappers
{
	internal class AccountInfoPrivateMapper : IMapper<WotAccountInfoResponse, AccountInfoPrivate>
	{
		private readonly IMapper _mapper;

		public AccountInfoPrivateMapper()
		{
			_mapper = new Mapper(new MapperConfiguration(
				m =>
				{
					m.CreateMap<WotAccountInfoPrivate, AccountInfoPrivate>()
						.ForMember(d => d.ContactsUngrouped, o => o.MapFrom(s => JsonConvert.SerializeObject(s.GroupedContacts.Ungrouped)))
						.ForMember(d => d.ContactsBlocked, o => o.MapFrom(s => JsonConvert.SerializeObject(s.GroupedContacts.Blocked)))
						.ForMember(d => d.ContactsGroups, o => o.MapFrom(s => JsonConvert.SerializeObject(s.GroupedContacts.Groups)));
					m.CreateMap<WotAccountInfoResponse, AccountInfoPrivate>();
				}));
		}

		public AccountInfoPrivate Map(WotAccountInfoResponse source)
		{
			var resp = _mapper.Map<WotAccountInfoResponse, AccountInfoPrivate>(source);
			return _mapper.Map(source.Private, resp);
		}

		public AccountInfoPrivate Map(WotAccountInfoResponse source, AccountInfoPrivate destination)
		{
			var resp = _mapper.Map(source, destination);
			return _mapper.Map(source.Private, resp);
		}
	}
}
