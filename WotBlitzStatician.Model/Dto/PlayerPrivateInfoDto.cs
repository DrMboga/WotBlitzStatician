using System;

public class PlayerPrivateInfoDto
{
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

		public int? BattleLifeTimeInSeconds { get; set; }

		public TimeSpan BattleLifeTyme => BattleLifeTimeInSeconds.HasValue ? TimeSpan.FromSeconds(BattleLifeTimeInSeconds.Value) : TimeSpan.Zero;

}