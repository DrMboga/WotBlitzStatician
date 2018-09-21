using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WotBlitzStatician.Model;
using WotBlitzStatician.Model.Dto;

namespace WotBlitzStatician.Data.DataAccessors
{
	public class ClanInfoDataAccessor : IClanInfoDataAccessor
	{
		private readonly BlitzStaticianDbContext _dbContext;

		public ClanInfoDataAccessor(BlitzStaticianDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<PlayerClanInfoDto> GetClanInfo(long accountId)
		{
			return await _dbContext.AccountClanInfo.AsNoTracking()
				.Join(_dbContext.DictionaryClanRole, c => c.PlayerRole, r => r.ClanRoleId,
				(c, r) => new {
					c.AccountId,
					ClanInfo = new PlayerClanInfoDto
					{
						ClanId = c.ClanId,
						PlayerJoinedAt = c.PlayerJoinedAt,
						PlayerRole = r.RoleName,
						ClanTag = c.ClanTag,
						ClanName = c.ClanName,
						ClanMotto = c.ClanMotto,
						ClanDescription = c.ClanDescription
					}
				})
				.Where(c => c.AccountId == accountId)
				.Select(j => j.ClanInfo)
				.FirstOrDefaultAsync();
		}

		public async Task<AccountClanInfo> GetAccountClanAsync(long accountId)
		{
			return await _dbContext.AccountClanInfo
							.AsNoTracking()
							.Where(a => a.AccountId == accountId)
							.FirstOrDefaultAsync();
		}

	}
}
