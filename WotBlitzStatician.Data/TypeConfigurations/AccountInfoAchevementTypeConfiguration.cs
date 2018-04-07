using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.TypeConfigurations
{
    internal static class AccountInfoAchevementTypeConfiguration
    {
		public static void Configure(EntityTypeBuilder<AccountInfoAchievement> achievementEntity)
		{
			achievementEntity.HasKey(a => a.AccountInfoAchievementId);
			achievementEntity.HasIndex(a => new { a.AccountId, a.AchievementId });
		}

	}
}
