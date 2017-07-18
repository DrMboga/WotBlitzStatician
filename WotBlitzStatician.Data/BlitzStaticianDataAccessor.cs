namespace WotBlitzStatician.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using log4net;
    using Microsoft.EntityFrameworkCore;
    using WotBlitzStatician.Model;

	public class BlitzStaticianDataAccessor : IBlitzStaticianDataAccessor
	{
        private static readonly ILog _log = LogManager.GetLogger(typeof(BlitzStaticianDataAccessor));
		private readonly IBlitzStaticianDataContextFactory _blitzStaticianDataContextFactory;

		public BlitzStaticianDataAccessor(IBlitzStaticianDataContextFactory blitzStaticianDataContextFactory)
		{
			_blitzStaticianDataContextFactory = blitzStaticianDataContextFactory;
		}

        // ToDo: refactor - split get methods and save methods
		public AccountInfo GetAccountInfo(string nick)
		{
            using(var context = _blitzStaticianDataContextFactory.CreateContext())
            {
	            var accountInfo = context.AccountInfo
		            .Join(context.AccountInfoStatistics, a => a.AccountId, s => s.AccountId, (a, s) => new {AccountInfo = a, Statistics = s})
		            .Where(q => q.AccountInfo.NickName.Equals(nick, StringComparison.CurrentCultureIgnoreCase))
		            .OrderByDescending(q => q.Statistics.UpdatedAt)
		            .FirstOrDefault();

                _log.Debug($"<GetAccountInfo> got " +
                           $"{(accountInfo == null ? "null" : accountInfo.Statistics.UpdatedAt.ToString())} " +
                           $"updated row by '{nick}' nick");

	            if (accountInfo == null)
		            return null;

	            var account = accountInfo.AccountInfo;
	            account.AccountInfoStatistics = accountInfo.Statistics;


/*	            var allAchievements = context
		            .AccountInfoAchievment
					.Where(a => a.AccountId == account.AccountId)
		            .ToList();

	            account.Achievments = allAchievements.Where(a => !a.IsMaxSeries).ToList();
				account.AchievmentsMaxSeries = allAchievements.Where(a => a.IsMaxSeries).ToList();*/

				return account;
			}
		}

		public AccountInfo GetLastLoggedAccount()
		{
            using(var context = _blitzStaticianDataContextFactory.CreateContext())
            {
                return context.AccountInfo.FirstOrDefault(a => a.IsLastSession);
            }
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
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				context.Merge(context.AccountInfo, new List<AccountInfo> {accountInfo});
				context.AccountInfoStatistics.Add(accountInfo.AccountInfoStatistics);
                context.SaveChanges();
			}
            _log.Debug($"<SaveAccountInfo> saved '{accountInfo.AccountId}' account. UpdatedAt date '{accountInfo.AccountInfoStatistics.UpdatedAt}'");
		}

		public void SaveTanksStatistic(List<AccountTankStatistics> taksStat)
		{
            using(var context = _blitzStaticianDataContextFactory.CreateContext())
            {
                context.AccountTankStatistics.AddRange(taksStat);
                context.SaveChanges();
            }
            _log.Debug($"<SaveTanksStatistic> saved {taksStat.Count()} tanks");
		}

		public void SaveLanguagesDictionary(List<DictionaryLanguage> languages)
		{
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				context.Merge(context.DictionaryLanguage, languages);
				context.SaveChanges();
			}
		}

		public void SaveNationsDictionary(List<DictionaryNations> nations)
		{
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				context.Merge(context.DictionaryNations, nations);
				context.SaveChanges();
			}
		}

		public void SaveVehicleTypesDictionary(List<DictionaryVehicleType> vehicleTypes)
		{
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				context.Merge(context.DictionaryVehicleType, vehicleTypes);
				context.SaveChanges();
			}
		}

		public void SaveVehicleEncyclopedia(List<VehicleEncyclopedia> vehicles)
		{
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				context.Merge(context.VehicleEncyclopedia, vehicles);
				context.SaveChanges();
			}
		}

		public void SaveClanInfo(AccountClanInfo clanInfo)
		{
            using(var context = _blitzStaticianDataContextFactory.CreateContext())
            {
                var previousClan = context.AccountClanInfo.Where(c => c.AccountId == clanInfo.AccountId).FirstOrDefault();
                if(previousClan != null)
                {
                    context.Entry(previousClan).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                context.AccountClanInfo.Add(clanInfo);
                context.SaveChanges();
            }
            _log.Debug($"<SaveClanInfo> saved '{clanInfo.ClanTag}' clan info.");
		}

		public void SaveAchievementsDictionary(List<Achievement> achievements)
		{
			var achievementOptions = new List<AchievementOption>();
            achievements
                .Where(a => a.Options != null).ToList()
                .ForEach(a => achievementOptions.AddRange(a.Options));

			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				context.Merge(context.Achievement, achievements);
				context.Merge(context.AchievementOption, achievementOptions);
				context.SaveChanges();
			}
		}

		public void SaveAccountAchievements(List<AccountInfoAchievment> accountInfoAchievments)
		{
            if(accountInfoAchievments == null || accountInfoAchievments.Count() == 0)
				return;
			// This entity is too complicated for merge. Just delete all achievements for account and add new
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				context.AccountInfoAchievment
					.Where(a => a.AccountId == accountInfoAchievments.First().AccountId)
					.ToList()
					.ForEach(a => context.Entry(a).State = EntityState.Deleted);
				context.SaveChanges();

				context.AccountInfoAchievment.AddRange(accountInfoAchievments);
				context.SaveChanges();
			}
            _log.Debug($"<SaveAccountAchievements> saved {accountInfoAchievments.Count()} achievements.");
		}

		public void SaveAccountTankAchievements(List<AccountInfoTankAchievment> accountInfoTankAchievments)
		{
            if (accountInfoTankAchievments == null || accountInfoTankAchievments.Count() == 0)
				return;
			// This entity is too complicated for merge. Just delete all achievements for account and add new
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
                context.AccountInfoTankAchievment
					.Where(a => a.AccountId == accountInfoTankAchievments.First().AccountId)
					.ToList()
					.ForEach(a => context.Entry(a).State = EntityState.Deleted);
				context.SaveChanges();

				context.AccountInfoTankAchievment.AddRange(accountInfoTankAchievments);
				context.SaveChanges();
			}
            _log.Debug($"<SaveAccountTankAchievements> saved {accountInfoTankAchievments.Count()} tank acievements.");
		}

        public DateTime GetStaticDataLastUpdateDate()
        {
            using(var context = _blitzStaticianDataContextFactory.CreateContext())
            {
                if (!context.DictionaryLanguage.Any())
                    return DefaultDate;
                return context.DictionaryLanguage.First().LastUpdated;
            }
        }

		private static DateTime DefaultDate => new DateTime(1980, 3, 28);

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

		public Dictionary<long, double> GetVehicleTires()
		{
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				return context.VehicleEncyclopedia.Select(v => new {v.TankId, v.Tier}).ToDictionary(k => k.TankId, v => (double) v.Tier);
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
	}
}
