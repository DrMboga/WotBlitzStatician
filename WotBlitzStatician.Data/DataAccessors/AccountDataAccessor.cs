﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.DataAccessors
{
	public class AccountDataAccessor : IAccountDataAccessor
	{
		private readonly BlitzStaticianDbContext _dbContext;

		public AccountDataAccessor(BlitzStaticianDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<AccountInfo>> GetAllAccountsAsync()
		{
			return await _dbContext.AccountInfo.AsNoTracking().ToListAsync();
		}

		public async Task SaveProlongedAccountAsync(long accountId, string accessToken, DateTime accesTokenExpiration)
		{
			var accountInfo = await _dbContext.AccountInfo
				.SingleAsync(a => a.AccountId == accountId);
			accountInfo.AccessToken = accessToken;
			accountInfo.AccessTokenExpiration = accesTokenExpiration;
			await _dbContext.SaveChangesAsync();
		}
	}
}
