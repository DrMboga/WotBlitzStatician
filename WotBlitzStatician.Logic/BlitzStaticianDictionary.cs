using System.Collections.Generic;
using System.Threading.Tasks;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Logic
{
	public class BlitzStaticianDictionary : IBlitzStaticianDictionary
	{
		public Task SaveAchievements(List<Achievement> achievements)
		{
			throw new System.NotImplementedException();
		}

		public Task SaveDictionaries(List<DictionaryLanguage> languages, List<DictionaryNations> natons, List<DictionaryVehicleType> vehicleTypes, List<AchievementSection> achievementSections, List<DictionaryClanRole> clanRoles)
		{
			throw new System.NotImplementedException();
		}

		public Task SaveVehicles(List<VehicleEncyclopedia> vehicles)
		{
			throw new System.NotImplementedException();
		}
	}
}
