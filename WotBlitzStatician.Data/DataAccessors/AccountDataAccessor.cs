using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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

		public async Task<IDbContextTransaction> OpenTransactionAsync()
		{
			return await _dbContext.Database.BeginTransactionAsync();
		}

		public async Task SaveLastBattleInfoAsync(AccountInfo accountInfo)
		{
			var accountFromDb = await _dbContext.AccountInfo
				.SingleAsync(a => a.AccountId == accountInfo.AccountId);
			accountFromDb.AccountCreatedAt = accountInfo.AccountCreatedAt;
			accountFromDb.LastBattleTime = accountInfo.LastBattleTime;
			await _dbContext.SaveChangesAsync();
		}

		public async Task SaveAccountPrivateInfoAndStatisticsAsync(AccountInfoStatistics accountInfoStatistics)
		{
			_dbContext.AccountInfoStatistics.Add(accountInfoStatistics);
			await _dbContext.SaveChangesAsync();
		}

		public async Task MergeFragsAsync(List<FragListItem> frags)
		{
			if(frags == null || frags.Count == 0)
			{
				return;
			}

			var accountId = frags.First().AccountId;

			bool fragsByAccount = frags.All(f => f.TankId == null);

			List<FragListItem> existingFrags = null;
			if(fragsByAccount)
			{
				existingFrags = await _dbContext.Frags
					.Where(f => f.AccountId == accountId && f.TankId == null)
					.ToListAsync();
			}
			else
			{
				existingFrags = await _dbContext.Frags
					.Where(f => f.AccountId == accountId && f.TankId != null)
					.ToListAsync();
			}

			foreach (var frag in frags)
			{
				var existing = existingFrags.FirstOrDefault(f =>
								f.KilledTankId == frag.KilledTankId &&
								((fragsByAccount) || (!fragsByAccount && f.TankId == frag.TankId)));
				if (existing == null)
				{
					_dbContext.Frags.Add(frag);
				}
				else
				{
					existing.FragsCount = frag.FragsCount;
				}
			}
			await _dbContext.SaveChangesAsync();
		}
	}
}
