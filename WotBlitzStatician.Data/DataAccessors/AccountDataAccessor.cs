using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.DataAccessors
{
	public class AccountDataAccessor : IAccountDataAccessor
	{
		private readonly Func<BlitzStaticianDbContext> _getDbContext;

		public AccountDataAccessor(Func<BlitzStaticianDbContext> getDbContext)
		{
			_getDbContext = getDbContext;
		}

		public async Task<List<AccountInfo>> GetAllAccountsAsync()
		{
			using (var context = _getDbContext())
			{
				return await context.AccountInfo.AsNoTracking().ToListAsync();
			}
		}

		public async Task SaveProlongedAccountAsync(long accountId, string accessToken, DateTime accesTokenExpiration)
		{
			using (var context = _getDbContext())
			{
				var accountInfo = await context.AccountInfo
					.SingleAsync(a => a.AccountId == accountId);
				accountInfo.AccessToken = accessToken;
				accountInfo.AccessTokenExpiration = accesTokenExpiration;
				await context.SaveChangesAsync();
			}
		}
	}
}
