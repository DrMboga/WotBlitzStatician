namespace WotBlitzStatician.WotApiClient.InternalModel
{
	using System.Collections.Generic;
	using Newtonsoft.Json;

	internal class WotAccountAchievementResponse
	{
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
	}
}