namespace WotBlitzStatician.Data.DataAccessors
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using log4net;
	using Microsoft.EntityFrameworkCore;
	using WotBlitzStatician.Model;

	public class AccountInfoDataAccessor : IAccountInfoDataAccessor
	{
		private static readonly ILog _log = LogManager.GetLogger(typeof(AccountInfoDataAccessor));
		private readonly IBlitzStaticianDataContextFactory _blitzStaticianDataContextFactory;

		public AccountInfoDataAccessor(IBlitzStaticianDataContextFactory blitzStaticianDataContextFactory)
		{
			_blitzStaticianDataContextFactory = blitzStaticianDataContextFactory;
		}

		public AccountInfo GetLastLoggedAccount()
		{
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				return context.AccountInfo.FirstOrDefault(a => a.IsLastSession);
			}
		}

		public DateTime? GetLastAccountUpdatedDate(long accountId)
		{
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				var lastDate = context.AccountInfoStatistics
					.Where(a => a.AccountId == accountId)
					.OrderByDescending(a => a.UpdatedAt)
					.Select(a => a.UpdatedAt)
					.FirstOrDefault();
				return lastDate == DateTime.MinValue ? null : (DateTime?)lastDate;
			}
		}

		public DateTime? GetLastBattleTime(long accountId)
		{
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				var lastBattle = context.AccountInfo
					.Where(a => a.AccountId == accountId)
					.Select(a => a.LastBattleTime)
					.FirstOrDefault();
				return lastBattle == DateTime.MinValue ? null : lastBattle;
			}
		}

		public AccountInfo GetAccountInfo(string nick)
		{
			return GetAccountInfo(a => a.NickName.Equals(nick, StringComparison.CurrentCultureIgnoreCase));
		}

		public AccountInfo GetAccountStatistics(long accountId)
		{
			return GetAccountInfo(a => a.AccountId == accountId);
		}

		public AccountInfoPrivate GetAccountPrivateStatistics(long accountId)
		{
			throw new NotImplementedException();
		}

		public void SetLastSession(long accountId)
		{
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				var accountInfo = context.AccountInfo.FirstOrDefault(a => a.AccountId == accountId);
				var anotherAccounts = context.AccountInfo.Where(a => a.AccountId != accountId && a.IsLastSession).ToList();
				if (accountInfo != null)
				{
					accountInfo.IsLastSession = true;
					anotherAccounts.ForEach(a => a.IsLastSession = false);

					context.SaveChanges();
				}
			}
		}

		public void SaveAccountInfo(AccountInfo accountInfo)
		{
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				context.Merge(context.AccountInfo, new List<AccountInfo> { accountInfo });
				context.AccountInfoStatistics.Add(accountInfo.AccountInfoStatistics);
				context.SaveChanges();
			}
			_log.Debug($"<SaveAccountInfo> saved '{accountInfo.AccountId}' account. UpdatedAt date '{accountInfo.AccountInfoStatistics.UpdatedAt}'");
		}

		public void SaveClanInfo(AccountClanInfo clanInfo)
		{
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				var previousClan = context.AccountClanInfo.FirstOrDefault(c => c.AccountId == clanInfo.AccountId);
				if (previousClan != null)
				{
					context.Entry(previousClan).State = EntityState.Deleted;
					context.SaveChanges();
				}
				context.AccountClanInfo.Add(clanInfo);
				context.SaveChanges();
			}
			_log.Debug($"<SaveClanInfo> saved '{clanInfo.ClanTag}' clan info.");
		}

		public void SaveAccountAchievements(List<AccountInfoAchievment> accountInfoAchievments)
		{
			if (accountInfoAchievments == null || !accountInfoAchievments.Any())
				return;
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				foreach (var accountInfoAchievment in accountInfoAchievments)
				{
					long accountInfoAchievmentId = context.AccountInfoAchievment
						.Where(a => a.AccountId == accountInfoAchievment.AccountId && a.AchievementId == accountInfoAchievment.AchievementId && a.IsMaxSeries == accountInfoAchievment.IsMaxSeries)
						.Select(a => a.AccountInfoAchievementId)
						.FirstOrDefault();
					if (default(long) == accountInfoAchievmentId)
					{
						context.AccountInfoAchievment.Add(accountInfoAchievment);
					}
					else
					{
						accountInfoAchievment.AccountInfoAchievementId = accountInfoAchievmentId;
						context.AccountInfoAchievment.Attach(accountInfoAchievment);
						context.Entry(accountInfoAchievment).State = EntityState.Modified;
					}
				}
				context.SaveChanges();
			}
			_log.Debug($"<SaveAccountAchievements> saved {accountInfoAchievments.Count()} achievements.");
		}

		private AccountInfo GetAccountInfo(Func<AccountInfo, bool> predicate)
		{
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				var accountInfo = context.AccountInfo
					.Join(context.AccountInfoStatistics, a => a.AccountId, s => s.AccountId,
						(a, s) => new { AccountInfo = a, Statistics = s })
					.Where(q => predicate(q.AccountInfo))
					.OrderByDescending(q => q.Statistics.UpdatedAt)
					.FirstOrDefault();


				if (accountInfo == null)
				{
					_log.Debug($"<GetAccountInfo> got nothing.");
					return null;
				}
				_log.Debug($"<GetAccountInfo> got '{accountInfo.AccountInfo.NickName}' account. Last battle at '{accountInfo.AccountInfo.LastBattleTime}'");

				var account = accountInfo.AccountInfo;
				account.AccountInfoStatistics = accountInfo.Statistics;

				// ToDo: Clan info

				/*	            var allAchievements = context
		            .AccountInfoAchievment
					.Where(a => a.AccountId == account.AccountId)
		            .ToList();

	            account.Achievments = allAchievements.Where(a => !a.IsMaxSeries).ToList();
				account.AchievmentsMaxSeries = allAchievements.Where(a => a.IsMaxSeries).ToList();*/

				return account;
			}
		}
	}
}