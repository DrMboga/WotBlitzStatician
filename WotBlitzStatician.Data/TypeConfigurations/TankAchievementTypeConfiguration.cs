using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.TypeConfigurations
{
    internal static class TankAchievementTypeConfiguration
    {
		public static void Configure(EntityTypeBuilder<AccountInfoTankAchievement> achievementEntity)
		{
			achievementEntity.HasIndex(a => new { a.AccountId, a.AchievementId, a.TankId });

		}

	}
}
