namespace WotBlitzStatician.Mappers
{
    using AutoMapper;
    using WotBlitzStatician.Logic.Dto;
    using WotBlitzStatician.ViewModel;

    public class AccountInfoDeltaMapper
    {
		private readonly IMapper _mapper;

        public AccountInfoDeltaMapper()
        {
            _mapper = new Mapper(new MapperConfiguration(
                m => m.CreateMap<BlitzAccountInfoStatisticsDelta, AccountInfoDeltaViewModel>()
                        .ForMember(dest => dest.AccountId, op => op.MapFrom(s => s.AccountId))
            ));

        }

		public AccountInfoDeltaViewModel Map(BlitzAccountInfoStatisticsDelta source)
		{
			return _mapper.Map<BlitzAccountInfoStatisticsDelta, AccountInfoDeltaViewModel>(source);
		}

		public AccountInfoDeltaViewModel Map(BlitzAccountInfoStatisticsDelta source, AccountInfoDeltaViewModel destination)
		{
			return _mapper.Map(source, destination);
		}

	}
}
