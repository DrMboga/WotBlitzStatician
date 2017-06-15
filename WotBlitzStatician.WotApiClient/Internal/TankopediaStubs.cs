namespace WotBlitzStatician.WotApiClient
{
	using System.Collections.Generic;
	using System.Linq;
	using WotBlitzStatician.WotApiClient.InternalModel;

	internal static class TankopediaStubs
	{
		/// <summary>
		/// It's a cratch. There is no MarkI tank in the API.
		/// </summary>
		/// <param name="vehicles"></param>
		public static void AddMarkI(this List<WotEncyclopediaVehiclesResponse> vehicles)
		{
			if (vehicles.Any(v => v.TankId == 64081))
				return;
			vehicles.Add(new WotEncyclopediaVehiclesResponse
				{
					TankId = 64081,
					Name = "Mk I* HeavyTank",
					Tier = 1,
					Nation = "uk",
					Type = "heavyTank",
					IsPremium = true
				}
			);
		}

		/// <summary>
		/// It's a cratch. There is no Hetzer Kame Sp yet tank in the API.
		/// </summary>
		/// <param name="vehicles"></param>
		public static void AddHetzerKame(this List<WotEncyclopediaVehiclesResponse> vehicles)
		{
			if (vehicles.Any(v => v.TankId == 52065))
				return;
			vehicles.Add(new WotEncyclopediaVehiclesResponse
				{
					TankId = 52065,
					Name = "Hetzer Kame SP",
					Tier = 4,
					Nation = "japan",
					Type = "AT-SPG",
					IsPremium = true
				}
			);
		}
	}
}