using System.Collections.Generic;
using System.Threading.Tasks;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.DataAccessors
{
	public class BlitzStaticianDictionary : IBlitzStaticianDictionary
	{
		private readonly BlitzStaticianDbContext _dbContext;

		public BlitzStaticianDictionary(BlitzStaticianDbContext dbContext)
		{
			_dbContext = dbContext;
		}

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
