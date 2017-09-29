namespace WotBlitzStatician.WotApiClient.Mappers
{
	using AutoMapper;
    using AutoMapper.Configuration.Conventions;
	using WotBlitzStatician.Model;
    using WotBlitzStatician.Model.MapperLogic;
    using WotBlitzStatician.WotApiClient.InternalModel;

    internal class ClanInfoResponseMapper : IMapper<WotClanInfoResponse, AccountClanInfo>
    {
		private readonly IMapper _mapper;

		public ClanInfoResponseMapper()
        {
			_mapper = new Mapper(new MapperConfiguration(m =>
				{
					m.CreateMap<WotClanInfoResponse, AccountClanInfo>();
					m.AddMemberConfiguration()
						.AddName<PrePostfixName>(p => p.AddStrings(n => n.DestinationPrefixes, "Clan")); // Maps fields like s.Description to dest.ClanDescription
				}
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
