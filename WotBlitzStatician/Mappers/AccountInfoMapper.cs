namespace WotBlitzStatician.Mappers
{
    using AutoMapper;
    using WotBlitzStatician.Model;
    using WotBlitzStatician.Model.MapperLogic;
    using WotBlitzStatician.ViewModel;

    public class AccountInfoMapper : IMapper<AccountInfo, AccountInfoViewModel>
    {
        private readonly IMapper _mapper;

        public AccountInfoMapper()
        {
	        _mapper = new Mapper(new MapperConfiguration(
				m =>
				{
					m.CreateMap<AccountInfo, AccountInfoViewModel>();
					m.CreateMap<AccountClanInfo, AccountClanInfoViewModel>();
				}));


        }

		public AccountInfoViewModel Map(AccountInfo source)
		{
			return _mapper.Map<AccountInfo, AccountInfoViewModel>(source);
		}

		public AccountInfoViewModel Map(AccountInfo source, AccountInfoViewModel destination)
		{
			return _mapper.Map(source, destination);
		}

	}
}
