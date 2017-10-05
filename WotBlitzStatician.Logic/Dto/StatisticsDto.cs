namespace WotBlitzStatician.Logic.Dto
{
	using System;

	internal class StatisticsDto
	{
		public long Battles { get; set; }
		public DateTime LastBattle { get; set; }
		public long Wins { get; set; }
		public long DamageDealt { get; set; }
		public long Xp { get; set; }
		public long Frags { get; set; }
		public long Spotted { get; set; }
		public long CapturePoints { get; set; }
		public long DroppedCapturePoints { get; set; }
		public double Tier { get; set; }
		public double Wn7 { get; set; }
		public double Effectivity { get; set; }


	}
}