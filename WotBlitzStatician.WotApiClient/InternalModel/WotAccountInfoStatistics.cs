namespace WotBlitzStatician.WotApiClient.InternalModel
{
	using System.Collections.Generic;
	using Newtonsoft.Json;

	internal class WotAccountInfoStatistics
	{
		///<summary>
		///Количество и модели уничтоженной игроком техники. Приватные данные игрока.
		///</summary>
		[JsonProperty("frags")]
		public Dictionary<string, string> Frags { get; set; }

		///<summary>
		///Вся статистика
		///</summary>
		[JsonProperty("all")]
		public WotAccountInfoStatisticsAll All { get; set; }

	}
}