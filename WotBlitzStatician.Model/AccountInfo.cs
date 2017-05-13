using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WotBlitzStaticitian.Model
{
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
		/// Последним просматривался (куки)
		/// </summary>
		public bool IsLastSession { get; set; }

		[NotMapped]
		public AccountInfoPrivate AccountInfoPrivate { get; set; }

		[NotMapped]
		public AccountInfoStatistics AccountInfoStatistica { get; set; }
	}
}
