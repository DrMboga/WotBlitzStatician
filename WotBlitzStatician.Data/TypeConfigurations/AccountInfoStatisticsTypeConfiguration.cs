using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.TypeConfigurations
{
    internal class AccountInfoStatisticsTypeConfiguration
    {
		public static void Configure(EntityTypeBuilder<AccountInfoStatistics> accountInfoStatEntity)
		{
			accountInfoStatEntity.HasKey(s => s.AccountInfoStatisticsId);
			accountInfoStatEntity
				.HasOne(s => s.AccountInfo)
				.WithMany(a => a.AccountInfoStatistics)
				.HasForeignKey(s => s.AccountId);
			accountInfoStatEntity.Ignore(s => s.FragsList);
			accountInfoStatEntity
				.HasOne(s => s.AccountInfoPrivate)
				.WithOne(p => p.AccountInfoStatistics)
				.HasForeignKey<AccountInfoPrivate>(p => p.AccountInfoPrivateId);

		}
	}
}
