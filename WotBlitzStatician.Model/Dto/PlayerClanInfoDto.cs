using System;

namespace WotBlitzStatician.Model.Dto
{
    public class PlayerClanInfoDto
    {
		public long ClanId { get; set; }

		public DateTime PlayerJoinedAt { get; set; }

		public string PlayerRole { get; set; }

		public string ClanTag { get; set; }

		public string ClanName { get; set; }

		public string ClanMotto { get; set; }

		public string ClanDescription { get; set; }
	}
}
