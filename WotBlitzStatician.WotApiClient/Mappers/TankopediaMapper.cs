namespace WotBlitzStatician.WotApiClient.Mappers
{
	using System.Collections.Generic;
	using AutoMapper;
	using WotBlitzStatician.Model;
	using WotBlitzStatician.Model.MapperLogic;
	using WotBlitzStatician.WotApiClient.InternalModel;

	internal class TankopediaMapper : IMapper<List<WotEncyclopediaVehiclesResponse>, List<VehicleEncyclopedia>>
	{
		private readonly IMapper _mapper;

		public TankopediaMapper()
        {
            _mapper = new Mapper(new MapperConfiguration(m => m.CreateMap<WotEncyclopediaVehiclesResponse, VehicleEncyclopedia>()
                .ForMember(dest => dest.PriceCredit, op => op.MapFrom(s => s.Cost["price_credit"]))
                .ForMember(dest => dest.PriceGold, op => op.MapFrom(s => s.Cost["price_gold"]))
                .ForMember(dest => dest.PreviewImageUrl, op => op.MapFrom(s => s.Images["preview"]))
                .ForMember(dest => dest.NormalImageUrl, op => op.MapFrom(s => s.Images["normal"]))
                                                        ));

        }

		public List<VehicleEncyclopedia> Map(List<WotEncyclopediaVehiclesResponse> source)
		{
			return _mapper.Map< List<WotEncyclopediaVehiclesResponse>, List<VehicleEncyclopedia>>(source);
		}

		public List<VehicleEncyclopedia> Map(List<WotEncyclopediaVehiclesResponse> source, List<VehicleEncyclopedia> destination)
		{
			return _mapper.Map(source, destination);
		}
	}
}
