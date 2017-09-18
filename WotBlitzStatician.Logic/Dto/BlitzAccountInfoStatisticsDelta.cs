using System;
namespace WotBlitzStatician.Logic.Dto
{
    public class BlitzAccountInfoStatisticsDelta
    {
        public long AccountId { get; set; }
        public ValueDelta<long, long> Battles { get; set; }
        public ValueDelta<DateTime, TimeSpan> UpdatedAt { get; set; }
        public ValueDelta<long, long> Wins { get; set; }
        public ValueDelta<decimal, decimal> Winrate { get; set; }
        public ValueDelta<decimal, decimal> AvgDamage { get; set; }
		public ValueDelta<decimal, decimal> AvgXp { get; set; }
		public ValueDelta<decimal, decimal> Wn7 { get; set; }
		public ValueDelta<decimal, decimal> AvgTier { get; set; }
        public ValueDelta<decimal, decimal> Effectivity { get; set; }

	    public decimal IntervalWinrate { get; set; }
	    public decimal IntervalAvgDamage { get; set; }
	    public decimal IntervalAvgXp { get; set; }
	    public decimal IntervalAvgTier { get; set; }
	    public decimal IntervalWn7 { get; set; }
	    public decimal IntervalEffectivity { get; set; }

	}
}
