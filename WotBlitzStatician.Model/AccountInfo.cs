namespace WotBlitzStatician.Model
{
	using System;
	using System.Collections.Generic;
	 
	public class AccountInfo
	{
		///<summary>
		///Идентификатор аккаунта игрока
		///</summary>
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

		public string AccessToken { get; set; }

		public DateTime? AccessTokenExpiration { get; set; }

		public List<AccountInfoStatistics> AccountInfoStatistics { get; set; }
		
		public List<AccountInfoAchievment> Achievments { get; set; }

		public List<AccountInfoAchievment> AchievmentsMaxSeries { get; set; }

		public AccountClanInfo AccountClanInfo { get; set; }
	}
}
