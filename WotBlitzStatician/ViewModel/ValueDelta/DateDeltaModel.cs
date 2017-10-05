namespace WotBlitzStatician.ViewModel.ValueDelta
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class DateDeltaModel
	{
		public DateTime PresentValue { get; set; }
		public DateTime PastValue { get; set; }
		[DisplayFormat(DataFormatString = "+{0}")]
		public TimeSpan Delta { get; set; }
		public TimeSpan Interval { get; set; }
		public bool IsNegative { get; set; }

	}
}