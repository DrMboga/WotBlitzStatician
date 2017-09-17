namespace WotBlitzStatician.ViewModel
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using WotBlitzStatician.Model;

	public class TankDeltaViewModel
	{
		public long TankId { get; set; }
		public long AccountId { get; set; }
		public string Name { get; set; }
		public long Tier { get; set; }
        public string RomanTier { get; set; }
		public string Nation { get; set; }
		public string Type { get; set; }
		public bool IsPremium { get; set; }
		public string PreviewImageUrl { get; set; }
		public string NormalImageUrl { get; set; }
		public MarkOfMastery MarkOfMastery { get; set; }
		public string MarkOfMasteryImageUrl { get; set; }

		[DisplayFormat(DataFormatString = "{0:N0}")]
		public long PresentBattles { get; set; }

		[DisplayFormat(DataFormatString = "+{0:N0}")]
		public long BattlesDelta { get; set; }

		public DateTime PresentLastBattle { get; set; }
		public DateTime PastLastBattle { get; set; }

		[DisplayFormat(DataFormatString = "+{0}")]
		public TimeSpan LastBattleDelta { get; set; }

		[DisplayFormat(DataFormatString = "{0:N0}")]
		public long PresentWins { get; set; }

		[DisplayFormat(DataFormatString = "+{0:N0}")]
		public long WinsDelta { get; set; }

		[DisplayFormat(DataFormatString = "{0:N2}%")]
		public decimal PresentWinrate { get; set; }

		public string WinrateDelta { get; set; }

        public WinrateGradations WinrateGrade { get; set; }

		[DisplayFormat(DataFormatString = "{0:N0}")]
		public decimal PresentAvgDamage { get; set; }

		public string AvgDamageDelta { get; set; }

		[DisplayFormat(DataFormatString = "{0:N0}")]
		public decimal PresentAvgXp { get; set; }

		public string AvgXpDelta { get; set; }

		[DisplayFormat(DataFormatString = "{0:N0}")]
		public decimal PresentWn7 { get; set; }

		public string Wn7Delta { get; set; }

        public Wn7Gradations Wn7Grade { get; set; }

		[DisplayFormat(DataFormatString = "{0:N0}")]
		public decimal PresentEffectivity { get; set; }

		public string EffectivityDelta { get; set; }
	}
}