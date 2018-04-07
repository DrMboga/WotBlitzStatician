namespace WotBlitzStatician.Logic.Calculations
{
	using System.Collections.Generic;
	using WotBlitzStatician.Model;

	public static class TierHelper
	{
		public static void CalculateMiddleTier(this AccountInfoStatistics account, IList<AccountTankStatistics> allTanks, Dictionary<long, double> tankTires)
		{
			double x = 0d;
			foreach (var tank in allTanks)
			{
				if (tankTires.ContainsKey(tank.TankId) && tank.Battles > 0)
				{
					x += tankTires[tank.TankId] * tank.Battles;
				}
			}
			if (account.Battles > 0)
				x /= account.Battles;
			account.AvgTier = x;
		}
	}
}