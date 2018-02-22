using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.DataAccessors
{
	public interface IAccountDataAccessor
    {
		Task<List<AccountInfo>> GetAllAccountsAsync();

		Task SaveProlongedAccountAsync(long accountId, string accessToken, DateTime accesTokenExpiration);
    }
}
