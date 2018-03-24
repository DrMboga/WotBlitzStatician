using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.DataAccessors
{
	public interface IAccountDataAccessor
    {
		Task<List<AccountInfo>> GetAllAccountsAsync();

		Task SaveProlongedAccountAsync(long accountId, string accessToken, DateTime accesTokenExpiration);

		Task<IDbContextTransaction> OpenTransactionAsync();

		Task SaveLastBattleInfoAsync(AccountInfo accountInfo);

		Task SaveAccountPrivateInfoAndStatisticsAsync(AccountInfoStatistics accountInfoStatistics);

		Task MergeFragsAsync(List<FragListItem> frags);

		Task SaveAccountClanInfoAsync(long accountId, AccountClanInfo accountClanInfo);
    }
}
