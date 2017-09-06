namespace WotBlitzStatician.ViewModel
{
	using System;

	public static class Wn7StyleHelper
	{
		public static Wn7Gradations GetWn7Grade(this double wn7)
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

		public static WinrateGradations GetWinrateGrade(this decimal winrate)
		{
			int winrateInt = Convert.ToInt32(winrate);
			// ToDo: Potensial bug - Enum must be sorted
			foreach (var value in Enum.GetValues(typeof(WinrateGradations)))
			{
				if (winrateInt <= (int)value)
					return (WinrateGradations)value;
			}
			return WinrateGradations.VeryBad;

		}
	}
}