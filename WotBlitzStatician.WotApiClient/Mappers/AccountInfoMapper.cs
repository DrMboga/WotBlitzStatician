namespace WotBlitzStatician.WotApiClient.Mappers
{
	using AutoMapper;
	using WotBlitzStatician.Model;
	using WotBlitzStatician.Model.MapperLogic;
	using WotBlitzStatician.WotApiClient.InternalModel;

	internal class AccountInfoMapper : IMapper<WotAccountInfoResponse, AccountInfo>
	{
        private readonly IMapper _mapper;

		public AccountInfoMapper()
		{
            _mapper = new Mapper(new MapperConfiguration(m => m.CreateMap<WotAccountInfoResponse, AccountInfo>()
               .ForMember(dest => dest.AccountCreatedAt, op => op.MapFrom(s => s.CreatedAt))));
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
