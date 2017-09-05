﻿namespace WotBlitzStatician.WotApiClient.Mappers
{
    using System;
    using AutoMapper;
    using WotBlitzStatician.Model;
    using WotBlitzStatician.WotApiClient.InternalModel;

    internal class ClanInfoResponseMapper : IMapper<WotClanInfoResponse, AccountClanInfo>
    {
		private readonly IMapper _mapper;

		public ClanInfoResponseMapper()
        {
			_mapper = new Mapper(new MapperConfiguration(m => m.CreateMap<WotClanInfoResponse, AccountClanInfo>()
							  .ForMember(dest => dest.ClanDescription, op => op.MapFrom(s => s.Description))
                              .ForMember(dest => dest.ClanCreatedAt, op => op.MapFrom(s => s.CreatedAt))
                              .ForMember(dest => dest.ClanId, op => op.MapFrom(s => s.ClanId))
                              .ForMember(dest => dest.ClanLeaderName, op => op.MapFrom(s => s.LeaderName))
                              .ForMember(dest => dest.ClanMotto, op => op.MapFrom(s => s.Motto))
                              .ForMember(dest => dest.ClanName, op => op.MapFrom(s => s.Name))
                              .ForMember(dest => dest.ClanTag, op => op.MapFrom(s => s.Tag))
                              .ForMember(dest => dest.MembersCount, op => op.MapFrom(s => s.MembersCount))
                                                        ));

		}

        public AccountClanInfo Map(WotClanInfoResponse source)
        {
            return _mapper.Map<WotClanInfoResponse, AccountClanInfo>(source);
        }

        public AccountClanInfo Map(WotClanInfoResponse source, AccountClanInfo destination)
        {
            return _mapper.Map(source, destination);
        }
    }
}
