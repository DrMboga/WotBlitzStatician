namespace WotBlitzStatician.WotApiClient.InternalModel
{
	using System;
	using System.Collections.Generic;
	using Newtonsoft.Json;

	internal class WotEncyclopediaAchievementsResponse
	{
		[JsonProperty("achievement_id")]
		public string AchievementId { get; set; }

		///<summary>
		///Условие
		///</summary>
		[JsonProperty("condition")]
		public string Condition { get; set; }

		///<summary>
		///Описание достижения
		///</summary>
		[JsonProperty("description")]
		public string Description { get; set; }

		///<summary>
		///Историческая справка
		///</summary>
		[JsonProperty("hero_info")]
		public string HeroInfo { get; set; }

		///<summary>
		///Ссылка на изображение
		///</summary>
		[JsonProperty("image")]
		public string Image { get; set; }

		///<summary>
		///Изображение 180x180px
		///</summary>
		[JsonProperty("image_big")]
		public string ImageBig { get; set; }

		///<summary>
		///Название достижения
		///</summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		///<summary>
		///Локализованное поле name
		///</summary>
		[JsonProperty("name_i18n")]
		public string NameI18n { get; set; }

		///<summary>
		///Порядок сортировки
		///</summary>
		[JsonProperty("order")]
		public Int64? Order { get; set; }

		///<summary>
		///Показывает, если достижение устарело и больше не может быть получено
		///</summary>
		[JsonProperty("outdated")]
		public bool Outdated { get; set; }

		///<summary>
		///Раздел
		///</summary>
		[JsonProperty("section")]
		public string Section { get; set; }

		///<summary>
		///Порядок отображения раздела
		///</summary>
		[JsonProperty("section_order")]
		public Int64? SectionOrder { get; set; }

		///<summary>
		///Тип
		///</summary>
		[JsonProperty("type")]
		public string Type { get; set; }

		///<summary>
		///Достижения
		///</summary>
		[JsonProperty("options")]
		public WotEncyclopediaAchievementsOptions[] Options { get; set; }
	}

	internal class WotEncyclopediaAchievementsOptions
	{

		///<summary>
		///Описание достижения
		///</summary>
		[JsonProperty("description")]
		public string Description { get; set; }

		///<summary>
		///Ссылка на изображение
		///</summary>
		[JsonProperty("image")]
		public string Image { get; set; }

		///<summary>
		///Изображение 180x180px
		///</summary>
		[JsonProperty("image_big")]
		public string ImageBig { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		///<summary>
		///Локализованное поле name
		///</summary>
		[JsonProperty("name_i18n")]
		public string NameI18n { get; set; }

		///<summary>
		///Информация об эмблемах наций
		///</summary>
		[JsonProperty("nation_images")]
		public WotEncyclopediaAchievementsOptionsNation_images NationImages { get; set; }
	}

	internal class WotEncyclopediaAchievementsOptionsNation_images
	{

		///<summary>
		///Перечень ссылок на эмблемы 180x180 px
		///</summary>
		[JsonProperty("x180")]
		public Dictionary<string, string> X180 { get; set; }

		///<summary>
		///Перечень ссылок на эмблемы 67x71 px
		///</summary>
		[JsonProperty("x71")]
		public Dictionary<string, string> X71 { get; set; }

		///<summary>
		///Перечень ссылок на эмблемы 95x85 px
		///</summary>
		[JsonProperty("x85")]
		public Dictionary<string, string> X85 { get; set; }
	}
}