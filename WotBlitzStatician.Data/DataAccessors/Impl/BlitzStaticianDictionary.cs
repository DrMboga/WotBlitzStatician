namespace WotBlitzStatician.Data.DataAccessors.Impl
{
	using System;
	using System.Diagnostics;
	using Microsoft.Extensions.Logging;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.EntityFrameworkCore;
	using WotBlitzStatician.Model;
    using WotBlitzStatician.Data.DataAccessors.Impl.EntityHelpers;

    public class BlitzStaticianDictionary : IBlitzStaticianDictionary
	{
		private readonly Func<BlitzStaticianDbContext> _getDbContext;
		private readonly ILogger<BlitzStaticianDictionary> _logger;

		public BlitzStaticianDictionary(Func<BlitzStaticianDbContext> getDbContext, ILogger<BlitzStaticianDictionary> logger)
		{
			_getDbContext = getDbContext;
			_logger = logger;
		}

        public void CreateDatabase()
        {
			using (var dbContext = _getDbContext())
			{
            	dbContext.Database.Migrate();
			}
        }

		public async Task SaveAchievements(List<Achievement> achievements)
		{
			using (var dbContext = _getDbContext())
			{
				await dbContext.MergeAchievements(achievements);

				dbContext.SaveChanges();
			}
		}

		public async Task SaveDictionaries(
			List<DictionaryLanguage> languages, 
			List<DictionaryNations> natons, 
			List<DictionaryVehicleType> vehicleTypes, 
			List<AchievementSection> achievementSections, 
			List<DictionaryClanRole> clanRoles)
		{
			using (var dbContext = _getDbContext())
			{
				await dbContext.MergeLanguages(languages);
				await dbContext.MergeNations(natons);
				await dbContext.MergeVehicleType(vehicleTypes);
				await dbContext.MergeAchievementSection(achievementSections);
				await dbContext.MergeClanRoles(clanRoles);

				await dbContext.SaveChangesAsync();
			}
		}

		public async Task SaveVehicles(List<VehicleEncyclopedia> vehicles)
		{
			using (var dbContext = _getDbContext())
			{
				await dbContext.MergeVehicles(vehicles);
				await dbContext.SaveChangesAsync();
			}
		}

		public async Task<Dictionary<long, double>> GetVehiclesTires()
		{
			using (var dbContext = _getDbContext())
			{
				return await dbContext.VehicleEncyclopedia
					.Select(v => new { v.TankId, v.Tier })
					.ToDictionaryAsync(s => s.TankId, s => Convert.ToDouble(s.Tier));
			}
		}

		public async Task<List<string>> GetAllImages()
		{
			using (var dbContext = _getDbContext())
			{
				return await dbContext.Achievement.AsNoTracking()
					.Where(a => a.Image != null)
					.Select(a => a.Image)
					.Union(dbContext.AchievementOption.AsNoTracking()
						.Where(o => o.Image != null)
						.Select(o => o.Image))
					.Union(dbContext.VehicleEncyclopedia.AsNoTracking()
						.Where(v => v.PreviewImageUrl != null)
						.Select(v => v.PreviewImageUrl))
					.ToListAsync();
			}
		}
    }
}
