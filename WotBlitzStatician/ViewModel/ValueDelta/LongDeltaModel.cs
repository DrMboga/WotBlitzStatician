namespace WotBlitzStatician.ViewModel.ValueDelta
{
	using System.ComponentModel.DataAnnotations;

	public class LongDeltaModel
	{
		[DisplayFormat(DataFormatString = "{0:N0}")]
		public long PresentValue { get; set; }
		[DisplayFormat(DataFormatString = "{0:N0}")]
		public long PastValue { get; set; }
		[DisplayFormat(DataFormatString = "+{0:N0}")]
		public long Delta { get; set; }
		[DisplayFormat(DataFormatString = "{0:N0}")]
		public long Interval { get; set; }
		public bool IsNegative { get; set; }
	}
}