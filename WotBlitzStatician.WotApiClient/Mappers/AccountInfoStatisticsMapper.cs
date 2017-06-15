namespace WotBlitzStatician.WotApiClient.Mappers
{
	using AutoMapper;
	using WotBlitzStatician.WotApiClient.InternalModel;
	using WotBlitzStaticitian.Model;

	internal class AccountInfoStatisticsMapper : IMapper<WotAccountInfoResponse, AccountInfoStatistics>
	{
		public AccountInfoStatisticsMapper()
		{
			Mapper.Initialize(m => m.CreateMap<WotAccountInfoResponse, AccountInfoStatistics>()
				.ForMember(dest => dest.UpdatedAt, op => op.MapFrom(s => s.UpdatedAt))
				.ForMember(dest => dest.AccountId, op => op.MapFrom(s => s.AccountId))
				.ForMember(dest => dest.Battles, op => op.MapFrom(s => s.Statistics.All.Battles))
				.ForMember(dest => dest.CapturePoints, op => op.MapFrom(s => s.Statistics.All.CapturePoints))
				.ForMember(dest => dest.DamageDealt, op => op.MapFrom(s => s.Statistics.All.DamageDealt))
				.ForMember(dest => dest.DamageReceived, op => op.MapFrom(s => s.Statistics.All.DamageReceived))
				.ForMember(dest => dest.DroppedCapturePoints, op => op.MapFrom(s => s.Statistics.All.DroppedCapturePoints))
				.ForMember(dest => dest.Frags, op => op.MapFrom(s => s.Statistics.All.Frags))
				.ForMember(dest => dest.Frags8P, op => op.MapFrom(s => s.Statistics.All.Frags8P))
				.ForMember(dest => dest.Hits, op => op.MapFrom(s => s.Statistics.All.Hits))
				.ForMember(dest => dest.Losses, op => op.MapFrom(s => s.Statistics.All.Losses))
				.ForMember(dest => dest.MaxFrags, op => op.MapFrom(s => s.Statistics.All.MaxFrags))
				.ForMember(dest => dest.MaxFragsTankId, op => op.MapFrom(s => s.Statistics.All.MaxFragsTankId))
				.ForMember(dest => dest.MaxXp, op => op.MapFrom(s => s.Statistics.All.MaxXp))
				.ForMember(dest => dest.MaxXpTankId, op => op.MapFrom(s => s.Statistics.All.MaxXpTankId))
				.ForMember(dest => dest.Shots, op => op.MapFrom(s => s.Statistics.All.Shots))
				.ForMember(dest => dest.Spotted, op => op.MapFrom(s => s.Statistics.All.Spotted))
				.ForMember(dest => dest.SurvivedBattles, op => op.MapFrom(s => s.Statistics.All.SurvivedBattles))
				.ForMember(dest => dest.WinAndSurvived, op => op.MapFrom(s => s.Statistics.All.WinAndSurvived))
				.ForMember(dest => dest.Wins, op => op.MapFrom(s => s.Statistics.All.Wins))
				.ForMember(dest => dest.Xp, op => op.MapFrom(s => s.Statistics.All.Xp))
			);
		}

		public AccountInfoStatistics Map(WotAccountInfoResponse source)
		{
			return Mapper.Map<WotAccountInfoResponse, AccountInfoStatistics>(source);
		}

		public AccountInfoStatistics Map(WotAccountInfoResponse source, AccountInfoStatistics destination)
		{
			return Mapper.Map(source, destination);
		}
	}
}