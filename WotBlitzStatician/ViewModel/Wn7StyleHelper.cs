namespace WotBlitzStatician.ViewModel
{
	using System;
	using System.Collections.Generic;

	public static class Wn7StyleHelper
	{
		public static Wn7Gradations GetWn7(this double wn7)
		{
			int wn7Int = Convert.ToInt32(wn7);
			// ToDo: Potensial bug - Enum must be sorted
			foreach (var value in Enum.GetValues(typeof(Wn7Gradations)))
			{
				if (wn7Int <= (int) value)
					return (Wn7Gradations) value;
			}
			return Wn7Gradations.VeryBad;
		}

		private static Dictionary<Wn7Gradations, string> _wn7GradationStyles = new Dictionary<Wn7Gradations, string>
		{
			{Wn7Gradations.VeryBad, "wn7_very_bad"},
			{Wn7Gradations.Bad, "wn7_bad"},
			{Wn7Gradations.BelowAverage, "wn7_below_average"},
			{Wn7Gradations.Average, "wn7_average"},
			{Wn7Gradations.Good, "wn7_good"},
			{Wn7Gradations.VeryGood, "wn7_very_good"},
			{Wn7Gradations.Great, "wn7_great"},
			{Wn7Gradations.Unicum, "wn7_unicum"},
			{Wn7Gradations.SuperUnicum, "wn7_super_unicum"},
		};

		public static string GetWn7GradationStyle(this Wn7Gradations wn7Gradation)
		{
			return _wn7GradationStyles[wn7Gradation];
		}
	}
}