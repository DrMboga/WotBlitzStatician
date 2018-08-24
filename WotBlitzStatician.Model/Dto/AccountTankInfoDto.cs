using System;
using WotBlitzStatician.Model.Common;

namespace WotBlitzStatician.Model.Dto
{
	public class AccountTankInfoDto
    {
		public long AccountTankStatisticId { get; set; }

		public long AccountId { get; set; }

		///<summary>
		///Идентификатор техники
		///</summary>
		public long TankId { get; set; }

		public int BattleLifeTimeInSeconds { get; set; }
		///<summary>
		///Общее время в бою до уничтожения
		///</summary>
		public TimeSpan BattleLifeTime => TimeSpan.FromSeconds(BattleLifeTimeInSeconds);

		///<summary>
		///Время последнего боя
		///</summary>
		public DateTime LastBattleTime { get; set; }

		///<summary>
		///Знаки классности:
		///
		///0 — Отсутствует
		///1 — 3 степень
		///2 — 2 степень
		///3 — 1 степень
		///4 — Мастер
		///</summary>
		public MarkOfMastery MarkOfMastery { get; set; }

		/// <summary>
		/// Признак нахождения машины в гараже
		/// </summary>
		public bool InGarage { get; set; }

		public DateTime? InGarageUpdated { get; set; }

		///<summary>
		///Количество боёв
		///</summary>
		public long Battles { get; set; }

		///<summary>
		///Очки захвата базы
		///</summary>
		public long CapturePoints { get; set; }

		///<summary>
		///Нанесённый урон
		///</summary>
		public long DamageDealt { get; set; }

		///<summary>
		///Полученный урон
		///</summary>
		public long DamageReceived { get; set; }

		///<summary>
		///Очки защиты базы
		///</summary>
		public long DroppedCapturePoints { get; set; }

		///<summary>
		///Количество уничтоженной техники
		///</summary>
		public long Frags { get; set; }

		///<summary>
		///Количество уничтоженной техники (уровень >=8)
		///</summary>
		public long Frags8P { get; set; }

		///<summary>
		///Количество попаданий
		///</summary>
		public long Hits { get; set; }

		///<summary>
		///Количество поражений
		///</summary>
		public long Losses { get; set; }

		///<summary>
		///Максимум уничтожено за бой
		///</summary>
		public long MaxFrags { get; set; }

		///<summary>
		///Максимальный опыт за бой
		///</summary>
		public long MaxXp { get; set; }

		///<summary>
		///Произведено выстрелов
		///</summary>
		public long Shots { get; set; }

		///<summary>
		///Количество обнаруженной техники
		///</summary>
		public long Spotted { get; set; }

		///<summary>
		///Выжил в боях
		///</summary>
		public long SurvivedBattles { get; set; }

		///<summary>
		///Выжил в боях и победил
		///</summary>
		public long WinAndSurvived { get; set; }

		///<summary>
		///Количество побед
		///</summary>
		public long Wins { get; set; }

		///<summary>
		///Суммарный опыт
		///</summary>
		public long Xp { get; set; }

		public double Wn7 { get; set; }

		public double Wn8 { get; set; }

		public decimal WinRate { get { return (decimal)Wins / Battles; } }
		public decimal AvgDamage { get { return (decimal)DamageDealt / Battles; } }
		public decimal AvgXp { get { return (decimal)Xp / Battles; } }
		public decimal SurvivalRate { get { return (decimal)SurvivedBattles / Battles; } }

		public string MasteryImage { get; set; }
		public string MasteryLocalImage => MasteryImage.MakeImagePathLocal();

		///<summary>
		///Название техники
		///</summary>
		public string NankName { get; set; }

		///<summary>
		///Уровень
		///</summary>
		public long TankTier { get; set; }

		public string TankTierRoman { get; set; }

		///<summary>
		///Нация
		///</summary>
		public string TankNation { get; set; }

		///<summary>
		///Тип техники
		///</summary>
		public string TankType { get; set; }

		///<summary>
		///Показывает, является ли техника премиум техникой
		///</summary>
		public bool TankIsPremium { get; set; }

		/// <summary>
		/// Path to the png file
		/// </summary>
		public string PreviewImageUrl { get; set; }

		public string PreviewLocalImage => PreviewImageUrl.MakeImagePathLocal();

		/// <summary>
		/// Path to the png file
		/// </summary>
		public string NormalImageUrl { get; set; }

		public string NormalLocalImage => NormalImageUrl.MakeImagePathLocal();


	}
}
