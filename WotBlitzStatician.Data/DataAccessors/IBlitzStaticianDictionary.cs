﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.DataAccessors
{
    public interface IBlitzStaticianDictionary
    {
		void CreateDatabase();

		Task SaveDictionaries(
			List<DictionaryLanguage> languages,
			List<DictionaryNations> natons,
			List<DictionaryVehicleType> vehicleTypes,
			List<AchievementSection> achievementSections,
			List<DictionaryClanRole> clanRoles);
		Task SaveVehicles(List<VehicleEncyclopedia> vehicles);
		Task SaveAchievements(List<Achievement> achievements);

		Task<Dictionary<long, double>> GetVehiclesTires();

		Task<List<string>> GetAllImages();

    Task<string> GetClanRole(string clanRoleId);

    Task<List<VehicleEncyclopedia>> GetVehicles(List<long> tankId);

    Task<List<Achievement>> GetAchievements(List<string> acievementIds);

    Task<List<AchievementSection>> GetAchievementSections();

    Task<List<DictionaryNations>> GetAllNations();

    Task<List<DictionaryVehicleType>> GetAllVehicelTypes();
  }
}
