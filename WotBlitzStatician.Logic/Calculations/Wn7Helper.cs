namespace WotBlitzStatician.Logic.Calculations
{
	using System;
	using WotBlitzStatician.Model;

	public static class Wn7Helper
	{
		public static double CalculateWn7(long battles, double tier, double avdFrags, double avgDamage,
			double avgSpot, double avgDef, double winRate)
		{
			double firstSummand = CalculateFirstSummand(tier, avdFrags);
			double secondSummand = CalculateSecondSummand(tier, avgDamage);
			double thirdSummand = CalculateThirdSummand(tier, avgSpot);
			double fourthSummand = CalculateFourthSummand(avgDef);
			double fifthSummand = CalculateFifthSumand(winRate);
			double sixthSummand = CalculateSixthSummand(tier, battles);

			return firstSummand + secondSummand + thirdSummand + fourthSummand + fifthSummand + sixthSummand;
		}

		public static void CalculateWn7(this AccountTankStatistics tank, double tier)
		{
			double avdFrags = (double) tank.Frags / tank.Battles;
			double avgDamage = (double) tank.DamageDealt / tank.Battles;
			double avgSpot = (double) tank.Spotted / tank.Battles;
			double avgDef = (double) tank.DroppedCapturePoints / tank.Battles;
			double winRate = (100d * (double) tank.Wins / tank.Battles); // - 48;

			tank.Wn7 = CalculateWn7(tank.Battles, tier, avdFrags, avgDamage, avgSpot, avgDef, winRate);
		}

		public static void CalculateWn7(this AccountInfoStatistics account)
		{
			double avdFrags = (double) account.Frags / account.Battles;
			double avgDamage = (double) account.DamageDealt / account.Battles;
			double avgSpot = (double) account.Spotted / account.Battles;
			double avgDef = (double) account.DroppedCapturePoints / account.Battles;
			double winRate = (100 * (double) account.Wins / account.Battles); // - 48;

			account.Wn7 = CalculateWn7(account.Battles, account.AvgTier, avdFrags, avgDamage, avgSpot, avgDef, winRate);
		}


		//(1240-1040/(min(TIER,6)^0.164))*FRAGS
		private static double CalculateFirstSummand(double tier, double frags)
		{
			double x = Math.Min(tier, 6);
			x = Math.Pow(x, 0.164d);
			x = 1040d / x;
			x = 1240d - x;
			return x * frags;
		}

		//DAMAGE * 530/(184*exp(0.24*TIER)+130)
		private static double CalculateSecondSummand(double tier, double damage)
		{
			double x = 184d * Math.Exp(0.24d * tier) + 130d;
			return damage * 530d / x;
		}

		//SPOT * 125*min(TIER,3)/3
		private static double CalculateThirdSummand(double tier, double spot)
		{
			return spot * 125d * Math.Min(tier, 3d) / 3;
		}

		//min(DEF,2.2)*100
		private static double CalculateFourthSummand(double def)
		{
			return Math.Min(def, 2.2d) * 100;
		}

		//((185/(0.17+exp(((WINRATE)-35)*-0.134)))-500)*0.45
		private static double CalculateFifthSumand(double winRate)
		{
			double x = (winRate - 35d) * -0.134;
			x = Math.Exp(x) + 0.17d;
			return (185d / x - 500d) * 0.45d;
		}

		//(-1*(((5 - min(TIER,5))*125)/(1 + exp((TIER-(TOTAL/220^(3/TIER)))*1.5))))
		private static double CalculateSixthSummand(double tier, double total)
		{
			double x = -125d * (5d - Math.Min(tier, 5d));
			double y = total / Math.Pow(220d, 3d / tier);
			y = 1.5d * (tier - y);
			y = Math.Exp(y) + 1d;
			return x / y;
		}

		public static double DoubleValue(this long? val)
		{
			if (!val.HasValue)
				return 0d;
			return (double) val.Value;
		}
	}
}