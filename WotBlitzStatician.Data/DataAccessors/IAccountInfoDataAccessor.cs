namespace WotBlitzStatician.Data.DataAccessors
{
	using System;
	using System.Collections.Generic;
	using WotBlitzStatician.Model;

	public interface IAccountInfoDataAccessor
	{
		AccountInfo GetLastLoggedAccount();

		DateTime? GetLastAccountUpdatedDate(long accountId);

		DateTime? GetLastBattleTime(long accountId);

		AccountInfo GetAccountInfo(string nick);

		AccountInfo GetAccountStatistics(long accountId);

		AccountInfoPrivate GetAccountPrivateStatistics(long accountId);

		void SetLastSession(long accountId);

		void SaveAccountInfo(AccountInfo accountInfo);

		void SaveClanInfo(AccountClanInfo clanInfo);

		void SaveAccountAchievements(List<AccountInfoAchievment> accountInfoAchievments);
	}
}