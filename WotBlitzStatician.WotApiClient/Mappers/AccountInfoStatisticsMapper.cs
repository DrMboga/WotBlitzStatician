namespace WotBlitzStatician.WotApiClient.Mappers
{
	using AutoMapper;
	using WotBlitzStatician.Model;
	using WotBlitzStatician.Model.MapperLogic;
	using WotBlitzStatician.WotApiClient.InternalModel;

	internal class AccountInfoStatisticsMapper : IMapper<WotAccountInfoResponse, AccountInfoStatistics>
	{
		private readonly IMapper _mapper;

		public AccountInfoStatisticsMapper()
		{
			_mapper = new Mapper(new MapperConfiguration(
				m =>
				{
					m.CreateMap<WotAccountInfoStatisticsAll, AccountInfoStatistics>();
					m.CreateMap<WotAccountInfoResponse, AccountInfoStatistics>();
				}));
		}

		public AccountInfoStatistics Map(WotAccountInfoResponse source)
		{
			var resp = _mapper.Map<WotAccountInfoResponse, AccountInfoStatistics>(source);
			return _mapper.Map(source.Statistics.All, resp);
		}

		public AccountInfoStatistics Map(WotAccountInfoResponse source, AccountInfoStatistics destination)
		{
			var resp = _mapper.Map(source, destination);
			return _mapper.Map(source.Statistics.All, resp);
		}
	}
}
