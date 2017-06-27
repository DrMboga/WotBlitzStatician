namespace WotBlitzStatician.WotApiClient.Mappers
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using WotBlitzStatician.Model;

    internal class DictionaryVehicleTypeMapper : IMapper<KeyValuePair<string, string>, DictionaryVehicleType>
    {
        public DictionaryVehicleTypeMapper()
        {
			Mapper.Initialize(m => m.CreateMap<KeyValuePair<string, string>, DictionaryVehicleType>()
                              .ForMember(dest => dest.VehicleTypeId, op => op.MapFrom(s => s.Key))
                              .ForMember(dest => dest.VehicleTypeName, op => op.MapFrom(svm => svm.Value)));
        }

        public DictionaryVehicleType Map(KeyValuePair<string, string> source)
        {
            return Mapper.Map<KeyValuePair<string, string>, DictionaryVehicleType>(source);
        }

        public DictionaryVehicleType Map(KeyValuePair<string, string> source, DictionaryVehicleType destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}