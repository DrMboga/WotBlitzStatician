namespace WotBlitzStatician.WotApiClient.InternalModel
{
	using System;
	using System.Collections.Generic;
	using Newtonsoft.Json;

	internal class WotAccountInfoPrivate
	{
		///<summary>
		///Информация о блокировке аккаунта
		///</summary>
		[JsonProperty("ban_info")]
		public string BanInfo { get; set; }

		///<summary>
		///Время окончания блокировки аккаунта
		///</summary>
		[JsonProperty("ban_time")]
		public int? BanTime { get; set; }

		///<summary>
		///Общее время в бою до уничтожения в секундах
		///</summary>
		[JsonProperty("battle_life_time")]
		public Int64? BattleLifeTime { get; set; }

		///<summary>
		///Кредиты
		///</summary>
		[JsonProperty("credits")]
		public Int64? Credits { get; set; }

		///<summary>
		///Свободный опыт
		///</summary>
		[JsonProperty("free_xp")]
		public Int64? FreeXp { get; set; }

		///<summary>
		///Золото
		///</summary>
		[JsonProperty("gold")]
		public Int64? Gold { get; set; }

		///<summary>
		///Показывает, является ли аккаунт премиум аккаунтом
		///</summary>
		[JsonProperty("is_premium")]
		public bool IsPremium { get; set; }

		///<summary>
		///Срок действия премиум аккаунта
		///</summary>
		[JsonProperty("premium_expires_at")]
		public int? PremiumExpiresAt { get; set; }

		///<summary>
		///Группы контактов.
		///Дополнительное поле.
		///</summary>
		[JsonProperty("grouped_contacts")]
		public WotbAccountInfoPrivateGrouped_contacts GroupedContacts { get; set; }

		///<summary>
		///Ограничения аккаунта
		///</summary>
		[JsonProperty("restrictions")]
		public WotbAccountInfoPrivateRestrictions Restrictions { get; set; }
	}


	internal class WotbAccountInfoPrivateGrouped_contacts
	{

		///<summary>
		///Заблокированные
		///</summary>
		[JsonProperty("blocked")]
		public int[] Blocked { get; set; }

		///<summary>
		///Группы
		///</summary>
		[JsonProperty("groups")]
		public Dictionary<string, string> Groups { get; set; }

		///<summary>
		///Не сгруппированные
		///</summary>
		[JsonProperty("ungrouped")]
		public int[] Ungrouped { get; set; }
	}

	internal class WotbAccountInfoPrivateRestrictions
	{

		///<summary>
		///Время окончания блокировки в чате
		///</summary>
		[JsonProperty("chat_ban_time")]
		public int? ChatBanTime { get; set; }
	}
}