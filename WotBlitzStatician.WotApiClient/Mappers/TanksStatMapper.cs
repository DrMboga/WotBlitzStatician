namespace WotBlitzStatician.WotApiClient.Mappers
{
	using AutoMapper;
	using WotBlitzStatician.Model;
	using WotBlitzStatician.WotApiClient.InternalModel;

	internal class TanksStatMapper : IMapper<WotAccountTanksStatResponse, AccountTankStatistics>
	{
		private readonly IMapper _mapper;

		public TanksStatMapper()
		{
			_mapper = new Mapper(new MapperConfiguration(m => m.CreateMap<WotAccountTanksStatResponse, AccountTankStatistics>()
				.ForMember(dest => dest.TankId, op => op.MapFrom(s => s.TankId))
				.ForMember(dest => dest.BattleLifeTime, op => op.MapFrom(s => s.BattleLifeTime))
				.ForMember(dest => dest.LastBattleTime, op => op.MapFrom(s => s.LastBattleTime))
				.ForMember(dest => dest.MarkOfMastery, op => op.MapFrom(s => (int)s.MarkOfMastery))
				.ForMember(dest => dest.Battles, op => op.MapFrom(s => s.All.Battles))
				.ForMember(dest => dest.CapturePoints, op => op.MapFrom(s => s.All.CapturePoints))
				.ForMember(dest => dest.DamageDealt, op => op.MapFrom(s => s.All.DamageDealt))
				.ForMember(dest => dest.DamageReceived, op => op.MapFrom(s => s.All.DamageReceived))
				.ForMember(dest => dest.DroppedCapturePoints, op => op.MapFrom(s => s.All.DroppedCapturePoints))
				.ForMember(dest => dest.Frags, op => op.MapFrom(s => s.All.Frags))
				.ForMember(dest => dest.Frags8P, op => op.MapFrom(s => s.All.Frags8P))
				.ForMember(dest => dest.Hits, op => op.MapFrom(s => s.All.Hits))
				.ForMember(dest => dest.Losses, op => op.MapFrom(s => s.All.Losses))
				.ForMember(dest => dest.MaxFrags, op => op.MapFrom(s => s.All.MaxFrags))
				.ForMember(dest => dest.MaxXp, op => op.MapFrom(s => s.All.MaxXp))
				.ForMember(dest => dest.Shots, op => op.MapFrom(s => s.All.Shots))
				.ForMember(dest => dest.Spotted, op => op.MapFrom(s => s.All.Spotted))
				.ForMember(dest => dest.SurvivedBattles, op => op.MapFrom(s => s.All.SurvivedBattles))
				.ForMember(dest => dest.WinAndSurvived, op => op.MapFrom(s => s.All.WinAndSurvived))
				.ForMember(dest => dest.Wins, op => op.MapFrom(s => s.All.Wins))
				.ForMember(dest => dest.Xp, op => op.MapFrom(s => s.All.Xp))
                                                        ));
		}

		public AccountTankStatistics Map(WotAccountTanksStatResponse source)
		{
			return _mapper.Map<WotAccountTanksStatResponse, AccountTankStatistics>(source);
		}

		public AccountTankStatistics Map(WotAccountTanksStatResponse source, AccountTankStatistics destination)
		{
			return _mapper.Map(source, destination);
		}
	}
}
