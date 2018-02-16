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
	}
}
