using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using WotBlitzStatician.Model;
using WotBlitzStatician.Model.Dto;

namespace WotBlitzStatician.Data.DataAccessors
{
	public interface IAccountDataAccessor
    {
		Task<List<AccountInfo>> GetAllAccountsAsync();

		Task<AccountInfoDto> GetActualAccountInfo(long accountId);

		Task<AccountClanInfo> GetAccountClanAsync(long accountId);

		Task<List<AccountTankStatistics>> GetActualTanksAsync(long accountId);

		Task SaveProlongedAccountAsync(long accountId, string accessToken, DateTime accesTokenExpiration);

		Task<IDbContextTransaction> OpenTransactionAsync();

		Task SaveLastBattleInfoAsync(AccountInfo accountInfo);

		Task SaveAccountPrivateInfoAndStatisticsAsync(AccountInfoStatistics accountInfoStatistics);

		Task MergeFragsAsync(List<FragListItem> frags);

		Task SaveAccountClanInfoAsync(long accountId, AccountClanInfo accountClanInfo);

		Task SaveAccountClanHistoryAsync(AccountClanHistory accountClanHistory);

		Task MergeAccountAchievementsAsync(long accountId, 
			List<AccountInfoAchievement> achievements,
			List<AccountInfoAchievement> achievementsMaxSeries);

		Task SaveTankStatisticsAsync(List<AccountTankStatistics> tankStatistics);

		Task MergeAccountInfoTankAchievementsAsync(List<AccountTankStatistics> tanks);

		Task MergePresentAccountTanksInfoAsync(List<PresentAccountTanks> presentAccountTanks);

		Task UpdateInnGarageInfoAsync(List<AccountTankStatistics> tanks);
    }
}
