namespace WotBlitzStatician.Mappers
{
    using System;
    using AutoMapper;
	using WotBlitzStatician.Logic.Dto;
	using WotBlitzStatician.ViewModel;

	public class TankDeltaMapper
	{
		private readonly IMapper _mapper;

		public TankDeltaMapper()
		{
			_mapper = new Mapper(new MapperConfiguration(m =>
				m.CreateMap<BlitzTankInfoDelta, TankDeltaViewModel>()
					.ForMember(dest => dest.TankId, op => op.MapFrom(s => s.TankId))
					.ForMember(dest => dest.AccountId, op => op.MapFrom(s => s.AccountId))
					.ForMember(dest => dest.Name, op => op.MapFrom(s => s.Name))
					.ForMember(dest => dest.Tier, op => op.MapFrom(s => s.Tier))
                    .ForMember(dest => dest.RomanTier, op => op.MapFrom(s => Convert.ToInt32(s.Tier).ToRomanNumeral()))
					.ForMember(dest => dest.Nation, op => op.MapFrom(s => s.Nation))
					.ForMember(dest => dest.Type, op => op.MapFrom(s => s.Type))
					.ForMember(dest => dest.IsPremium, op => op.MapFrom(s => s.IsPremium))
					.ForMember(dest => dest.PreviewImageUrl, op => op.MapFrom(s => s.PreviewImageUrl))
					.ForMember(dest => dest.NormalImageUrl, op => op.MapFrom(s => s.NormalImageUrl))
					.ForMember(dest => dest.MarkOfMastery, op => op.MapFrom(s => s.MarkOfMastery))
					.ForMember(dest => dest.MarkOfMasteryImageUrl, op => op.MapFrom(s => s.MarkOfMasteryImageUrl))
					.ForMember(dest => dest.PresentBattles, op => op.MapFrom(s => s.Battles.PresentValue))
					.ForMember(dest => dest.BattlesDelta, op => op.MapFrom(s => s.Battles.Delta))
					.ForMember(dest => dest.PresentLastBattle, op => op.MapFrom(s => s.LastBattle.PresentValue))
					.ForMember(dest => dest.PastLastBattle, op => op.MapFrom(s => s.LastBattle.PastValue))
					.ForMember(dest => dest.LastBattleDelta, op => op.MapFrom(s => s.LastBattle.Delta))
					.ForMember(dest => dest.PresentWins, op => op.MapFrom(s => s.Wins.PresentValue))
					.ForMember(dest => dest.WinsDelta, op => op.MapFrom(s => s.Wins.Delta))
					.ForMember(dest => dest.PresentWinrate, op => op.MapFrom(s => s.Winrate.PresentValue))
					.ForMember(dest => dest.WinrateDelta,
						op => op.ResolveUsing(s => $"{(s.Winrate.IsNegative ? "-" : "+")}{s.Winrate.Delta:N2}%"))
					.ForMember(dest => dest.WinrateGrade,
						op => op.MapFrom(s => s.Winrate.PresentValue.GetWinrateGrade()))
					.ForMember(dest => dest.PresentAvgDamage, op => op.MapFrom(s => s.AvgDamage.PresentValue))
					.ForMember(dest => dest.AvgDamageDelta,
						op => op.ResolveUsing(s => $"{(s.AvgDamage.IsNegative ? "-" : "+")}{s.AvgDamage.Delta:N2}"))
					.ForMember(dest => dest.PresentAvgXp, op => op.MapFrom(s => s.AvgXp.PresentValue))
					.ForMember(dest => dest.AvgXpDelta,
						op => op.ResolveUsing(s => $"{(s.AvgXp.IsNegative ? "-" : "+")}{s.AvgXp.Delta:N2}"))
					.ForMember(dest => dest.PresentWn7, op => op.MapFrom(s => s.Wn7.PresentValue))
					.ForMember(dest => dest.Wn7Delta,
						op => op.ResolveUsing(s => $"{(s.Wn7.IsNegative ? "-" : "+")}{s.Wn7.Delta:N2}"))
					.ForMember(dest => dest.Wn7Grade, op => op.MapFrom(s => s.Wn7.PresentValue.GetWn7Grade()))
					.ForMember(dest => dest.PresentEffectivity, op => op.MapFrom(s => s.Effectivity.PresentValue))
					.ForMember(dest => dest.EffectivityDelta,
						op => op.ResolveUsing(s => $"{(s.Effectivity.IsNegative ? "-" : "+")}{s.Effectivity.Delta:N2}"))
					));

		}

		public TankDeltaViewModel Map(BlitzTankInfoDelta source)
		{
			return _mapper.Map<BlitzTankInfoDelta, TankDeltaViewModel>(source);
		}

		public TankDeltaViewModel Map(BlitzTankInfoDelta source, TankDeltaViewModel destination)
		{
			return _mapper.Map(source, destination);
		}

	}
}