using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.DataAccessors.Impl.EntityHelpers
{
    internal static class NationsDictionaryHelper
    {
        public static async Task MergeNations(this BlitzStaticianDbContext dbContext, List<DictionaryNations> nations)
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

    }
}