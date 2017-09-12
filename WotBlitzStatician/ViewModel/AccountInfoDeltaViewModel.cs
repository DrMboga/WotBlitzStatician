namespace WotBlitzStatician.ViewModel
{
	using System;
	using System.ComponentModel.DataAnnotations;

    public class AccountInfoDeltaViewModel
    {
        public long AccountId { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")] 
        public long PastBattles { get; set; }

        [DisplayFormat(DataFormatString = "+{0:N0}")] 
        public long BattlesDelta { get; set; }

        public DateTime PresentUpdatedAt { get; set; }
        public DateTime PastUpdatedAt { get; set; }

        [DisplayFormat(DataFormatString = "+{0}")] 
        public TimeSpan UpdatedAtDelta { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")] 
        public long PastWins { get; set; }

        [DisplayFormat(DataFormatString = "+{0:N0}")] 
        public long WinsDelta { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")] 
        public decimal PastWinrate { get; set; }

        public string WinrateDelta { get; set; }

		[DisplayFormat(DataFormatString = "{0:N0}")] 
        public decimal PastAvgDamage { get; set; }

		public string AvgDamageDelta { get; set; }
		
        [DisplayFormat(DataFormatString = "{0:N0}")] 
        public decimal PastAvgXp { get; set; }

		public string AvgXpDelta { get; set; }

		[DisplayFormat(DataFormatString = "{0:N0}")] 
        public decimal PastWn7 { get; set; }

		public string Wn7Delta { get; set; }

		[DisplayFormat(DataFormatString = "{0:N2}")] 
        public decimal PastAvgTier { get; set; }

		public string AvgTierDelta { get; set; }

		[DisplayFormat(DataFormatString = "{0:N0}")] 
        public decimal PastEffectivity { get; set; }

		public string EffectivityDelta { get; set; }

	}
}
