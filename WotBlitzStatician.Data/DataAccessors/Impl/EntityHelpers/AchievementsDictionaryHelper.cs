using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.DataAccessors.Impl.EntityHelpers
{
    internal static class AchievementsDictionaryHelper
    {
        public static async Task MergeAchievementSection(this BlitzStaticianDbContext dbContext, 
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

		public static async Task MergeAchievements(this BlitzStaticianDbContext dbContext,
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
    }
}