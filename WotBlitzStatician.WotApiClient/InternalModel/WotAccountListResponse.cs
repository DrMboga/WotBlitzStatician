namespace WotBlitzStatician.WotApiClient.InternalModel
{
	using Newtonsoft.Json;

	internal class WotAccountListResponse
	{
		///<summary>
		///Идентификатор аккаунта игрока
		///</summary>
		[JsonProperty("account_id")]
		public long? AccountId { get; set; }

		///<summary>
		///Имя игрока
		///</summary>
		[JsonProperty("nickname")]
		public string Nickname { get; set; }
	}
}
