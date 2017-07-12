namespace WotBlitzStatician.Data
{
    using System;
    using System.Collections.Generic;
    using WotBlitzStatician.Model;

    public class BlitzStaticianDataAccessor : IBlitzStaticianDataAccessor
	{

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

        public void SaveLanguagesDictionary(List<DictionaryLanguage> languages)
        {
            //ToDo: contextFactory
			using (var context = new BlitzStaticianDataContext())
			{
				context.Merge(context.DictionaryLanguage, languages);
				context.SaveChanges();
			}
        }

        public void SaveNationsDictionary(List<DictionaryNations> nations)
        {
			//ToDo: contextFactory
			using (var context = new BlitzStaticianDataContext())
			{
                context.Merge(context.DictionaryNations, nations);
				context.SaveChanges();
			}
		}

        public void SaveVehicleTypesDictionary(List<DictionaryVehicleType> vehicleTypes)
        {
			//ToDo: contextFactory
			using (var context = new BlitzStaticianDataContext())
			{
                context.Merge(context.DictionaryVehicleType, vehicleTypes);
				context.SaveChanges();
			}
        }

        public void SaveVehicleEncyclopedia(List<VehicleEncyclopedia> vehicles)
        {
			//ToDo: contextFactory
            // ToDo: ChangeTableName
			using (var context = new BlitzStaticianDataContext())
			{
                context.Merge(context.VehiclesEncyclopedia, vehicles);
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
            achievements.ForEach(a => achievementOptions.AddRange(a.Options));

            //ToDo: contextFactory
            using (var context = new BlitzStaticianDataContext())
            {
                context.Merge(context.Achievements, achievements);
                context.Merge(context.AchievementOptions, achievementOptions);
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

    }
}
