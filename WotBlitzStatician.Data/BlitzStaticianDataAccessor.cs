﻿namespace WotBlitzStatician.Data
{
	using System.Collections.Generic;
	using WotBlitzStaticitian.Model;

	public class BlitzStaticianDataAccessor : IBlitzStaticianDataAccessor
	{
		// ToDo: Get EF framework from nuget

		public AccountInfo GetAccountInfo(string nick)
		{
			throw new System.NotImplementedException();
		}

		public AccountInfo GetLastLoggedAccount()
		{
			throw new System.NotImplementedException();
		}

		public AccountTankStatistics GetAllTanksByAccount(long accountId)
		{
			throw new System.NotImplementedException();
		}

		public AccountInfoStatistics GetAccountStatistics(long accountId)
		{
			throw new System.NotImplementedException();
		}

		public AccountInfoPrivate GetAccountPrivateStatistics(long accountId)
		{
			throw new System.NotImplementedException();
		}

		public void SaveAccountInfo(AccountInfo accountInfo)
		{
			throw new System.NotImplementedException();
		}

		public void SaveTanksStatistic(List<AccountTankStatistics> taksStat)
		{
			throw new System.NotImplementedException();
		}
	}
}