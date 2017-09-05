namespace WotBlitzStatician.WotApiClient.Mappers
{
	using AutoMapper;
	using WotBlitzStatician.Model;
	using WotBlitzStatician.WotApiClient.InternalModel;

	internal class AccounutFindResponseMapper : IMapper<WotAccountListResponse, AccountInfo>
	{
		private readonly IMapper _mapper;

		public AccounutFindResponseMapper()
		{
			_mapper = new Mapper(new MapperConfiguration(m => m.CreateMap<WotAccountListResponse, AccountInfo>()
				.ForMember(dest => dest.AccountId, op => op.MapFrom(s => s.AccountId))
				.ForMember(dest => dest.NickName, op => op.MapFrom(s => s.Nickname))
                                                        ));

		}

		public AccountInfo Map(WotAccountListResponse source)
		{
			return _mapper.Map<WotAccountListResponse, AccountInfo>(source);
		}

		public AccountInfo Map(WotAccountListResponse source, AccountInfo destination)
		{
			return _mapper.Map(source, destination);
		}
	}
}
