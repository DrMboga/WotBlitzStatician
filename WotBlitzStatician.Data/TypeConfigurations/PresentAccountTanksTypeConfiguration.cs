using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.TypeConfigurations
{
	internal class PresentAccountTanksTypeConfiguration
    {
		public static void Configure(EntityTypeBuilder<PresentAccountTanks> presentAccountTanksEntuty)
		{
			presentAccountTanksEntuty.HasKey(a => a.PresentAccountTankId);
			presentAccountTanksEntuty.HasIndex(e => new { e.TankId, e.AccountTankStatisticId });
			presentAccountTanksEntuty.HasIndex(e => e.AccountTankStatisticId );
		}

	}
}
