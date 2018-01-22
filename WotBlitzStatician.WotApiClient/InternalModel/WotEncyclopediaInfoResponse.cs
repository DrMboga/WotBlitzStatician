namespace WotBlitzStatician.WotApiClient.InternalModel
{
	using System;
	using System.Collections.Generic;
	using Newtonsoft.Json;

	internal class WotEncyclopediaInfoResponse
	{
		///<summary>
		///Версия игрового клиента
		///</summary>
		[JsonProperty("game_version")]
		public string GameVersion { get; set; }

		///<summary>
		///Cписок поддерживаемых языков
		///</summary>
		[JsonProperty("languages")]
		public Dictionary<string, string> Languages { get; set; }

		///<summary>
		///Время обновления информации о технике в энциклопедии
		///</summary>
		[JsonProperty("tanks_updated_at")]
		private int? _tanksUpdatedAt;
		public DateTime? TanksUpdatedAt => _tanksUpdatedAt.ToDateTime();

		///<summary>
		///Возможные специальности экипажа
		///</summary>
		[JsonProperty("vehicle_crew_roles")]
		public Dictionary<string, string> VehicleCrewRoles { get; set; }

		///<summary>
		///Доступные нации
		///</summary>
		[JsonProperty("vehicle_nations")]
		public Dictionary<string, string> VehicleNations { get; set; }

		///<summary>
		///Возможные типы техники
		///</summary>
		[JsonProperty("vehicle_types")]
		public Dictionary<string, string> VehicleTypes { get; set; }

		///<summary>
		///Разделы наград
		///</summary>
		[JsonProperty("achievement_sections")]
		public Dictionary<string, WotEncyclopediaInfoAchievement_section> AchievementSections { get; set; }
	}

	internal class WotEncyclopediaInfoAchievement_section
	{

		///<summary>
		///Название раздела наград
		///</summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		///<summary>
		///Порядок раздела наград
		///</summary>
		[JsonProperty("order")]
		public long? Order { get; set; }

	}
}