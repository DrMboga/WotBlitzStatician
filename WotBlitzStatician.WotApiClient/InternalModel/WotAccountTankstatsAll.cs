namespace WotBlitzStatician.WotApiClient.InternalModel
{
	using Newtonsoft.Json;

	internal class WotAccountTankstatsAll
	{
		///<summary>
		///Количество боёв
		///</summary>
		[JsonProperty("battles")]
		public long? Battles { get; set; }

		///<summary>
		///Очки захвата базы
		///</summary>
		[JsonProperty("capture_points")]
		public long? CapturePoints { get; set; }

		///<summary>
		///Нанесённый урон
		///</summary>
		[JsonProperty("damage_dealt")]
		public long? DamageDealt { get; set; }

		///<summary>
		///Полученный урон
		///</summary>
		[JsonProperty("damage_received")]
		public long? DamageReceived { get; set; }

		///<summary>
		///Очки защиты базы
		///</summary>
		[JsonProperty("dropped_capture_points")]
		public long? DroppedCapturePoints { get; set; }

		///<summary>
		///Количество уничтоженной техники
		///</summary>
		[JsonProperty("frags")]
		public long? Frags { get; set; }

		///<summary>
		///Количество уничтоженной техники (уровень >=8)
		///</summary>
		[JsonProperty("frags8p")]
		public long? Frags8P { get; set; }

		///<summary>
		///Количество попаданий
		///</summary>
		[JsonProperty("hits")]
		public long? Hits { get; set; }

		///<summary>
		///Количество поражений
		///</summary>
		[JsonProperty("losses")]
		public long? Losses { get; set; }

		///<summary>
		///Максимум уничтожено за бой
		///</summary>
		[JsonProperty("max_frags")]
		public long? MaxFrags { get; set; }

		///<summary>
		///Максимальный опыт за бой
		///</summary>
		[JsonProperty("max_xp")]
		public long? MaxXp { get; set; }

		///<summary>
		///Произведено выстрелов
		///</summary>
		[JsonProperty("shots")]
		public long? Shots { get; set; }

		///<summary>
		///Количество обнаруженной техники
		///</summary>
		[JsonProperty("spotted")]
		public long? Spotted { get; set; }

		///<summary>
		///Выжил в боях
		///</summary>
		[JsonProperty("survived_battles")]
		public long? SurvivedBattles { get; set; }

		///<summary>
		///Выжил в боях и победил
		///</summary>
		[JsonProperty("win_and_survived")]
		public long? WinAndSurvived { get; set; }

		///<summary>
		///Количество побед
		///</summary>
		[JsonProperty("wins")]
		public long? Wins { get; set; }

		///<summary>
		///Суммарный опыт
		///</summary>
		[JsonProperty("xp")]
		public long? Xp { get; set; }

	}
}