using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Linq;
using WotBlitzStatician.Model;
using WotBlitzStatician.Model.Dto;
using WotBlitzStatician.Model.MapperLogic;

namespace WotBlitzStatician.Data.Mappers
{
	public class PlayerStatisticsDtoMapper : IQueryableMapper<AccountInfoStatistics, PlayerStatDto>
	{
		private readonly IMapper _mapper;

		public PlayerStatisticsDtoMapper()
		{
			_mapper = new Mapper(new MapperConfiguration(m => 
			m.CreateMap<AccountInfoStatistics, PlayerStatDto>()
				.ForMember(d => d.BattleLifeTimeInSeconds, 
						o => o.MapFrom(s => s.AccountInfoPrivate == null 
							? (int?) null 
							: s.AccountInfoPrivate.BattleLifeTimeInSeconds))
				.ForMember(d => d.Credits,
						o => o.MapFrom(s => s.AccountInfoPrivate == null
							? (long?)null
							: s.AccountInfoPrivate.Credits))
				.ForMember(d => d.FreeXp,
						o => o.MapFrom(s => s.AccountInfoPrivate == null
							? (long?)null
							: s.AccountInfoPrivate.FreeXp))
				.ForMember(d => d.Gold,
						o => o.MapFrom(s => s.AccountInfoPrivate == null
							? (long?)null
							: s.AccountInfoPrivate.Gold))
				.ForMember(d => d.IsPremium,
						o => o.MapFrom(s => s.AccountInfoPrivate == null
							? (bool?)null
							: s.AccountInfoPrivate.IsPremium))
				.ForMember(d => d.PremiumExpiresAt,
						o => o.MapFrom(s => s.AccountInfoPrivate == null
							? (DateTime?)null
							: s.AccountInfoPrivate.PremiumExpiresAt))
				));
		}

        public IQueryable<PlayerStatDto> ProjectTo(IQueryable<AccountInfoStatistics> source)
        {
			return source.ProjectTo<PlayerStatDto>(_mapper.ConfigurationProvider);
        }

		public PlayerStatDto Map(AccountInfoStatistics source)
		{
			return _mapper.Map<AccountInfoStatistics, PlayerStatDto>(source);
		}

		public PlayerStatDto Map(AccountInfoStatistics source, PlayerStatDto destination)
		{
			return _mapper.Map(source, destination);
		}
    }
}
