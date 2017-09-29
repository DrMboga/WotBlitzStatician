namespace WotBlitzStatician.WotApiClient.Mappers
{
	using System.Collections.Generic;
	using AutoMapper;
	using WotBlitzStatician.Model;
	using WotBlitzStatician.Model.MapperLogic;
	using WotBlitzStatician.WotApiClient.InternalModel;

	internal class AccounutFindResponseMapper : IMapper<List<WotAccountListResponse>, List<AccountInfo>>
	{
		private readonly IMapper _mapper;

		public AccounutFindResponseMapper()
		{
			_mapper = new Mapper(new MapperConfiguration(m => m.CreateMap<WotAccountListResponse, AccountInfo>()));

		}

		public List<AccountInfo> Map(List<WotAccountListResponse> source)
		{
			return _mapper.Map<List<WotAccountListResponse>, List<AccountInfo>>(source);
		}

		public List<AccountInfo> Map(List<WotAccountListResponse> source, List<AccountInfo> destination)
		{
			return _mapper.Map(source, destination);
		}
	}
}
