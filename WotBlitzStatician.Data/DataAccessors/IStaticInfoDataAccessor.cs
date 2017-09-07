namespace WotBlitzStatician.Data.DataAccessors
{
	using System;
	using System.Collections.Generic;
	using WotBlitzStatician.Model;

	public interface IStaticInfoDataAccessor
	{
		DateTime GetStaticDataLastUpdateDate();
		Dictionary<long, double> GetVehicleTires();


		void SaveLanguagesDictionary(List<DictionaryLanguage> languages);

		void SaveNationsDictionary(List<DictionaryNations> nations);

		void SaveVehicleTypesDictionary(List<DictionaryVehicleType> vehicleTypes);

		void SaveVehicleEncyclopedia(List<VehicleEncyclopedia> vehicles);

		void SaveAchievementsDictionary(List<Achievement> achievements);
	}
}