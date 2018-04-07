using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.TypeConfigurations
{
    internal static class AccountInfoTypeConfiguration
    {
		public static void Configure(EntityTypeBuilder<AccountInfo> accountInfoEntity)
		{
			accountInfoEntity.HasKey(a => a.AccountId);
			accountInfoEntity.Property(a => a.AccountId)
				.ValueGeneratedNever();
			accountInfoEntity.HasIndex(a => a.LastBattleTime);
			accountInfoEntity.Ignore(a => a.Achievements);
			accountInfoEntity.Ignore(a => a.AchievementsMaxSeries);
		}

	}
}
