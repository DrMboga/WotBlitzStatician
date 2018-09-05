using System;
using WotBlitzStatician.Model.Common;

namespace WotBlitzStatician.Model.Dto
{
	public class AccountTankInfoDto
    {
		public long TankAccountTankStatisticId { get; set; }

		public long TankAccountId { get; set; }

		///<summary>
		///Идентификатор техники
		///</summary>
		public long TankTankId { get; set; }

		public int TankBattleLifeTimeInSeconds { get; set; }
		///<summary>
		///Общее время в бою до уничтожения
		///</summary>
		public TimeSpan BattleLifeTime => TimeSpan.FromSeconds(TankBattleLifeTimeInSeconds);

		///<summary>
		///Время последнего боя
		///</summary>
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
		public MarkOfMastery TankMarkOfMastery { get; set; }

		/// <summary>
		/// Признак нахождения машины в гараже
		/// </summary>
		public bool TankInGarage { get; set; }

		public DateTime? TankInGarageUpdated { get; set; }

		///<summary>
		///Количество боёв
		///</summary>
		public long TankBattles { get; set; }

		///<summary>
		///Очки захвата базы
		///</summary>
		public long TankCapturePoints { get; set; }

		///<summary>
		///Нанесённый урон
		///</summary>
		public long TankDamageDealt { get; set; }

		///<summary>
		///Полученный урон
		///</summary>
		public long TankDamageReceived { get; set; }

		///<summary>
		///Очки защиты базы
		///</summary>
		public long TankDroppedCapturePoints { get; set; }

		///<summary>
		///Количество уничтоженной техники
		///</summary>
		public long TankFrags { get; set; }

		///<summary>
		///Количество уничтоженной техники (уровень >=8)
		///</summary>
		public long TankFrags8P { get; set; }

		///<summary>
		///Количество попаданий
		///</summary>
		public long TankHits { get; set; }

		///<summary>
		///Количество поражений
		///</summary>
		public long TankLosses { get; set; }

		///<summary>
		///Максимум уничтожено за бой
		///</summary>
		public long TankMaxFrags { get; set; }

		///<summary>
		///Максимальный опыт за бой
		///</summary>
		public long TankMaxXp { get; set; }

		///<summary>
		///Произведено выстрелов
		///</summary>
		public long TankShots { get; set; }

		///<summary>
		///Количество обнаруженной техники
		///</summary>
		public long TankSpotted { get; set; }

		///<summary>
		///Выжил в боях
		///</summary>
		public long TankSurvivedBattles { get; set; }

		///<summary>
		///Выжил в боях и победил
		///</summary>
		public long TankWinAndSurvived { get; set; }

		///<summary>
		///Количество побед
		///</summary>
		public long TankWins { get; set; }

		///<summary>
		///Суммарный опыт
		///</summary>
		public long TankXp { get; set; }

		public double TankWn7 { get; set; }

		public double TankWn8 { get; set; }

		public decimal WinRate { get { return (decimal)TankWins / TankBattles; } set { } }
		public decimal AvgDamage { get { return (decimal)TankDamageDealt / TankBattles; } set { } }
		public decimal AvgXp { get { return (decimal)TankXp / TankBattles; } set { } }
		public decimal SurvivalRate { get { return (decimal)TankSurvivedBattles / TankBattles; } set { } }

		public string MasteryImage { get; set; }
		public string MasteryLocalImage { get; set; }

		///<summary>
		///Название техники
		///</summary>
		public string VehicleName { get; set; }

		///<summary>
		///Уровень
		///</summary>
		public int VehicleTier { get; set; }

	    public string TankTierRoman { get; set; }

		///<summary>
		///Нация
		///</summary>
		public string VehicleNation { get; set; }

		///<summary>
		///Тип техники
		///</summary>
		public string VehicleType { get; set; }

		///<summary>
		///Показывает, является ли техника премиум техникой
		///</summary>
		public bool VehicleIsPremium { get; set; }

		/// <summary>
		/// Path to the png file
		/// </summary>
		public string VehiclePreviewImageUrl { get; set; }

	    public string PreviewLocalImage { get; set; }

	    /// <summary>
		/// Path to the png file
		/// </summary>
		public string VehicleNormalImageUrl { get; set; }

		public string NormalLocalImage { get; set; }


	}
}
