namespace WotBlitzStatician.Logic.Calculations
{
	using System;
	using WotBlitzStatician.Model;

	public static class EffectivityHelper
	{
		/*
РЭ = DAMAGE * (10 / (TIER + 2)) * (0.23 + 2*TIER / 100)
+ FRAGS * 250
+ SPOT * 150
+ log(CAP + 1,1.732) * 150
+ DEF * 150
*/

		public static double CalculateEffectivity(this AccountTankStatistics tank, double tier)
		{
			double avgFrags = (double) tank.Frags / tank.Battles;
			double avgDamage = (double) tank.DamageDealt / tank.Battles;
			double avgSpot = (double) tank.Spotted / tank.Battles;
			double avgCap = (double) tank.CapturePoints / tank.Battles;
			double avgDef = (double) tank.DroppedCapturePoints / tank.Battles;

			return Effectivity(avgDamage, tier, avgFrags, avgSpot, avgCap, avgDef);
		}

		public static double CalculateEffectivity(this AccountInfoStatistics account)
		{
			double avgFrags = (double) account.Frags / account.Battles;
			double avgDamage = (double) account.DamageDealt / account.Battles;
			double avgSpot = (double) account.Spotted / account.Battles;
			double avgCap = (double) account.CapturePoints / account.Battles;
			double avgDef = (double) account.DroppedCapturePoints / account.Battles;
			double tier = account.AvgTier;

			return Effectivity(avgDamage, tier, avgFrags, avgSpot, avgCap, avgDef);
		}

		public static double Effectivity(double avgDamage, double tier, double avgFrags, double avgSpot, double avgCap,
			double avgDef)
		{
			double x = avgDamage * (10 / (tier + 2)) * (0.23 + 2 * tier / 100)
			           + avgFrags * 250
			           + avgSpot * 150
			           + Math.Log((avgCap + 1), 1.732) * 150
			           + avgDef * 150;

			return x;
		}
	}
}