namespace WotBlitzStatician.WotApiClient.Mappers
{
    using AutoMapper;
    using WotBlitzStatician.Model;
    using WotBlitzStatician.WotApiClient.InternalModel;

    internal class ClanAccountInfoMapper : IMapper<WotClansAccountinfoResponse, AccountClanInfo>
    {
		private readonly IMapper _mapper;

		public ClanAccountInfoMapper()
        {
            _mapper = new Mapper(new MapperConfiguration(m => m.CreateMap<WotClansAccountinfoResponse, AccountClanInfo>()
                              .ForMember(dest => dest.AccountId, op => op.MapFrom(s => s.AccountId))
                              .ForMember(dest => dest.PlayerJoinedAt, op => op.MapFrom(s => s.JoinedAt))
                              .ForMember(dest => dest.PlayerRole, op => op.MapFrom(svm => svm.Role))
                                                        ));

        }

        public AccountClanInfo Map(WotClansAccountinfoResponse source)
        {
            return _mapper.Map<WotClansAccountinfoResponse, AccountClanInfo>(source);
        }

        public AccountClanInfo Map(WotClansAccountinfoResponse source, AccountClanInfo destination)
        {
            return _mapper.Map(source, destination);
        }
    }
}
