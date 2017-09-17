namespace WotBlitzStatician.ViewModel
{
	using System;

	public static class Wn7StyleHelper
	{
        public static Wn7Gradations GetWn7Grade(this decimal wn7)
        {
            return GetWn7Grade(Convert.ToDouble(wn7));
        }

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

        public static string GetColorByGrade (this Wn7Gradations grade)
		{
			switch (grade)
			{
                case Wn7Gradations.VeryBad:
					return "#000000";
                case Wn7Gradations.Bad:
					return "#cd3333";
                case Wn7Gradations.BelowAverage:
					return "#d77900";
                case Wn7Gradations.Average:
					return "#d7b600";
                case Wn7Gradations.Good:
					return "#6d9521";
                case Wn7Gradations.VeryGood:
					return "#4c762e";
                case Wn7Gradations.Great:
					return "#4a92b7";
                case Wn7Gradations.Unicum:
					return "#83579d";
                case Wn7Gradations.SuperUnicum:
					return "#5a3175";
				default:
					return "#000000";
			}
		}

        public static string GetColorByGrade(this WinrateGradations grade)
		{
			switch (grade)
			{
				case WinrateGradations.VeryBad:
                    return Wn7Gradations.VeryBad.GetColorByGrade();
				case WinrateGradations.Bad:
					return Wn7Gradations.Bad.GetColorByGrade();
				case WinrateGradations.BelowAverage:
					return Wn7Gradations.BelowAverage.GetColorByGrade();
				case WinrateGradations.Average:
					return Wn7Gradations.Average.GetColorByGrade();
				case WinrateGradations.Good:
					return Wn7Gradations.Good.GetColorByGrade();
				case WinrateGradations.VeryGood:
					return Wn7Gradations.VeryGood.GetColorByGrade();
				case WinrateGradations.Great:
					return Wn7Gradations.Great.GetColorByGrade();
				case WinrateGradations.Unicum:
					return Wn7Gradations.Unicum.GetColorByGrade();
				case WinrateGradations.SuperUnicum:
					return Wn7Gradations.SuperUnicum.GetColorByGrade();
				default:
					return Wn7Gradations.VeryBad.GetColorByGrade();
			}
		}

	}
}