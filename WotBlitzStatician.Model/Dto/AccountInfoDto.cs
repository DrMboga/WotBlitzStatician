using System;
using System.Collections.Generic;

namespace WotBlitzStatician.Model.Dto
{

	public class AccountInfoDto
    {
		public long AccountId { get; set; }

		public string NickName { get; set; }

		public DateTime AccountCreatedAt { get; set; }

		public DateTime LastBattleTime { get; set; }

		public PlayerClanInfoDto PlayerClanInfo { get; set; }

		public PlayerStatDto PlayerStatistics { get; set; }

	    public List<AccountMasteryInfoDto> AccountMasteryInfo { get; set; }
	}
}
