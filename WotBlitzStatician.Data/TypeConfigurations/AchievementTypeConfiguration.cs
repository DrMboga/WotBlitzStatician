using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.TypeConfigurations
{
    internal static class AchievementTypeConfiguration
    {
		public static void Configure(EntityTypeBuilder<Achievement> achevementEntity)
		{
			achevementEntity.HasKey(a => a.AchievementId);
			achevementEntity.HasOne(a => a.AchievementSection)
				.WithMany(s => s.Achievements)
				.HasForeignKey(a => a.Section);
		}

	}
}
