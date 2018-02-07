using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

		public async Task SaveDictionaries(
			List<DictionaryLanguage> languages, 
			List<DictionaryNations> natons, 
			List<DictionaryVehicleType> vehicleTypes, 
			List<AchievementSection> achievementSections, 
			List<DictionaryClanRole> clanRoles)
		{
			await MergeLanguages(_dbContext, languages);
			await MergeNations(_dbContext, natons);
			await MergeVehicleType(_dbContext, vehicleTypes);
			await MergeAchievementSection(_dbContext, achievementSections);
			await MergeClanRoles(_dbContext, clanRoles);

			await _dbContext.SaveChangesAsync();
		}

		public Task SaveVehicles(List<VehicleEncyclopedia> vehicles)
		{
			throw new System.NotImplementedException();
		}

		private static async Task MergeLanguages(BlitzStaticianDbContext dbContext, List<DictionaryLanguage> languages)
		{
			var langIds = languages.Select(l => l.LanguageId);
			var existing = await dbContext.DictionaryLanguage
				.Where(l => langIds.Contains(l.LanguageId))
				.Select(l => l.LanguageId)
				.AsNoTracking()
				.ToListAsync();

			languages.ForEach(l =>
			{
				dbContext.DictionaryLanguage.Attach(l);
				dbContext.Entry(l).State = existing.Contains(l.LanguageId)
					? EntityState.Modified
					: EntityState.Added;
			});

		}

		private static async Task MergeNations(BlitzStaticianDbContext dbContext, List<DictionaryNations> nations)
		{
			var nationIds = nations.Select(l => l.NationId);
			var existing = await dbContext.DictionaryNation
				.Where(l => nationIds.Contains(l.NationId))
				.Select(l => l.NationId)
				.AsNoTracking()
				.ToListAsync();

			nations.ForEach(l =>
			{
				dbContext.DictionaryNation.Attach(l);
				dbContext.Entry(l).State = existing.Contains(l.NationId)
					? EntityState.Modified
					: EntityState.Added;
			});
		}

		private static async Task MergeVehicleType(BlitzStaticianDbContext dbContext, 
			List<DictionaryVehicleType> vehicleTypes)
		{
			var ids = vehicleTypes.Select(l => l.VehicleTypeId);
			var existing = await dbContext.DictionaryVehicleType
				.Where(l => ids.Contains(l.VehicleTypeId))
				.Select(l => l.VehicleTypeId)
				.AsNoTracking()
				.ToListAsync();

			vehicleTypes.ForEach(l =>
			{
				dbContext.DictionaryVehicleType.Attach(l);
				dbContext.Entry(l).State = existing.Contains(l.VehicleTypeId)
					? EntityState.Modified
					: EntityState.Added;
			});
		}

		private static async Task MergeAchievementSection(BlitzStaticianDbContext dbContext, 
			List<AchievementSection> achievementSections)
		{
			var ids = achievementSections.Select(l => l.Section);
			var existing = await dbContext.AchievementSection
				.Where(l => ids.Contains(l.Section))
				.Select(l => l.Section)
				.AsNoTracking()
				.ToListAsync();

			achievementSections.ForEach(l =>
			{
				dbContext.AchievementSection.Attach(l);
				dbContext.Entry(l).State = existing.Contains(l.Section)
					? EntityState.Modified
					: EntityState.Added;
			});
		}

		private static async Task MergeClanRoles(BlitzStaticianDbContext dbContext, 
			List<DictionaryClanRole> clanRoles)
		{
			var ids = clanRoles.Select(l => l.ClanRoleId);
			var existing = await dbContext.DictionaryClanRole
				.Where(l => ids.Contains(l.ClanRoleId))
				.Select(l => l.ClanRoleId)
				.AsNoTracking()
				.ToListAsync();

			clanRoles.ForEach(l =>
			{
				dbContext.DictionaryClanRole.Attach(l);
				dbContext.Entry(l).State = existing.Contains(l.ClanRoleId)
					? EntityState.Modified
					: EntityState.Added;
			});
		}

	}
}
