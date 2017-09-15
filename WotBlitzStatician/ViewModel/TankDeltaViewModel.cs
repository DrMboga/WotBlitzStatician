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
		public long PastBattles { get; set; }

		[DisplayFormat(DataFormatString = "+{0:N0}")]
		public long BattlesDelta { get; set; }

		public DateTime PresentLastBattle { get; set; }
		public DateTime PastLastBattle { get; set; }

		[DisplayFormat(DataFormatString = "+{0}")]
		public TimeSpan LastBattleDelta { get; set; }

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

		[DisplayFormat(DataFormatString = "{0:N0}")]
		public decimal PastEffectivity { get; set; }

		public string EffectivityDelta { get; set; }
	}
}