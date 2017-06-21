﻿namespace WotBlitzStatician.WotApiClient.Mappers
{
	using AutoMapper;
	using WotBlitzStatician.WotApiClient.InternalModel;
	using WotBlitzStaticitian.Model;

	internal class AccounutFindResponseMapper : IMapper<WotAccountListResponse, AccountInfo>
	{
		public AccounutFindResponseMapper()
		{
			Mapper.Initialize(m => m.CreateMap<WotAccountListResponse, AccountInfo>()
				.ForMember(dest => dest.AccountId, op => op.MapFrom(s => s.AccountId))
				.ForMember(dest => dest.NickName, op => op.MapFrom(s => s.Nickname))
			);

		}

		public AccountInfo Map(WotAccountListResponse source)
		{
			return Mapper.Map<WotAccountListResponse, AccountInfo>(source);
		}

		public AccountInfo Map(WotAccountListResponse source, AccountInfo destination)
		{
			return Mapper.Map(source, destination);
		}
	}
}