namespace WotBlitzStatician.WotApiClient.InternalModel
{
	using System.Collections.Generic;
	using Newtonsoft.Json;

	internal class WotAccountTankAchievementResponse
	{
		///<summary>
		///Идентификатор аккаунта игрока
		///</summary>
		[JsonProperty("account_id")]
		public long? AccountId { get; set; }

		///<summary>
		///Полученные достижения
		///</summary>
		[JsonProperty("achievements")]
		public Dictionary<string, string> Achievements { get; set; }

		///<summary>
		///Максимальные значения серийных достижений
		///</summary>
		[JsonProperty("max_series")]
		public Dictionary<string, string> MaxSeries { get; set; }

		///<summary>
		///Идентификатор техники
		///</summary>
		[JsonProperty("tank_id")]
		public long? TankId { get; set; }
	}
}