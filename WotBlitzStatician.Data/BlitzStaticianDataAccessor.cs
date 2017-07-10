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
            throw new NotImplementedException();
        }

        public void SaveNationsDictionary(List<DictionaryNations> nations)
        {
            throw new NotImplementedException();
        }

        public void SaveVehicleTypesDictionary(List<DictionaryVehicleType> vehicleTypes)
        {
            throw new NotImplementedException();
        }

        public void SaveVehicleEncyclopedia(List<VehicleEncyclopedia> vehicles)
        {
            throw new NotImplementedException();
        }

        public void SaveClanInfo(AccountClanInfo clanInfo)
        {
            throw new NotImplementedException();
        }
	
	public void SaveAchievementsDictionary(List<Achievement> achievements)
		{
			throw new NotImplementedException();
		}
    }
}
