namespace WotBlitzStatician.WotApiClient
{
	using System;

	internal  static class TimeExtrensions
	{
		public static DateTime? ToDateTime(this int? unixTimeStamp)
		{
			if (!unixTimeStamp.HasValue)
				return null;
			// Unix timestamp is seconds past epoch
			var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			dtDateTime = dtDateTime.AddSeconds(unixTimeStamp.Value).ToLocalTime();
			return dtDateTime;
		}

		public static TimeSpan? ToTimeSpan(this int? unixTimeStamp)
		{
			if (!unixTimeStamp.HasValue)
				return null;
			return TimeSpan.FromSeconds(unixTimeStamp.Value);
		}

	}
}