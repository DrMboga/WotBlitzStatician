using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.DataAccessors.Impl.EntityHelpers
{
    internal static class LanguagesDictionaryHelper
    {
        public static async Task MergeLanguages(this BlitzStaticianDbContext dbContext, List<DictionaryLanguage> languages)
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
    }
}