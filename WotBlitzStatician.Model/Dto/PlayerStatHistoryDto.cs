using System;
using System.Collections.Generic;
using System.Text;

namespace WotBlitzStatician.Model.Dto
{
    public class PlayerStatHistoryDto
    {
		public DateTime UpdatedAt { get; set; }
		public long Battles { get; set; }
		public long? BattlesDiff { get; set; }
		public long Wins { get; set; }

		public decimal AvgTier { get; set; }
		public decimal? AvgTierDiff { get; set; }
		public decimal Wn7 { get; set; }
		public decimal? Wn7Diff { get; set; }
		public decimal WinRate { get; set; }
		public decimal? WinRateDiff { get; set; }
		public long AvgDamage { get; set; }
		public long? AvgDamageDiff { get; set; }
		public long AvgXp { get; set; }
		public long? AvgXpDiff { get; set; }
		public decimal SurvivalRate { get; set; }
		public decimal? SurvivalRateDiff { get; set; }
	}
}
