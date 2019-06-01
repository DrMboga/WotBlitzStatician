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
      m.CreateMap<AccountInfoStatistics, PlayerStatDto>()));
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
