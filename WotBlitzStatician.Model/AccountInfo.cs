namespace WotBlitzStatician.Model
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	 
	public class AccountInfo
	{
		///<summary>
		///Идентификатор аккаунта игрока
		///</summary>
		[Key]
		public long AccountId { get; set; }

		///<summary>
		///Имя игрока
		///</summary>
		public string NickName { get; set; }

		///<summary>
		///Дата создания аккаунта игрока
		///</summary>
		public DateTime AccountCreatedAt { get; set; }

		/// <summary>
		/// Время последнего боя
		/// </summary>
		public DateTime? LastBattleTime { get; set; }

		/// <summary>
		/// Последним просматривался (куки)
		/// </summary>
		public bool IsLastSession { get; set; }

		[NotMapped]
		public AccountInfoPrivate AccountInfoPrivate { get; set; }

		[NotMapped]
		public AccountInfoStatistics AccountInfoStatistics { get; set; }
		
		[NotMapped]
		public List<AccountInfoAchievment> Achievments { get; set; }

		[NotMapped]
		public List<AccountInfoAchievment> AchievmentsMaxSeries { get; set; }

		[NotMapped]
		public AccountClanInfo AccountClanInfo { get; set; }
	}
}
