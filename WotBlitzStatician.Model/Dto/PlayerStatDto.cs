using System;

namespace WotBlitzStatician.Model.Dto
{
    public class PlayerStatDto
    {
		public long Battles { get; set; }
		public DateTime UpdatedAt { get; set; }
		public long Wins { get; set; }
		public double AvgTier { get; set; }
		public double Wn7 { get; set; }
		public long Frags { get; set; }
		public long Frags8P { get; set; }
		public long MaxFrags { get; set; }
		public string MaxFragsTankInfo { get; set; }
		public long MaxXp { get; set; }
		public string MaxXpTankInfo { get; set; }

		public decimal WinRate { get { return (decimal)Wins / Battles; } }
		public decimal AvgDamage { get { return (decimal)DamageDealt / Battles; } }
		public decimal AvgXp { get { return (decimal)Xp / Battles; } }
		public decimal SurvivalRate { get { return (decimal)SurvivedBattles / Battles; } }

		public long CapturePoints { get; set; }
		public long DamageDealt { get; set; }
		public long DamageReceived { get; set; }
		public long DroppedCapturePoints { get; set; }
		public long Hits { get; set; }
		public long Losses { get; set; }
		public long MaxFragsTankId { get; set; }
		public long MaxXpTankId { get; set; }
		public long Shots { get; set; }
		public long Spotted { get; set; }
		public long SurvivedBattles { get; set; }
		public long WinAndSurvived { get; set; }
		public long Xp { get; set; }

		public int? BattleLifeTimeInSeconds { get; set; }
		public long? Credits { get; set; }
		public long? FreeXp { get; set; }
		public long? Gold { get; set; }
		public bool? IsPremium { get; set; }
		public DateTime? PremiumExpiresAt { get; set; }
	}
}
