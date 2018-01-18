namespace WotBlitzStatician.Data.DataAccessors
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using WotBlitzStatician.Model;

	public class StaticInfoDataAccessor : IStaticInfoDataAccessor
	{
		public DateTime GetStaticDataLastUpdateDate()
		{
			throw new NotImplementedException();
		}

		public Dictionary<long, double> GetVehicleTires()
		{
			throw new NotImplementedException();
		}

		public AchievementOption[] GetMarksOfMastery()
		{
			throw new NotImplementedException();
		}

		public void SaveLanguagesDictionary(List<DictionaryLanguage> languages)
		{
			throw new NotImplementedException();
		}

		public void SaveNationsDictionary(List<DictionaryNations> nations)
		{
			throw new NotImplementedException();
		}

		public void SaveVehicleTypesDictionary(List<DictionaryVehicleType> vehicleTypes)
		{
			throw new NotImplementedException();
		}

		public void SaveVehicleEncyclopedia(List<VehicleEncyclopedia> vehicles)
		{
			throw new NotImplementedException();
		}

		public void SaveAchievementsDictionary(List<Achievement> achievements)
		{
			throw new NotImplementedException();
		}
	}
}