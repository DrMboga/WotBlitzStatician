using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WotBlitzStaticitian.Model
{
	public class AccountInfoPrivate
	{
		[Key]
		public long AccountInfoPrivateId { get; set; }

		[ForeignKey("AccountInfo")]
		public long AccountId { get; set; }

		///<summary>
		///Информация о блокировке аккаунта
		///</summary>
		public string BanInfo { get; set; }

		///<summary>
		///Время окончания блокировки аккаунта
		///</summary>
		public DateTime? BanTime { get; set; }

		///<summary>
		///Общее время в бою до уничтожения в секундах
		///</summary>
		public long BattleLifeTime { get; set; }

		///<summary>
		///Кредиты
		///</summary>
		public long Credits { get; set; }

		///<summary>
		///Свободный опыт
		///</summary>
		public long FreeXp { get; set; }

		///<summary>
		///Золото
		///</summary>
		public long Gold { get; set; }

		///<summary>
		///Показывает, является ли аккаунт премиум аккаунтом
		///</summary>
		public bool IsPremium { get; set; }

		///<summary>
		///Срок действия премиум аккаунта
		///</summary>
		public DateTime? PremiumExpiresAt { get; set; }
	}
}
