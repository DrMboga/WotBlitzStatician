namespace WotBlitzStatician.WotApiClient.InternalModel
{
	using System;
	using System.Collections.Generic;
	using Newtonsoft.Json;
	using WotBlitzStatician.Model;

	internal class WotAccountTanksStatResponse
	{
		///<summary>
		///Идентификатор аккаунта игрока
		///</summary>
		[JsonProperty("account_id")]
		public long? AccountId { get; set; }

		[JsonProperty("battle_life_time")]
		private int? _battleLifeTime;

		///<summary>
		///Общее время в боях в секундах
		///</summary>
		public TimeSpan? BattleLifeTime => _battleLifeTime.ToTimeSpan();

		[JsonProperty("last_battle_time")]
		private int? _lastBattleTime;
		///<summary>
		///Время последнего боя
		///</summary>
		public DateTime? LastBattleTime => _lastBattleTime.ToDateTime();

		[JsonProperty("in_garage")]
		public bool InGarage { get; set; }

		[JsonProperty("in_garage_updated")]
		private int? _inGarageUpdated;
		public DateTime? InGarageUpdated => _inGarageUpdated.ToDateTime();

		///<summary>
		///Знаки классности:
		///
		///0 — Отсутствует
		///1 — 3 степень
		///2 — 2 степень
		///3 — 1 степень
		///4 — Мастер
		///</summary>
		[JsonProperty("mark_of_mastery")]
		private long? _markOfMastery;
		public MarkOfMastery MarkOfMastery => _markOfMastery.HasValue ? (MarkOfMastery)_markOfMastery.Value : MarkOfMastery.None;

		///<summary>
		///Идентификатор техники
		///</summary>
		[JsonProperty("tank_id")]
		public long? TankId { get; set; }

		///<summary>
		///Вся статистика
		///</summary>
		[JsonProperty("all")]
		public WotAccountTankstatsAll All { get; set; }

		[JsonProperty("frags")]
		public Dictionary<string, string> Frags { get; set; }

	}
}
