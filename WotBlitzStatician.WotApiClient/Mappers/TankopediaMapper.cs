namespace WotBlitzStatician.WotApiClient.Mappers
{
	using AutoMapper;
	using WotBlitzStatician.Model;
	using WotBlitzStatician.WotApiClient.InternalModel;

	internal class TankopediaMapper : IMapper<WotEncyclopediaVehiclesResponse, VehicleEncyclopedia>
	{
		private readonly IMapper _mapper;

		public TankopediaMapper()
        {
            _mapper = new Mapper(new MapperConfiguration(m => m.CreateMap<WotEncyclopediaVehiclesResponse, VehicleEncyclopedia>()
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
                .ForMember(dest => dest.PreviewImageUrl, op => op.MapFrom(s => s.Images["preview"]))
                .ForMember(dest => dest.NormalImageUrl, op => op.MapFrom(s => s.Images["normal"]))
                                                        ));

        }

		public VehicleEncyclopedia Map(WotEncyclopediaVehiclesResponse source)
		{
			return _mapper.Map<WotEncyclopediaVehiclesResponse, VehicleEncyclopedia>(source);
		}

		public VehicleEncyclopedia Map(WotEncyclopediaVehiclesResponse source, VehicleEncyclopedia destination)
		{
			return _mapper.Map(source, destination);
		}
	}
}
