namespace WotBlitzStatician.Mappers
{
    using AutoMapper;
    using WotBlitzStatician.Model;
    using WotBlitzStatician.ViewModel;

    public class AccountInfoMapper
    {
        private readonly IMapper _mapper;

        public AccountInfoMapper()
        {
	        _mapper = new Mapper(new MapperConfiguration(m => m.CreateMap<AccountInfo, AccountInfoViewModel>()
		        .ForMember(dest => dest.AccountId, op => op.MapFrom(s => s.AccountId))
		        .ForMember(dest => dest.NickName, op => op.MapFrom(s => s.NickName))
		        .ForMember(dest => dest.LastBattleTime, op => op.MapFrom(s => s.LastBattleTime))
		        .ForMember(dest => dest.Battles, op => op.MapFrom(s => s.AccountInfoStatistics.Battles))
		        .ForMember(dest => dest.Wins, op => op.MapFrom(s => s.AccountInfoStatistics.Wins))
		        .ForMember(dest => dest.Winrate, op => op
			        .MapFrom(s => (decimal) s.AccountInfoStatistics.Wins * 100 / s.AccountInfoStatistics.Battles))
		        .ForMember(dest => dest.WinrateGrade, op => op.MapFrom(s => ((decimal)s.AccountInfoStatistics.Wins * 100 / s.AccountInfoStatistics.Battles).GetWinrateGrade()))
				.ForMember(dest => dest.Wn7, op => op.MapFrom(s => s.AccountInfoStatistics.Wn7))
		        .ForMember(dest => dest.Wn7Grade, op => op.MapFrom(s => s.AccountInfoStatistics.Wn7.GetWn7Grade()))
		        .ForMember(dest => dest.AvgDamage, op => op
			        .MapFrom(s => s.AccountInfoStatistics.DamageDealt / s.AccountInfoStatistics.Battles))
		        .ForMember(dest => dest.AvgXp, op => op
			        .MapFrom(s => s.AccountInfoStatistics.Xp / s.AccountInfoStatistics.Battles))
		        .ForMember(dest => dest.AvgTier, op => op.MapFrom(s => s.AccountInfoStatistics.AvgTier))
	        ));


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
