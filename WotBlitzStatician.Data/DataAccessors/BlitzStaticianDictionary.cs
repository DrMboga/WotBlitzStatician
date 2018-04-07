namespace WotBlitzStatician.Data.DataAccessors
{
	using System;
	using System.Diagnostics;
	using Microsoft.Extensions.Logging;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.EntityFrameworkCore;
	using WotBlitzStatician.Model;

	public class BlitzStaticianDictionary : IBlitzStaticianDictionary
	{
		private readonly Func<BlitzStaticianDbContext> _getDbContext;
		private readonly ILogger<BlitzStaticianDictionary> _logger;

		public BlitzStaticianDictionary(Func<BlitzStaticianDbContext> getDbContext, ILogger<BlitzStaticianDictionary> logger)
		{
			_getDbContext = getDbContext;
			_logger = logger;
		}

		public async Task SaveAchievements(List<Achievement> achievements)
		{
			using (var dbContext = _getDbContext())
			{
				await MergeAchievements(dbContext, achievements);

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
				await MergeLanguages(dbContext, languages);
				await MergeNations(dbContext, natons);
				await MergeVehicleType(dbContext, vehicleTypes);
				await MergeAchievementSection(dbContext, achievementSections);
				await MergeClanRoles(dbContext, clanRoles);

				await dbContext.SaveChangesAsync();
			}
		}

		public async Task SaveVehicles(List<VehicleEncyclopedia> vehicles)
		{
			using (var dbContext = _getDbContext())
			{
				await MergeVehicles(dbContext, vehicles);
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


		// ToDo: Move to extensions
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

		private async Task MergeClanRoles(BlitzStaticianDbContext dbContext, 
			List<DictionaryClanRole> clanRoles)
		{
			var stopwatch = Stopwatch.StartNew();
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
			stopwatch.Stop();
			_logger.LogInformation($"MergeClanRoles took '{stopwatch.ElapsedMilliseconds}' ms");
		}

		private async Task MergeClanRolesThirdWay(BlitzStaticianDbContext dbContext, 
			List<DictionaryClanRole> clanRoles)
		{
			var stopwatch = Stopwatch.StartNew();
			foreach (var dictionaryClanRole in clanRoles)
			{
				var exists = await dbContext.DictionaryClanRole.AnyAsync(r => r.ClanRoleId == dictionaryClanRole.ClanRoleId);
				dbContext.DictionaryClanRole.Attach(dictionaryClanRole);
				dbContext.Entry(dictionaryClanRole).State = exists
					? EntityState.Modified
					: EntityState.Added;
			}
			stopwatch.Stop();
			_logger.LogInformation($"MergeClanRolesThirdWay took '{stopwatch.ElapsedMilliseconds}' ms");
		}

		private static async Task MergeAchievements(BlitzStaticianDbContext dbContext,
			List<Achievement> achievements)
		{
			await DeleteAchievementOptions(dbContext);

			var ids = achievements.Select(l => l.AchievementId);
			var existing = await dbContext.Achievement
				.Where(l => ids.Contains(l.AchievementId))
				.Select(l => l.AchievementId)
				.AsNoTracking()
				.ToListAsync();

			achievements.ForEach(l =>
			{
				dbContext.Achievement.Attach(l);
				dbContext.Entry(l).State = existing.Contains(l.AchievementId)
					? EntityState.Modified
					: EntityState.Added;
			});
		}
		private static async Task DeleteAchievementOptions(BlitzStaticianDbContext dbContext)
		{
			await dbContext.Database.ExecuteSqlCommandAsync("TRUNCATE TABLE wotb.AchievementOption");
		}

		private static async Task MergeVehicles(BlitzStaticianDbContext dbContext,
			List<VehicleEncyclopedia> vehicles)
		{
			var ids = vehicles.Select(l => l.TankId);
			var existing = await dbContext.VehicleEncyclopedia
				.Where(l => ids.Contains(l.TankId))
				.Select(l => l.TankId)
				.ToListAsync();

			vehicles.ForEach(v =>
			{
				dbContext.VehicleEncyclopedia.Attach(v);
				dbContext.Entry(v).State = existing.Contains(v.TankId)
					? EntityState.Modified
					: EntityState.Added;
			});
		}
	}
}
