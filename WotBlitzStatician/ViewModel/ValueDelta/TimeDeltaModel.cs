namespace WotBlitzStatician.ViewModel.ValueDelta
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class TimeDeltaModel
	{
		public TimeSpan PresentValue { get; set; }
		public TimeSpan PastValue { get; set; }
		[DisplayFormat(DataFormatString = "+{0}")]
		public TimeSpan Delta { get; set; }
		public TimeSpan Interval { get; set; }
		public bool IsNegative { get; set; }

	}
}