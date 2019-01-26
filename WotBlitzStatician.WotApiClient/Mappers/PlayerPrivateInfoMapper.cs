using AutoMapper;
using WotBlitzStatician.Model.MapperLogic;
using WotBlitzStatician.WotApiClient.InternalModel;

namespace WotBlitzStatician.WotApiClient.Mappers
{
    internal class PlayerPrivateInfoMapper : IMapper<WotAccountInfoPrivate, PlayerPrivateInfoDto>
    {
        private readonly IMapper _mapper;

        public PlayerPrivateInfoMapper()
        {
            _mapper = new Mapper(new MapperConfiguration(
				m => m.CreateMap<WotAccountInfoPrivate, PlayerPrivateInfoDto>()));
        }

        public PlayerPrivateInfoDto Map(WotAccountInfoPrivate source)
        {
			return _mapper.Map<WotAccountInfoPrivate, PlayerPrivateInfoDto>(source);
        }

        public PlayerPrivateInfoDto Map(WotAccountInfoPrivate source, PlayerPrivateInfoDto destination)
        {
            return _mapper.Map(source, destination);
        }
    }
}