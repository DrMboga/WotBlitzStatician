namespace WotBlitzStatician.WotApiClient.InternalModel
{
    using System;
    using Newtonsoft.Json;

    internal class WotClansAccountinfoResponse
    {

		///<summary>
		///Идентификатор игрока
		///</summary>
		[JsonProperty("account_id")]
		public long? AccountId { get; set; }

		///<summary>
		///Имя игрока
		///</summary>
		[JsonProperty("account_name")]
		public string AccountName { get; set; }

		///<summary>
		///Идентификатор клана
		///</summary>
		[JsonProperty("clan_id")]
		public long? ClanId { get; set; }

		[JsonProperty("joined_at")]
        private int? _joinedAt { get; set; }

		///<summary>
		///Дата вступления в клан
		///</summary>
		public DateTime? JoinedAt => _joinedAt.ToDateTime();

		///<summary>
		///Техническое название должности
		///</summary>
		[JsonProperty("role")]
		public string Role { get; set; }
	}
}
