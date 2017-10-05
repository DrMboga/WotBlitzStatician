namespace WotBlitzStatician.ViewModel.ValueDelta
{
	using System.ComponentModel.DataAnnotations;

	public class DecimalDeltaModel
	{
		[DisplayFormat(DataFormatString = "{0:N2}")]
		public decimal PresentValue { get; set; }
		[DisplayFormat(DataFormatString = "{0:N2}")]
		public decimal PastValue { get; set; }
		public string Delta { get; set; }
		[DisplayFormat(DataFormatString = "{0:N2}")]
		public decimal Interval { get; set; }
		public bool IsNegative { get; set; }

	}
}