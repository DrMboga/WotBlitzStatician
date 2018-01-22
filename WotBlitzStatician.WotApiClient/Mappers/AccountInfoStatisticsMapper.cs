namespace WotBlitzStatician.WotApiClient.Mappers
{
	using System.Collections.Generic;
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
					m.CreateMap<KeyValuePair<string, string>, FragListItem>()
						.ForMember(d => d.KilledTankId, o => o.MapFrom(s => long.Parse(s.Key)))
						.ForMember(d => d.FragsCount, o => o.MapFrom(s => int.Parse(s.Value)));
					m.CreateMap<WotAccountInfoStatistics, AccountInfoStatistics>()
						.ForMember(d => d.FragsList, o => o.MapFrom(s => s.Frags));
					m.CreateMap<WotAccountInfoStatisticsAll, AccountInfoStatistics>();
					m.CreateMap<WotAccountInfoResponse, AccountInfoStatistics>();
				}));
		}

		public AccountInfoStatistics Map(WotAccountInfoResponse source)
		{
			var resp = _mapper.Map<WotAccountInfoResponse, AccountInfoStatistics>(source);
			_mapper.Map(source.Statistics, resp);
			resp.FragsList?.ForEach(f => f.AccountId = resp.AccountId);
			return _mapper.Map(source.Statistics.All, resp);
		}

		public AccountInfoStatistics Map(WotAccountInfoResponse source, AccountInfoStatistics destination)
		{
			var resp = _mapper.Map(source, destination);
			_mapper.Map(source.Statistics, resp);
			resp.FragsList?.ForEach(f => f.AccountId = resp.AccountId);
			return _mapper.Map(source.Statistics.All, resp);
		}
	}
}
