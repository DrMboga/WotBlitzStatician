namespace WotBlitzStatician.Model
{
	 	public class VehicleEncyclopedia
	{
		///<summary>
		///Идентификатор техники
		///</summary>
		public long TankId { get; set; }

		///<summary>
		///Название техники
		///</summary>
		public string Name { get; set; }

		///<summary>
		///Уровень
		///</summary>
		public long Tier { get; set; }

		///<summary>
		///Нация
		///</summary>
		public string Nation { get; set; }

		///<summary>
		///Тип техники
		///</summary>
		public string Type { get; set; }

		///<summary>
		///Описание техники
		///</summary>
		public string Description { get; set; }

		///<summary>
		///Показывает, является ли техника премиум техникой
		///</summary>
		public bool IsPremium { get; set; }

		///<summary>
		///Стоимость в кредитах
		///</summary>
		public long PriceCredit { get; set; }

		///<summary>
		///Стоимость в золоте
		///</summary>
		public long PriceGold { get; set; }

		/// <summary>
		/// Path to the png file
		/// </summary>
		public string PreviewImageUrl { get; set; }

		/// <summary>
		/// Path to the png file
		/// </summary>
		public string NormalImageUrl { get; set; }
	}
}
