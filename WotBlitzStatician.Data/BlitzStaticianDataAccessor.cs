namespace WotBlitzStatician.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using WotBlitzStatician.Model;

	public class BlitzStaticianDataAccessor : IBlitzStaticianDataAccessor
	{
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
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				context.Merge(context.AccountInfo, new List<AccountInfo> {accountInfo});
				context.AccountInfoStatistics.Add(accountInfo.AccountInfoStatistics);
			}
		}

		public void SaveTanksStatistic(List<AccountTankStatistics> taksStat)
		{
			throw new System.NotImplementedException();
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
			throw new NotImplementedException();
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
			if(accountInfoAchievments == null)
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
		}

		public void SaveAccountTankAchievements(List<AccountInfoTankAchievment> accountInfoTankAchievments)
		{
			throw new NotImplementedException();
		}

        public DateTime GetStaticDataLastUpdateDate()
        {
            using(var context = _blitzStaticianDataContextFactory.CreateContext())
            {
                if (context.DictionaryLanguage.Count() == 0)
                    return new DateTime(1980, 3, 28);
                return context.DictionaryLanguage.First().LastUpdated;
            }
        }

	}
}
