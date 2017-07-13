namespace WotBlitzStatician.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
                var account = context.AccountInfo.FirstOrDefault(a => a.NickName.Equals(nick, StringComparison.CurrentCultureIgnoreCase));
                if (account == null)
                    return null;
                // ToDo: use join
                account.AccountInfoStatistics = context
                    .AccountInfoStatistics
                    .Where(a => a.AccountId == account.AccountId)
                    .OrderByDescending(i => i.UpdatedAt)
                    .FirstOrDefault();
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
			throw new System.NotImplementedException();
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
			throw new NotImplementedException();
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
