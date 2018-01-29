namespace WotBlitzStatician.Logic
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using WotBlitzStatician.Model;

	public interface IBlitzStaticianDictionary
	{
		Task SaveDictionaries(
			List<DictionaryLanguage> languages,
			List<DictionaryNations> natons,
			List<DictionaryVehicleType> vehicleTypes,
			List<AchievementSection> achievementSections,
			List<DictionaryClanRole> clanRoles);
		Task SaveVehicles(List<VehicleEncyclopedia> vehicles);
		Task SaveAchievements(List<Achievement> achievements);
	}
}