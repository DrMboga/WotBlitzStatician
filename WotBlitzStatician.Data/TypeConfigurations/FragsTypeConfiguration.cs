using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.TypeConfigurations
{
    internal static class FragsTypeConfiguration
    {
		public static void Configure(EntityTypeBuilder<FragListItem> fragsEntity)
		{
			fragsEntity.HasKey(f => f.FragListItemId);
			fragsEntity.HasIndex(f => new { f.AccountId, f.KilledTankId, f.TankId });
			fragsEntity.HasIndex(f => new { f.AccountId, f.KilledTankId });
		}

	}
}
