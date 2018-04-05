using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.TypeConfigurations
{
	internal class AccountClanInfoTypeConfiguration
    {
		public static void Configure(EntityTypeBuilder<AccountClanInfo> accountClanInfoEntity)
		{
			accountClanInfoEntity.HasKey(v => v.AccountClanInfoId);
			accountClanInfoEntity.HasIndex(t => t.AccountId);
			accountClanInfoEntity.HasIndex(t => new { t.AccountId, t.ClanId });
		}

	}
}
