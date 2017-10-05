namespace WotBlitzStatician.Logic.Dto
{
	using System.Collections.Generic;

	public class BlitzAccountInfoStatisticsDelta
    {
        public long AccountId { get; set; }
		public ValueDelta<decimal, decimal> AvgTier { get; set; }
	    public StatisticsDelta Statistics { get; set; }

		public List<BlitzTankInfoDelta> TanksForPeriod { get; set; }
	}
}
