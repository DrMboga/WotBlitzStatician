using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.TypeConfigurations
{
    internal static class VehicleEncyclopediaTypeConfiguration
    {
		public static void Configure(EntityTypeBuilder<VehicleEncyclopedia> vehicleEncyclopediaEntity)
		{
			vehicleEncyclopediaEntity.HasKey(e => e.TankId);
			vehicleEncyclopediaEntity.HasIndex(e => e.Tier);
			vehicleEncyclopediaEntity.HasIndex(e => e.Type);
			vehicleEncyclopediaEntity.HasIndex(e => e.Nation);

		}

	}
}
