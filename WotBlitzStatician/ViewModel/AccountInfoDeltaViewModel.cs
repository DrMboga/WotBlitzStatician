namespace WotBlitzStatician.ViewModel
{
	using WotBlitzStatician.ViewModel.ValueDelta;

	public class AccountInfoDeltaViewModel
    {
        public long AccountId { get; set; }

	    public StatisticsViewModel Statistics { get; set; }

	    public DecimalDeltaModel AvgTier { get; set; }
	}
}
