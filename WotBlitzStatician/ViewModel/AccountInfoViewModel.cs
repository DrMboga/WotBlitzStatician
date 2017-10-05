namespace WotBlitzStatician.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using WotBlitzStatician.Logic.Dto;

	public class AccountInfoViewModel
	{
        public long AccountId { get; set; }

        public string NickName { get; set; }

        public DateTime LastBattleTime { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime PreLastUpdatedDate { get; set; }

		public AccountClanInfoViewModel AccountClanInfo { get; set; }

		public AccountInfoDeltaViewModel AccountInfoDelta { get; set; }

		public List<BlitzTankInfoDelta> TanksDelta { get; set; }

	}
}