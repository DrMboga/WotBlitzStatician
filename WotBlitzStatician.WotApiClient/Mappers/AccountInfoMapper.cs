namespace WotBlitzStatician.WotApiClient.Mappers
{
	using AutoMapper;
	using WotBlitzStatician.WotApiClient.InternalModel;
	using WotBlitzStaticitian.Model;

	internal class AccountInfoMapper : IMapper<WotAccountInfoResponse, AccountInfo>
	{
		public AccountInfoMapper()
		{
			Mapper.Initialize(m => m.CreateMap<WotAccountInfoResponse, AccountInfo>()
				.ForMember(dest => dest.AccountId, op => op.MapFrom(s => s.AccountId))
				.ForMember(dest => dest.AccountCreatedAt, op => op.MapFrom(s => s.CreatedAt))
				.ForMember(dest => dest.NickName, op => op.MapFrom(s => s.Nickname))
				.ForMember(dest => dest.LastBattleTime, op => op.MapFrom(s => s.LastBattleTime))
			);
		}

		public AccountInfo Map(WotAccountInfoResponse source)
		{
			return Mapper.Map<WotAccountInfoResponse, AccountInfo>(source);
		}

		public AccountInfo Map(WotAccountInfoResponse source, AccountInfo destination)
		{
			return Mapper.Map(source, destination);
		}
	}
}