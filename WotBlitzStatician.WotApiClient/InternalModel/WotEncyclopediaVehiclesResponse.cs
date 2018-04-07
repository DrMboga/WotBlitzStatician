namespace WotBlitzStatician.WotApiClient.InternalModel
{
	using System.Collections.Generic;
	using Newtonsoft.Json;

	internal class WotEncyclopediaVehiclesResponse
	{
		///<summary>
		///Описание техники
		///</summary>
		[JsonProperty("description")]
		public string Description { get; set; }

		///<summary>
		///Список идентификаторов совместимых двигателей
		///</summary>
		[JsonProperty("engines")]
		public int[] Engines { get; set; }

		///<summary>
		///Список идентификаторов совместимых орудий
		///</summary>
		[JsonProperty("guns")]
		public int[] Guns { get; set; }

		///<summary>
		///Показывает, является ли техника подарочной
		///</summary>
		[JsonProperty("is_gift")]
		public bool IsGift { get; set; }

		///<summary>
		///Показывает, является ли техника премиум техникой
		///</summary>
		[JsonProperty("is_premium")]
		public bool IsPremium { get; set; }

		///<summary>
		///Указывает технику IGR. Действительно только для корейского региона
		///</summary>
		[JsonProperty("is_premium_igr")]
		public bool IsPremiumIgr { get; set; }

		///<summary>
		///Название техники
		///</summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		///<summary>
		///Нация
		///</summary>
		[JsonProperty("nation")]
		public string Nation { get; set; }

		///<summary>
		///Список доступной для исследования техники в виде пар:
		///
		///идентификатор исследуемой техники
		///стоимость исследования в опыте
		///</summary>
		[JsonProperty("next_tanks")]
		public Dictionary<string, string> NextTanks { get; set; }

		///<summary>
		///Стоимость в кредитах
		///</summary>
		[JsonProperty("price_credit")]
		public long? PriceCredit { get; set; }

		///<summary>
		///Стоимость в золоте
		///</summary>
		[JsonProperty("price_gold")]
		public long? PriceGold { get; set; }

		///<summary>
		///Список значений стоимости исследования в виде пар:
		///</summary>
		[JsonProperty("cost")]
		public Dictionary<string, string> Cost { get; set; }

		///идентификатор родительской техники
		///стоимость исследованния в опыте
		///</summary>
		[JsonProperty("prices_xp")]
		public Dictionary<string, string> PricesXp { get; set; }

		///<summary>
		///Список идентификаторов совместимого оборудования и снаряжения
		///</summary>
		[JsonProperty("provisions")]
		public int[] Provisions { get; set; }

		///<summary>
		///Список идентификаторов устанавливаемых радиостанций
		///</summary>
		[JsonProperty("radios")]
		public int[] Radios { get; set; }

		///<summary>
		///Сокращённое название техники
		///</summary>
		[JsonProperty("short_name")]
		public string ShortName { get; set; }

		///<summary>
		///Список идентификаторов совместимых ходовых
		///</summary>
		[JsonProperty("suspensions")]
		public int[] Suspensions { get; set; }

		///<summary>
		///Тег техники
		///</summary>
		[JsonProperty("tag")]
		public string Tag { get; set; }

		///<summary>
		///Идентификатор техники
		///</summary>
		[JsonProperty("tank_id")]
		public long? TankId { get; set; }

		///<summary>
		///Уровень
		///</summary>
		[JsonProperty("tier")]
		public long? Tier { get; set; }

		///<summary>
		///Список идентификаторов совместимых башен
		///</summary>
		[JsonProperty("turrets")]
		public int[] Turrets { get; set; }

		///<summary>
		///Тип техники
		///</summary>
		[JsonProperty("type")]
		public string Type { get; set; }

		///<summary>
		///Экипаж
		///</summary>
		[JsonProperty("crew")]
		public WotEncyclopediaVehiclesCrew Crew { get; set; }

		///<summary>
		///Характеристики базовой комплектации
		///</summary>
		[JsonProperty("default_profile")]
		public WotEncyclopediaVehiclesDefaultProfile DefaultProfile { get; set; }

		///<summary>
		///Изображения техники
		///</summary>
		[JsonProperty("images")]
		public Dictionary<string, string> Images { get; set; }
		//		///Изображения техники
		//		///</summary>
		//		[JsonProperty("images")]
		//		public WotEncyclopediaVehiclesImages Images { get; set; }

		///<summary>
		///Информация об исследовании модулей
		///</summary>
		[JsonProperty("modules_tree")]
		public WotEncyclopediaVehiclesModulesTree ModulesTree { get; set; }

	}
}