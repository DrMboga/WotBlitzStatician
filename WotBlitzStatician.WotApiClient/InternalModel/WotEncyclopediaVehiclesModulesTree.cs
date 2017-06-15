namespace WotBlitzStatician.WotApiClient.InternalModel
{
	using Newtonsoft.Json;

	internal class WotEncyclopediaVehiclesModulesTree
	{
		///<summary>
		///Показывает, является ли модуль базовым
		///</summary>
		[JsonProperty("is_default")]
		public bool IsDefault { get; set; }

		///<summary>
		///Идентификатор модуля
		///</summary>
		[JsonProperty("module_id")]
		public long? ModuleId { get; set; }

		///<summary>
		///Название модуля
		///</summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		///<summary>
		///Список идентификаторов модулей, доступных после исследования модуля
		///</summary>
		[JsonProperty("next_modules")]
		public int[] NextModules { get; set; }

		///<summary>
		///Список идентификаторов техники доступной после исследования модуля
		///</summary>
		[JsonProperty("next_tanks")]
		public int[] NextTanks { get; set; }

		///<summary>
		///Стоимость в кредитах
		///</summary>
		[JsonProperty("price_credit")]
		public long? PriceCredit { get; set; }

		///<summary>
		///Стоимость исследования
		///</summary>
		[JsonProperty("price_xp")]
		public long? PriceXp { get; set; }

		///<summary>
		///Тип модуля
		///</summary>
		[JsonProperty("type")]
		public string Type { get; set; }

	}
}