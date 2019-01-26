using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.DataAccessors.Impl.EntityHelpers
{
    public static class ClanHelper
    {
        public static async Task MergeClanRoles(this BlitzStaticianDbContext dbContext, 
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
		}
    }
}