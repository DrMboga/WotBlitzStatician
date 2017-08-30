﻿namespace WotBlitzStatician.Logic
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using WotBlitzStatician.Model;
	 
	public interface IBlitzStaticianLogic
	{
		Task LoadStatisticsFromWgAsync(long accountId);

        Task LoadStaticDataAndSaveToDb();

		AccountInfo GetLastLoggedAccount();

		Task<AccountInfo> GetAccount(string nick);

		Task<List<AccountInfo>> FindAccounts(string nick);

		AccountInfoStatistics GetAccountStatistics(long accountId);

		AccountInfoPrivate GetAccountPrivateStatistics(long accountId);

		AccountTankStatistics GetAllTanksByAccount(long accountId);
	}
}
