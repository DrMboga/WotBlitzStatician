namespace WotBlitzStatician.WotApiClient.Mappers
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using WotBlitzStatician.Model;

    internal class DictionaryVehicleTypeMapper : IMapper<KeyValuePair<string, string>, DictionaryVehicleType>
    {
		private readonly IMapper _mapper;

		public DictionaryVehicleTypeMapper()
        {
			_mapper = new Mapper(new MapperConfiguration(m => m.CreateMap<KeyValuePair<string, string>, DictionaryVehicleType>()
                              .ForMember(dest => dest.VehicleTypeId, op => op.MapFrom(s => s.Key))
                                                         .ForMember(dest => dest.VehicleTypeName, op => op.MapFrom(svm => svm.Value))));
        }

        public DictionaryVehicleType Map(KeyValuePair<string, string> source)
        {
            return _mapper.Map<KeyValuePair<string, string>, DictionaryVehicleType>(source);
        }

        public DictionaryVehicleType Map(KeyValuePair<string, string> source, DictionaryVehicleType destination)
        {
            return _mapper.Map(source, destination);
        }
    }
}