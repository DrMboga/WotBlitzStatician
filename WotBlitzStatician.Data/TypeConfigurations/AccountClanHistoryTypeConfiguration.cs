using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.TypeConfigurations
{
	internal static class AccountClanHistoryTypeConfiguration
    {
		public static void Configure(EntityTypeBuilder<AccountClanHistory> accountClanHistoryEntity)
		{
			accountClanHistoryEntity.HasKey(a => a.AccountClanHistoryId);
			accountClanHistoryEntity.HasIndex(a => new { a.AccountId });
		}
	}
}
