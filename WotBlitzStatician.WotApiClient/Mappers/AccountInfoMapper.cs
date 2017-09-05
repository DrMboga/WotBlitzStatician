namespace WotBlitzStatician.WotApiClient.Mappers
{
	using AutoMapper;
	using WotBlitzStatician.Model;
	using WotBlitzStatician.WotApiClient.InternalModel;

	internal class AccountInfoMapper : IMapper<WotAccountInfoResponse, AccountInfo>
	{
        private readonly IMapper _mapper;

		public AccountInfoMapper()
		{
            _mapper = new Mapper(new MapperConfiguration(m => m.CreateMap<WotAccountInfoResponse, AccountInfo>()
               .ForMember(dest => dest.AccountId, op => op.MapFrom(s => s.AccountId))
               .ForMember(dest => dest.AccountCreatedAt, op => op.MapFrom(s => s.CreatedAt))
               .ForMember(dest => dest.NickName, op => op.MapFrom(s => s.Nickname))
               .ForMember(dest => dest.LastBattleTime, op => op.MapFrom(s => s.LastBattleTime))
                                                         ));
		}

		public AccountInfo Map(WotAccountInfoResponse source)
		{
			return _mapper.Map<WotAccountInfoResponse, AccountInfo>(source);
		}

		public AccountInfo Map(WotAccountInfoResponse source, AccountInfo destination)
		{
			return _mapper.Map(source, destination);
		}
	}
}
