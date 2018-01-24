namespace WotBlitzStatician.Model
{
	using System;
	using System.Collections.Generic;

	public class AccountInfoStatistics
	{
		public long AccountInfoStatisticsId { get; set; }

		public long AccountId { get; set; }

		///<summary>
		///Количество боёв
		///</summary>
		public long Battles { get; set; }
		
		///<summary>
		///Дата обновления информации об игроке
		///</summary>
		public DateTime UpdatedAt { get; set; }

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
		///Техника, на которой уничтожено максимальное количество техники противника
		///</summary>
		public long MaxFragsTankId { get; set; }

		///<summary>
		///Максимальный опыт за бой
		///</summary>
		public long MaxXp { get; set; }

		///<summary>
		///Техника, на которой получен максимальный опыт за бой
		///</summary>
		public long MaxXpTankId { get; set; }

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

		/// <summary>
		/// Танкоуровень
		/// </summary>
		public double AvgTier { get; set; }

		/// <summary>
		/// Показатель Wn7
		/// </summary>
		public double Wn7 { get; set; }

		/// <summary>
		/// Показатель Wn8
		/// </summary>
		public double Wn8 { get; set; }

		public List<FragListItem> FragsList { get; set; }
		
		public AccountInfoPrivate AccountInfoPrivate { get; set; }
	}
}
