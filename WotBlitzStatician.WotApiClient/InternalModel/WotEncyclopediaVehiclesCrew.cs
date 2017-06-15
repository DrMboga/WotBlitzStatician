namespace WotBlitzStatician.WotApiClient.InternalModel
{
	using System.Collections.Generic;
	using Newtonsoft.Json;

	internal class WotEncyclopediaVehiclesCrew
	{
		///<summary>
		///Идентификатор члена экипажа
		///</summary>
		[JsonProperty("member_id")]
		public string MemberId { get; set; }

		///<summary>
		///Список должностей члена экипажа
		///</summary>
		[JsonProperty("roles")]
		public Dictionary<string, string> Roles { get; set; }
	}
}