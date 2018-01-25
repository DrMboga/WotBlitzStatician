namespace WotBlitzStatician.Model
{
	using System;

	public class AccountClanInfo
    {
		public long AccountClanInfoId { get; set; }

		public AccountInfo AccountInfo { get; set; }

        public long ClanId { get; set; }

        public DateTime PlayerJoinedAt { get; set; }

        public string PlayerRole { get; set; }

        public DateTime ClanCreatedAt { get; set; }

        public string ClanLeaderName { get; set; }
 
        public long MembersCount { get; set; }

        public string ClanTag { get; set; }

        public string ClanName { get; set; }

        public string ClanMotto { get; set; }

        public string ClanDescription { get; set; }

		// ToDo: Clan members - direct call from API
    }
}
