using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.DataAccessors.Impl.EntityHelpers
{
    internal static class VehicleDictionaryHelper
    {
        public static async Task MergeVehicleType(this BlitzStaticianDbContext dbContext, 
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

		public static async Task MergeVehicles(this BlitzStaticianDbContext dbContext,
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