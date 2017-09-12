namespace WotBlitzStatician.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using WotBlitzStatician.Model;

	public class AccountInfoViewModel
	{
        public long AccountId { get; set; }

        public string NickName { get; set; }

        public DateTime LastBattleTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public long Battles { get; set; }

		[DisplayFormat(DataFormatString = "{0:N0}")]
		public long Wins { get; set; }

		[DisplayFormat(DataFormatString = "{0:N2}")]
		public decimal Winrate { get; set; }

        public WinrateGradations WinrateGrade { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")] 
        public double Wn7 { get; set; }

        public Wn7Gradations Wn7Grade { get; set; }

		[DisplayFormat(DataFormatString = "{0:N0}")]
		public decimal AvgDamage { get; set; }

		[DisplayFormat(DataFormatString = "{0:N0}")]
		public decimal AvgXp { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")] 
        public decimal AvgTier { get; set; }

		public AccountClanInfoViewModel AccountClanInfo { get; set; }

		public AccountInfoDeltaViewModel AccountInfoDelta { get; set; }
	}
}