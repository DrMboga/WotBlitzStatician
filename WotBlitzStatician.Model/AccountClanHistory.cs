using System;

namespace WotBlitzStatician.Model
{
	public class AccountClanHistory
    {
		public long AccountClanHistoryId { get; set; }

		public long AccountId { get; set; }

		public long? ClanId { get; set; }

		public DateTime PlayerJoinedAt { get; set; }

		public string ClanTag { get; set; }

		public string ClanName { get; set; }

		public string PlayerRole { get; set; }
	}
}
