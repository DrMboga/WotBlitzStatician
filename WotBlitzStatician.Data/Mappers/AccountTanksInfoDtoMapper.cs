using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using WotBlitzStatician.Model;
using WotBlitzStatician.Model.Common;
using WotBlitzStatician.Model.Dto;
using WotBlitzStatician.Model.MapperLogic;

namespace WotBlitzStatician.Data.Mappers
{
	public class AccountTanksInfoDtoMapper : IQueryableMapper<AccountTanksStatisticsTuple, AccountTankInfoDto>
	{
		private readonly IMapper _mapper;

		public AccountTanksInfoDtoMapper(Dictionary<MarkOfMastery, string> masteryImages)
		{
			_mapper = new Mapper(new MapperConfiguration(m =>
			  m.CreateMap<AccountTanksStatisticsTuple, AccountTankInfoDto>()
				.ForMember(d => d.VehicleTier, o => o.MapFrom(s => Convert.ToInt32(s.Vehicle.Tier)))
				.ForMember(d => d.TankTierRoman, o => o.MapFrom(s => Convert.ToInt32(s.Vehicle.Tier).ToRomanNumeral()))
				.ForMember(d => d.PreviewLocalImage, o => o.MapFrom(s => s.Vehicle.PreviewImageUrl.MakeImagePathLocal()))
				.ForMember(d => d.NormalLocalImage, o => o.MapFrom(s => s.Vehicle.NormalImageUrl.MakeImagePathLocal()))
				.ForMember(d => d.MasteryImage, o => o.MapFrom(s => masteryImages[s.Tank.MarkOfMastery]))
				.ForMember(d => d.MasteryLocalImage, o => o.MapFrom(s => masteryImages[s.Tank.MarkOfMastery].MakeImagePathLocal()))
			));
		}

		// ToDo: Create a derived interface with this method: IQueryable<TDestination> ProjectTo<TSource>(IQueryable<TSource> source)
		public IQueryable<AccountTankInfoDto> ProjectTo(IQueryable<AccountTanksStatisticsTuple> source)
		{
			return source.ProjectTo<AccountTankInfoDto>(_mapper.ConfigurationProvider);
		}

		public AccountTankInfoDto Map(AccountTanksStatisticsTuple source)
		{
			return _mapper.Map<AccountTanksStatisticsTuple, AccountTankInfoDto>(source);
		}

		public AccountTankInfoDto Map(AccountTanksStatisticsTuple source, AccountTankInfoDto destination)
		{
			return _mapper.Map(source, destination);
		}
	}
}
