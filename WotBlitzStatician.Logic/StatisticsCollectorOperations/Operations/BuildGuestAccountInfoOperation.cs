using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WotBlitzStatician.Data.DataAccessors;
using WotBlitzStatician.Data.Mappers;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;
using WotBlitzStatician.Model;
using WotBlitzStatician.Model.Common;
using WotBlitzStatician.Model.Dto;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations
{
  public class BuildGuestAccountInfoOperation : IStatisticsCollectorOperation
  {
    private readonly GuestAccountInfo _guestAccountInfo;
    private readonly IMapper _playerStatDtoMapper;
    private readonly IMapper _clanInfoMapper;
    private readonly IMapper _tanksMapper;

    private readonly IAccountsTankInfoDataAccessor _accountsTankInfoDataAccessor;
    private readonly IBlitzStaticianDictionary _dictionaryDataAccessor;


    public BuildGuestAccountInfoOperation(GuestAccountInfo guestAccountInfo,
            IAccountsTankInfoDataAccessor dataAccessor,
            IBlitzStaticianDictionary dictionayDataAccessor)
    {
      _guestAccountInfo = guestAccountInfo;
      _accountsTankInfoDataAccessor = dataAccessor;
      _dictionaryDataAccessor = dictionayDataAccessor;
      _playerStatDtoMapper = new Mapper(new MapperConfiguration(m =>
            m.CreateMap<AccountInfoStatistics, PlayerStatDto>()));
      _clanInfoMapper = new Mapper(new MapperConfiguration(m =>
            m.CreateMap<AccountClanInfo, PlayerClanInfoDto>()
                .ForMember(d => d.PlayerRole, o => o.MapFrom(s => _dictionaryDataAccessor.GetClanRole(s.PlayerRole).GetAwaiter().GetResult()))));

      _tanksMapper = new Mapper(new MapperConfiguration(m =>
            m.CreateMap<AccountTanksStatisticsTuple, AccountTankInfoDto>()
            .ForMember(d => d.VehicleTier, o => o.MapFrom(s => Convert.ToInt32(s.Vehicle.Tier)))
            .ForMember(d => d.TankTierRoman, o => o.MapFrom(s => Convert.ToInt32(s.Vehicle.Tier).ToRomanNumeral()))
            .ForMember(d => d.PreviewLocalImage, o => o.MapFrom(s => s.Vehicle.PreviewImageUrl.MakeImagePathLocal()))
            .ForMember(d => d.NormalLocalImage, o => o.MapFrom(s => s.Vehicle.NormalImageUrl.MakeImagePathLocal()))
            ));
    }

    public async Task Execute(StatisticsCollectorOperationContext operationContext)
    {
      var accountInfoFromWg = operationContext.Accounts.Single();

      _guestAccountInfo.AccountInfo = new AccountInfoDto
      {
        AccountId = accountInfoFromWg.WargamingAccountInfo.AccountId,
        NickName = accountInfoFromWg.WargamingAccountInfo.NickName,
        AccountCreatedAt = accountInfoFromWg.WargamingAccountInfo.AccountCreatedAt.Value,
        LastBattleTime = accountInfoFromWg.WargamingAccountInfo.LastBattleTime.Value
      };

      _guestAccountInfo.AccountInfo.PlayerStatistics =
                _playerStatDtoMapper.Map<AccountInfoStatistics, PlayerStatDto>(accountInfoFromWg.WargamingAccountInfo.AccountInfoStatistics.Single());

      await GetMaxXpAndFragsTanksInfo();
      GetAccountMasteryInfo(accountInfoFromWg.AccountInfoTanks);
      _guestAccountInfo.AccountInfo.PlayerClanInfo = _clanInfoMapper.Map<AccountClanInfo, PlayerClanInfoDto>(accountInfoFromWg.WargamingAccountInfo.AccountClanInfo);

      var (tanks, aggregated) = GetTanks(accountInfoFromWg.AccountInfoTanks);
      _guestAccountInfo.Tanks = tanks;
      _guestAccountInfo.AggregatedAccountInfo = aggregated.OrderBy(t => t.Tier).ToList();
      _guestAccountInfo.Achievements = GetAccountAchievements(accountInfoFromWg.WargamingAccountInfo.Achievements);
    }

    private async Task GetMaxXpAndFragsTanksInfo()
    {
      var tanksInfos = await _accountsTankInfoDataAccessor.GetStringTankInfos(new long[] {
                                _guestAccountInfo.AccountInfo.PlayerStatistics.MaxFragsTankId,
                                _guestAccountInfo.AccountInfo.PlayerStatistics.MaxXpTankId });

      if (tanksInfos.Exists(t => t.tankId == _guestAccountInfo.AccountInfo.PlayerStatistics.MaxFragsTankId))
      {
        _guestAccountInfo.AccountInfo.PlayerStatistics.MaxFragsTankInfo = tanksInfos
        .First(t => t.tankId == _guestAccountInfo.AccountInfo.PlayerStatistics.MaxFragsTankId)
        .tankInfo;
      }

      if (tanksInfos.Exists(t => t.tankId == _guestAccountInfo.AccountInfo.PlayerStatistics.MaxXpTankId))
      {
        _guestAccountInfo.AccountInfo.PlayerStatistics.MaxXpTankInfo = tanksInfos
        .First(t => t.tankId == _guestAccountInfo.AccountInfo.PlayerStatistics.MaxXpTankId)
        .tankInfo;
      }
    }

    private void GetAccountMasteryInfo(List<AccountTankStatistics> tanks)
    {
      var allTanksCount = tanks.Count();
      _guestAccountInfo.AccountInfo.AccountMasteryInfo = new List<AccountMasteryInfoDto>{
          new AccountMasteryInfoDto {MarkOfMastery = MarkOfMastery.Master, AllTanksCount = allTanksCount, TanksCount = tanks.Where(t => t.MarkOfMastery == MarkOfMastery.Master).Count()},
          new AccountMasteryInfoDto {MarkOfMastery = MarkOfMastery.Rank1, AllTanksCount = allTanksCount, TanksCount = tanks.Where(t => t.MarkOfMastery == MarkOfMastery.Rank1).Count()},
          new AccountMasteryInfoDto {MarkOfMastery = MarkOfMastery.Rank2, AllTanksCount = allTanksCount, TanksCount = tanks.Where(t => t.MarkOfMastery == MarkOfMastery.Rank2).Count()},
          new AccountMasteryInfoDto {MarkOfMastery = MarkOfMastery.Rank3, AllTanksCount = allTanksCount, TanksCount = tanks.Where(t => t.MarkOfMastery == MarkOfMastery.Rank3).Count()},
      };
    }

    private (List<AccountTankInfoDto>, List<AccountTanksInfoAggregationDto>) GetTanks(List<AccountTankStatistics> tanks)
    {
      var tanksDto = new List<AccountTankInfoDto>();
      var aggregatedDto = new List<AccountTanksInfoAggregationDto>();
      var vehicles = _dictionaryDataAccessor.GetVehicles(tanks.Select(t => t.TankId).ToList()).GetAwaiter().GetResult();
      var nations = _dictionaryDataAccessor.GetAllNations().GetAwaiter().GetResult();
      var vehicleTypes = _dictionaryDataAccessor.GetAllVehicelTypes().GetAwaiter().GetResult();
      foreach (var tank in tanks)
      {
        var tuple = new AccountTanksStatisticsTuple
        {
          Tank = tank,
          Vehicle = vehicles.SingleOrDefault(v => v.TankId == tank.TankId)
        };
        tanksDto.Add(_tanksMapper.Map<AccountTanksStatisticsTuple, AccountTankInfoDto>(tuple));

        if (tuple.Vehicle != null)
        {
          aggregatedDto.Add(new AccountTanksInfoAggregationDto
          {
            InGarage = tuple.Tank.InGarage,
            Battles = tuple.Tank.Battles,
            Wins = tuple.Tank.Wins,
            DamageDealt = tuple.Tank.DamageDealt,
            MarkOfMastery = tuple.Tank.MarkOfMastery,
            Wn7 = tuple.Tank.Wn7,
            Tier = tuple.Vehicle.Tier,
            Nation = tuple.Vehicle.Nation,
            NationName = nations.First(n => n.NationId == tuple.Vehicle.Nation).NationName,
            Type = tuple.Vehicle.Type,
            TypeName = vehicleTypes.First(t => t.VehicleTypeId == tuple.Vehicle.Type).VehicleTypeName,
            IsPremium = tuple.Vehicle.IsPremium
          });
        }
      }
      return (tanksDto, aggregatedDto);
    }

    private List<AchievementDto> GetAccountAchievements(List<AccountInfoAchievement> achievements)
    {
      var result = new List<AchievementDto>();
      var dictionaryAchievements = _dictionaryDataAccessor.GetAchievements(achievements.Select(a => a.AchievementId).ToList()).GetAwaiter().GetResult();
      var achievementsSections = _dictionaryDataAccessor.GetAchievementSections().GetAwaiter().GetResult();
      foreach (var accountAchievement in achievements)
      {
        var dictionaryAchievement = dictionaryAchievements.SingleOrDefault(a => a.AchievementId == accountAchievement.AchievementId);
        if(dictionaryAchievement == null)
        {
          continue;
        }
        result.Add(new AchievementDto
        {
          AchievementId = accountAchievement.AchievementId,
          Section = dictionaryAchievement.Section,
          SectionName = achievementsSections.SingleOrDefault(s => s.Section == dictionaryAchievement.Section)?.SectionName,
          Order = dictionaryAchievement.Order,
          Name = dictionaryAchievement.Name,
          Description = dictionaryAchievement.Description,
          Count = accountAchievement.Count,
          Image = dictionaryAchievement.Image,
          IsAchievementOption = string.IsNullOrEmpty(dictionaryAchievement.Image)
        });
      }
      return result;
    }

  }
}