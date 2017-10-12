namespace WotBlitzStatician.Mappers
{
    using System;
    using AutoMapper;
	using WotBlitzStatician.Logic.Dto;
    using WotBlitzStatician.Model.MapperLogic;
    using WotBlitzStatician.ViewModel;
    using WotBlitzStatician.ViewModel.ValueDelta;

	public class TankDeltaMapper : IMapper<BlitzTankInfoDelta, TankDeltaViewModel>
	{
		private readonly IMapper _mapper;

		public TankDeltaMapper()
		{
			_mapper = new Mapper(new MapperConfiguration(m =>
			{
				m.CreateMap<ValueDelta<long, long>, LongDeltaModel>();
				m.CreateMap<ValueDelta<DateTime, TimeSpan>, DateDeltaModel>();
				m.CreateMap<ValueDelta<decimal, decimal>, DecimalDeltaModel>()
					.ForMember(d => d.Delta, o => o.ResolveUsing(s => $"{(s.IsNegative ? "-" : "+")}{s.Delta:N2}"));
				m.CreateMap<StatisticsDelta, StatisticsViewModel>();
				m.CreateMap<BlitzTankInfoDto, TankInfoVewModel> ()
			        .ForMember(dest => dest.RomanTier, op => op.MapFrom(s => Convert.ToInt32(s.Tier).ToRomanNumeral()));

			    m.CreateMap<BlitzTankInfoDelta, TankDeltaViewModel>();
			}));

		}

		public TankDeltaViewModel Map(BlitzTankInfoDelta source)
		{
			return _mapper.Map<BlitzTankInfoDelta, TankDeltaViewModel>(source);
		}

		public TankDeltaViewModel Map(BlitzTankInfoDelta source, TankDeltaViewModel destination)
		{
			return _mapper.Map(source, destination);
		}

	}
}