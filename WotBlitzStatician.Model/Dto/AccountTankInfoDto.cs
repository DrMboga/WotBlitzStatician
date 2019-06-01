using System;
using WotBlitzStatician.Model.Common;
using Newtonsoft.Json;

namespace WotBlitzStatician.Model.Dto
{
  public class AccountTankInfoDto
  {
    [JsonProperty(PropertyName = "TankAccountTankStatisticId")]
    public long TankAccountTankStatisticId { get; set; }

    [JsonProperty(PropertyName = "TankAccountId")]
    public long TankAccountId { get; set; }

    ///<summary>
    ///Идентификатор техники
    ///</summary>
    [JsonProperty(PropertyName = "TankTankId")]
    public long TankTankId { get; set; }

    [JsonProperty(PropertyName = "TankBattleLifeTimeInSeconds")]
    public int TankBattleLifeTimeInSeconds { get; set; }
    ///<summary>
    ///Общее время в бою до уничтожения
    ///</summary>
    [JsonProperty(PropertyName = "BattleLifeTime")]
    public TimeSpan BattleLifeTime => TimeSpan.FromSeconds(TankBattleLifeTimeInSeconds);

    ///<summary>
    ///Время последнего боя
    ///</summary>
    [JsonProperty(PropertyName = "TankLastBattleTime")]
    public DateTime TankLastBattleTime { get; set; }

    ///<summary>
    ///Знаки классности:
    ///
    ///0 — Отсутствует
    ///1 — 3 степень
    ///2 — 2 степень
    ///3 — 1 степень
    ///4 — Мастер
    ///</summary>
    [JsonProperty(PropertyName = "TankMarkOfMastery")]
    public MarkOfMastery TankMarkOfMastery { get; set; }

    /// <summary>
    /// Признак нахождения машины в гараже
    /// </summary>
    [JsonProperty(PropertyName = "TankInGarage")]
    public bool TankInGarage { get; set; }

    [JsonProperty(PropertyName = "TankInGarageUpdated")]
    public DateTime? TankInGarageUpdated { get; set; }

    ///<summary>
    ///Количество боёв
    ///</summary>
    [JsonProperty(PropertyName = "TankBattles")]
    public long TankBattles { get; set; }

    ///<summary>
    ///Очки захвата базы
    ///</summary>
    [JsonProperty(PropertyName = "TankCapturePoints")]
    public long TankCapturePoints { get; set; }

    ///<summary>
    ///Нанесённый урон
    ///</summary>
    [JsonProperty(PropertyName = "TankDamageDealt")]
    public long TankDamageDealt { get; set; }

    ///<summary>
    ///Полученный урон
    ///</summary>
    [JsonProperty(PropertyName = "TankDamageReceived")]
    public long TankDamageReceived { get; set; }

    ///<summary>
    ///Очки защиты базы
    ///</summary>
    [JsonProperty(PropertyName = "TankDroppedCapturePoints")]
    public long TankDroppedCapturePoints { get; set; }

    ///<summary>
    ///Количество уничтоженной техники
    ///</summary>
    [JsonProperty(PropertyName = "TankFrags")]
    public long TankFrags { get; set; }

    ///<summary>
    ///Количество уничтоженной техники (уровень >=8)
    ///</summary>
    [JsonProperty(PropertyName = "TankFrags8P")]
    public long TankFrags8P { get; set; }

    ///<summary>
    ///Количество попаданий
    ///</summary>
    [JsonProperty(PropertyName = "TankHits")]
    public long TankHits { get; set; }

    ///<summary>
    ///Количество поражений
    ///</summary>
    [JsonProperty(PropertyName = "TankLosses")]
    public long TankLosses { get; set; }

    ///<summary>
    ///Максимум уничтожено за бой
    ///</summary>
    [JsonProperty(PropertyName = "TankMaxFrags")]
    public long TankMaxFrags { get; set; }

    ///<summary>
    ///Максимальный опыт за бой
    ///</summary>
    [JsonProperty(PropertyName = "TankMaxXp")]
    public long TankMaxXp { get; set; }

    ///<summary>
    ///Произведено выстрелов
    ///</summary>
    [JsonProperty(PropertyName = "TankShots")]
    public long TankShots { get; set; }

    ///<summary>
    ///Количество обнаруженной техники
    ///</summary>
    [JsonProperty(PropertyName = "TankSpotted")]
    public long TankSpotted { get; set; }

    ///<summary>
    ///Выжил в боях
    ///</summary>
    [JsonProperty(PropertyName = "TankSurvivedBattles")]
    public long TankSurvivedBattles { get; set; }

    ///<summary>
    ///Выжил в боях и победил
    ///</summary>
    [JsonProperty(PropertyName = "TankWinAndSurvived")]
    public long TankWinAndSurvived { get; set; }

    ///<summary>
    ///Количество побед
    ///</summary>
    [JsonProperty(PropertyName = "TankWins")]
    public long TankWins { get; set; }

    ///<summary>
    ///Суммарный опыт
    ///</summary>
    [JsonProperty(PropertyName = "TankXp")]
    public long TankXp { get; set; }

    [JsonProperty(PropertyName = "TankWn7")]
    public double TankWn7 { get; set; }

    [JsonProperty(PropertyName = "TankWn8")]
    public double TankWn8 { get; set; }

    [JsonProperty(PropertyName = "WinRate")]
    public decimal WinRate { get { return (decimal)TankWins / TankBattles; } set { } }
    [JsonProperty(PropertyName = "AvgDamage")]
    public decimal AvgDamage { get { return (decimal)TankDamageDealt / TankBattles; } set { } }
    [JsonProperty(PropertyName = "AvgXp")]
    public decimal AvgXp { get { return (decimal)TankXp / TankBattles; } set { } }
    [JsonProperty(PropertyName = "SurvivalRate")]
    public decimal SurvivalRate { get { return (decimal)TankSurvivedBattles / TankBattles; } set { } }

    ///<summary>
    ///Название техники
    ///</summary>
    [JsonProperty(PropertyName = "VehicleName")]
    public string VehicleName { get; set; }

    ///<summary>
    ///Уровень
    ///</summary>
    [JsonProperty(PropertyName = "VehicleTier")]
    public int VehicleTier { get; set; }

    [JsonProperty(PropertyName = "TankTierRoman")]
    public string TankTierRoman { get; set; }

    ///<summary>
    ///Нация
    ///</summary>
    [JsonProperty(PropertyName = "VehicleNation")]
    public string VehicleNation { get; set; }

    ///<summary>
    ///Тип техники
    ///</summary>
    [JsonProperty(PropertyName = "VehicleType")]
    public string VehicleType { get; set; }

    ///<summary>
    ///Показывает, является ли техника премиум техникой
    ///</summary>
    [JsonProperty(PropertyName = "VehicleIsPremium")]
    public bool VehicleIsPremium { get; set; }

    /// <summary>
    /// Path to the png file
    /// </summary>
    [JsonProperty(PropertyName = "VehiclePreviewImageUrl")]
    public string VehiclePreviewImageUrl { get; set; }

    [JsonProperty(PropertyName = "PreviewLocalImage")]
    public string PreviewLocalImage { get; set; }

    /// <summary>
    /// Path to the png file
    /// </summary>
    [JsonProperty(PropertyName = "VehicleNormalImageUrl")]
    public string VehicleNormalImageUrl { get; set; }

    [JsonProperty(PropertyName = "NormalLocalImage")]
    public string NormalLocalImage { get; set; }


  }
}
