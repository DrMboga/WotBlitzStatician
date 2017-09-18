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
	        _mapper = new Mapper(new MapperConfiguration(m =>
		        m.CreateMap<BlitzAccountInfoStatisticsDelta, AccountInfoDeltaViewModel>()
			        .ForMember(dest => dest.AccountId, op => op.MapFrom(s => s.AccountId))
			        .ForMember(dest => dest.PastBattles, op => op.MapFrom(s => s.Battles.PastValue))
			        .ForMember(dest => dest.BattlesDelta, op => op.MapFrom(s => s.Battles.Delta))
			        .ForMember(dest => dest.PresentUpdatedAt, op => op.MapFrom(s => s.UpdatedAt.PresentValue))
			        .ForMember(dest => dest.PastUpdatedAt, op => op.MapFrom(s => s.UpdatedAt.PastValue))
			        .ForMember(dest => dest.UpdatedAtDelta, op => op.MapFrom(s => s.UpdatedAt.Delta))
			        .ForMember(dest => dest.PastWins, op => op.MapFrom(s => s.Wins.PastValue))
			        .ForMember(dest => dest.WinsDelta, op => op.MapFrom(s => s.Wins.Delta))
			        .ForMember(dest => dest.PastWinrate, op => op.MapFrom(s => s.Winrate.PastValue))
			        .ForMember(dest => dest.WinrateDelta,
				        op => op.ResolveUsing(s => $"{(s.Winrate.IsNegative ? "-" : "+")}{s.Winrate.Delta:N2}%"))
			        .ForMember(dest => dest.PastAvgDamage, op => op.MapFrom(s => s.AvgDamage.PastValue))
			        .ForMember(dest => dest.AvgDamageDelta,
				        op => op.ResolveUsing(s => $"{(s.AvgDamage.IsNegative ? "-" : "+")}{s.AvgDamage.Delta:N2}"))
			        .ForMember(dest => dest.PastAvgXp, op => op.MapFrom(s => s.AvgXp.PastValue))
			        .ForMember(dest => dest.AvgXpDelta,
				        op => op.ResolveUsing(s => $"{(s.AvgXp.IsNegative ? "-" : "+")}{s.AvgXp.Delta:N2}"))
			        .ForMember(dest => dest.PastWn7, op => op.MapFrom(s => s.Wn7.PastValue))
			        .ForMember(dest => dest.Wn7Delta,
				        op => op.ResolveUsing(s => $"{(s.Wn7.IsNegative ? "-" : "+")}{s.Wn7.Delta:N2}"))
			        .ForMember(dest => dest.PastAvgTier, op => op.MapFrom(s => s.AvgTier.PastValue))
			        .ForMember(dest => dest.AvgTierDelta,
				        op => op.ResolveUsing(s => $"{(s.AvgTier.IsNegative ? "-" : "+")}{s.AvgTier.Delta:N2}"))
			        .ForMember(dest => dest.PastEffectivity, op => op.MapFrom(s => s.Effectivity.PastValue))
			        .ForMember(dest => dest.EffectivityDelta,
				        op => op.ResolveUsing(s => $"{(s.Effectivity.IsNegative ? "-" : "+")}{s.Effectivity.Delta:N2}"))
			        .ForMember(dest => dest.IntervalWinrate, op => op.MapFrom(s => s.IntervalWinrate))
			        .ForMember(dest => dest.IntervalAvgDamage, op => op.MapFrom(s => s.IntervalAvgDamage))
			        .ForMember(dest => dest.IntervalAvgXp, op => op.MapFrom(s => s.IntervalAvgXp))
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
