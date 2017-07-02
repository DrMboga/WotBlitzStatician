namespace WotBlitzStatician.WotApiClient.Mappers
{
    using AutoMapper;
    using WotBlitzStatician.Model;
    using WotBlitzStatician.WotApiClient.InternalModel;

    internal class ClanAccountInfoMapper : IMapper<WotClansAccountinfoResponse, AccountClanInfo>
    {
        public ClanAccountInfoMapper()
        {
            Mapper.Initialize(m => m.CreateMap<WotClansAccountinfoResponse, AccountClanInfo>()
                              .ForMember(dest => dest.AccountId, op => op.MapFrom(s => s.AccountId))
                              .ForMember(dest => dest.PlayerJoinedAt, op => op.MapFrom(s => s.JoinedAt))
                              .ForMember(dest => dest.PlayerRole, op => op.MapFrom(svm => svm.Role))
                             );

        }

        public AccountClanInfo Map(WotClansAccountinfoResponse source)
        {
            return Mapper.Map<WotClansAccountinfoResponse, AccountClanInfo>(source);
        }

        public AccountClanInfo Map(WotClansAccountinfoResponse source, AccountClanInfo destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}
