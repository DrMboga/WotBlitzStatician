namespace WotBlitzStatician.Data.DataAccessors
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using WotBlitzStatician.Model;

	public class AccountInfoDataAccessor : IAccountInfoDataAccessor
	{

		public AccountInfo GetLastLoggedAccount()
		{
			throw new NotImplementedException();
		}

		public DateTime? GetLastAccountUpdatedDate(long accountId)
		{
			throw new NotImplementedException();
		}

		public DateTime? GetLastBattleTime(long accountId)
		{
			throw new NotImplementedException();
		}

		public AccountInfo GetAccountInfo(string nick)
		{
			throw new NotImplementedException();
		}

		public AccountInfo GetAccountStatistics(long accountId)
		{
			throw new NotImplementedException();
		}

		public AccountInfoPrivate GetAccountPrivateStatistics(long accountId)
		{
			throw new NotImplementedException();
		}

		public void SetLastSession(long accountId)
		{
			throw new NotImplementedException();
		}

		public void SaveAccountInfo(AccountInfo accountInfo)
		{
			throw new NotImplementedException();
		}

		public void SaveClanInfo(AccountClanInfo clanInfo)
		{
			throw new NotImplementedException();
		}

		public void SaveAccountAchievements(List<AccountInfoAchievment> accountInfoAchievments)
		{
			throw new NotImplementedException();
		}

		private AccountInfo GetAccountInfo(Func<AccountInfo, bool> predicate)
		{
			throw new NotImplementedException();
		}
	}
}