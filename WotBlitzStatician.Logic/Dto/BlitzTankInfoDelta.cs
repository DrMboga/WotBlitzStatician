namespace WotBlitzStatician.Logic.Dto
{
	using System;
	using WotBlitzStatician.Model;

	public class BlitzTankInfoDelta
	{
		public long AccountId { get; set; }
		public long TankId { get; set; }
		public string Name { get; set; }
		public long Tier { get; set; }
		public string Nation { get; set; }
		public string Type { get; set; }
		public bool IsPremium { get; set; }
		public string PreviewImageUrl { get; set; }
		public string NormalImageUrl { get; set; }
		public MarkOfMastery MarkOfMastery { get; set; }
		public string MarkOfMasteryImageUrl { get; set; }

		public ValueDelta<long, long> Battles { get; set; }
		public ValueDelta<DateTime, TimeSpan> LastBattle { get; set; }
		public ValueDelta<long, long> Wins { get; set; }
		public ValueDelta<decimal, decimal> Winrate { get; set; }
		public ValueDelta<decimal, decimal> AvgDamage { get; set; }
		public ValueDelta<decimal, decimal> AvgXp { get; set; }
		public ValueDelta<decimal, decimal> Wn7 { get; set; }
		public ValueDelta<decimal, decimal> Effectivity { get; set; }

		public decimal IntervalWinrate { get; set; }
		public decimal IntervalAvgDamage { get; set; }
		public decimal IntervalAvgXp { get; set; }
		public decimal IntervalWn7 { get; set; }
		public decimal IntervalEffectivity { get; set; }

	}
}