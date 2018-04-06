using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.TypeConfigurations
{
    internal static class AccountTankStatisticsTypeConfiguration
    {
		public static void Configure(EntityTypeBuilder<AccountTankStatistics> tankInfoEntity)
		{
			tankInfoEntity.HasKey(t => t.AccountTankStatisticId);
			tankInfoEntity.HasIndex(t => t.AccountId);
			tankInfoEntity.HasIndex(t => t.TankId);
			tankInfoEntity.Ignore(t => t.BattleLifeTime);
			tankInfoEntity.Ignore(t => t.VehicleInfo);
			tankInfoEntity.Ignore(t => t.FragsList);
			tankInfoEntity.Ignore(t => t.Achievements);
			tankInfoEntity.Ignore(t => t.AchievementsMaxSeries);
		}

	}
}
