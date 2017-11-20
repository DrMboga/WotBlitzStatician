namespace WotBlitzStatician.Mappers
{
	using System;
	using AutoMapper;
    using WotBlitzStatician.Logic.Dto;
	using WotBlitzStatician.Model.MapperLogic;
	using WotBlitzStatician.ViewModel;
	using WotBlitzStatician.ViewModel.ValueDelta;

	public class AccountInfoDeltaMapper : IMapper<BlitzAccountInfoStatisticsDelta, AccountInfoDeltaViewModel>
	{
		private readonly IMapper _mapper;

        public AccountInfoDeltaMapper()
        {
	        _mapper = new Mapper(new MapperConfiguration(m =>
		        {
			        m.CreateMap<ValueDelta<long, long>, LongDeltaModel>();
			        m.CreateMap<ValueDelta<DateTime, TimeSpan>, DateDeltaModel>();
			        m.CreateMap<ValueDelta<TimeSpan, TimeSpan>, TimeDeltaModel>();
			        m.CreateMap<ValueDelta<decimal, decimal>, DecimalDeltaModel>()
				        .ForMember(d => d.Delta, o => o.ResolveUsing(s => $"{(s.IsNegative ? "-" : "+")}{s.Delta:N2}"));
			        m.CreateMap<StatisticsDelta, StatisticsViewModel>();
			        m.CreateMap<BlitzAccountInfoStatisticsDelta, AccountInfoDeltaViewModel>();
		        }
	        ));

        }

		public AccountInfoDeltaViewModel Map(BlitzAccountInfoStatisticsDelta source)
		{
			return _mapper.Map<BlitzAccountInfoStatisticsDelta, AccountInfoDeltaViewModel>(source);
		}

		public AccountInfoDeltaViewModel Map(BlitzAccountInfoStatisticsDelta source, AccountInfoDeltaViewModel destination)
		{
			return _mapper.Map(source, destination);
		}

	}
}
