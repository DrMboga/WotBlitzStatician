using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.TypeConfigurations
{
	internal static class PrivateAccountInfoTypeConfiguration
    {
		public static void Configure(EntityTypeBuilder<AccountInfoPrivate> accountInfoPrivateEntity)
		{
			accountInfoPrivateEntity.HasKey(v => v.AccountInfoPrivateId);
			accountInfoPrivateEntity.Ignore(v => v.BattleLifeTime);
				
		}

	}
}
