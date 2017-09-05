namespace WotBlitzStatician.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using WotBlitzStatician.Model;

	public class AccountInfoViewModel
	{
        [Obsolete]
		public string Wn7Style { get; set; }
        [Obsolete]
		public string Wn7GradeS { get; set; }

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
		public long AvgDamage { get; set; }

		[DisplayFormat(DataFormatString = "{0:N0}")]
		public long AvgXp { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")] 
        public decimal AvgTier { get; set; }
	}
}