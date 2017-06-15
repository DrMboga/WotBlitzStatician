namespace WotBlitzStatician.WotApiClient.Mappers
{
	using AutoMapper;
	using WotBlitzStatician.WotApiClient.InternalModel;
	using WotBlitzStaticitian.Model;

	internal class TankopediaMapper : IMapper<WotEncyclopediaVehiclesResponse, VehicleEncyclopedia>
	{

		public TankopediaMapper()
		{
			Mapper.Initialize(m => m.CreateMap<WotEncyclopediaVehiclesResponse, VehicleEncyclopedia>()
				.ForMember(dest => dest.TankId, op => op.MapFrom(s => s.TankId))
				.ForMember(dest => dest.Name, op => op.MapFrom(s => s.Name))
				.ForMember(dest => dest.Tier, op => op.MapFrom(s => s.Tier))
				.ForMember(dest => dest.Nation, op => op.MapFrom(s => s.Nation))
				.ForMember(dest => dest.Type, op => op.MapFrom(s => s.Type))
				.ForMember(dest => dest.Description, op => op.MapFrom(s => s.Description))
				.ForMember(dest => dest.IsGift, op => op.MapFrom(s => s.IsGift))
				.ForMember(dest => dest.IsPremium, op => op.MapFrom(s => s.IsPremium))
				.ForMember(dest => dest.PriceCredit, op => op.MapFrom(s => s.Cost["price_credit"]))
				.ForMember(dest => dest.PriceGold, op => op.MapFrom(s => s.Cost["price_gold"]))
				.ForMember(dest => dest.ShortName, op => op.MapFrom(s => s.ShortName))
				.ForMember(dest => dest.Tag, op => op.MapFrom(s => s.Tag))
			);

		}

		public VehicleEncyclopedia Map(WotEncyclopediaVehiclesResponse source)
		{
			return Mapper.Map<WotEncyclopediaVehiclesResponse, VehicleEncyclopedia>(source);
		}

		public VehicleEncyclopedia Map(WotEncyclopediaVehiclesResponse source, VehicleEncyclopedia destination)
		{
			return Mapper.Map(source, destination);
		}
	}
}