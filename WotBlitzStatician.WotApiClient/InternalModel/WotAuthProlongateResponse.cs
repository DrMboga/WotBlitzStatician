using System;
using Newtonsoft.Json;

namespace WotBlitzStatician.WotApiClient.InternalModel
{
	internal class WotAuthProlongateResponse
    {
		///<summary>
		///Ключ доступа. Передаётся во все методы, требующие авторизацию.
		///</summary>
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }

		///<summary>
		///Идентификатор аккаунта игрока
		///</summary>
		[JsonProperty("account_id")]
		public long? AccountId { get; set; }

		[JsonProperty("expires_at")]
		private int? _expiresAt { get; set; }
		///<summary>
		///Срок действия access_token
		///</summary>
		public DateTime? ExpiresAt => _expiresAt.ToDateTime();

	}
}
