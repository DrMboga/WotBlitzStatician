using AutoMapper;
using WotBlitzStatician.Model;
using WotBlitzStatician.Model.MapperLogic;
using WotBlitzStatician.WotApiClient.InternalModel;

namespace WotBlitzStatician.WotApiClient.Mappers
{
	internal class WotAuthProlongateResponseMapper : IMapper<WotAuthProlongateResponse, AccountInfo>
	{
		private readonly IMapper _mapper;

		public WotAuthProlongateResponseMapper()
		{
			_mapper = new Mapper(
				new MapperConfiguration(m => m.CreateMap<WotAuthProlongateResponse, AccountInfo>()
				.ForMember(d => d.AccessTokenExpiration, r => r.MapFrom(s => s.ExpiresAt))
			   ));
		}

		public AccountInfo Map(WotAuthProlongateResponse source)
		{
			return _mapper.Map<WotAuthProlongateResponse, AccountInfo>(source);
		}

		public AccountInfo Map(WotAuthProlongateResponse source, AccountInfo destination)
		{
			return _mapper.Map(source, destination);
		}
	}
}
