namespace WotBlitzStatician.WotApiClient.InternalModel
{
	using System;
	using Newtonsoft.Json;

	internal class WotAccountInfoResponse
	{
		///<summary>
		///Идентификатор аккаунта игрока
		///</summary>
		[JsonProperty("account_id")]
		public long? AccountId { get; set; }

		[JsonProperty("created_at")]
		private int? _createdAt;
		///<summary>
		///Дата создания аккаунта игрока
		///</summary>
		public DateTime? CreatedAt => _createdAt.ToDateTime();

		[JsonProperty("last_battle_time")]
		private int? _lastBattleTime;
		///<summary>
		///Время последнего боя
		///</summary>
		public DateTime? LastBattleTime => _lastBattleTime.ToDateTime();

		///<summary>
		///Имя игрока
		///</summary>
		[JsonProperty("nickname")]
		public string Nickname { get; set; }

		[JsonProperty("updated_at")]
		private int? _updatedAt;
		///<summary>
		///Дата обновления информации об игроке
		///</summary>
		public DateTime? UpdatedAt => _updatedAt.ToDateTime();

		///<summary>
		///Приватные данные игрока
		///</summary>
		[JsonProperty("private")]
		public WotAccountInfoPrivate Private { get; set; }

		///<summary>
		///Статистика игрока
		///</summary>
		[JsonProperty("statistics")]
		public WotAccountInfoStatistics Statistics { get; set; }

	}
}