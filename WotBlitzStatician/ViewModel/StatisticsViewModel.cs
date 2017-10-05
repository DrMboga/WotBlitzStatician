namespace WotBlitzStatician.ViewModel
{
	using WotBlitzStatician.ViewModel.ValueDelta;

	public class StatisticsViewModel
	{
		public LongDeltaModel Battles { get; set; }

		public DateDeltaModel LastBattle { get; set; }
		public LongDeltaModel Wins { get; set; }
		public DecimalDeltaModel Winrate { get; set; }
		public DecimalDeltaModel AvgDamage { get; set; }
		public DecimalDeltaModel AvgXp { get; set; }
		public DecimalDeltaModel Wn7 { get; set; }
		public DecimalDeltaModel Effectivity { get; set; }

	}
}