namespace WotBlitzStatician.WotApiClient.Mappers
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using WotBlitzStatician.Model;
    using WotBlitzStatician.Model.MapperLogic;

	internal class DictionaryVehicleTypeMapper : IMapper<Dictionary<string, string>, List<DictionaryVehicleType>>
    {
		private readonly IMapper _mapper;

		public DictionaryVehicleTypeMapper()
        {
			_mapper = new Mapper(new MapperConfiguration(m => m.CreateMap<KeyValuePair<string, string>, DictionaryVehicleType>()
                              .ForMember(dest => dest.VehicleTypeId, op => op.MapFrom(s => s.Key))
                              .ForMember(dest => dest.VehicleTypeName, op => op.MapFrom(svm => svm.Value))));
        }

        public List<DictionaryVehicleType> Map(Dictionary<string, string> source)
        {
            return _mapper.Map<Dictionary<string, string>, List<DictionaryVehicleType>>(source);
        }

        public List<DictionaryVehicleType> Map(Dictionary<string, string> source, List<DictionaryVehicleType> destination)
        {
            return _mapper.Map(source, destination);
        }
    }
}