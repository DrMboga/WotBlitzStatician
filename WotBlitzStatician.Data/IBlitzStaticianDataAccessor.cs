namespace WotBlitzStatician.Data
{
    using System;
    using System.Collections.Generic;
	using WotBlitzStatician.Model;

    public interface IBlitzStaticianDataAccessor
    {
        AccountInfo GetAccountInfo(string nick);

        AccountInfo GetLastLoggedAccount();

        AccountTankStatistics GetAllTanksByAccount(long accountId);

	    AccountInfo GetAccountStatistics(long accountId);

        AccountInfoPrivate GetAccountPrivateStatistics(long accountId);

        void SaveAccountInfo(AccountInfo accountInfo);

	    void SetLastSession(long accountId);

        void SaveTanksStatistic(List<AccountTankStatistics> taksStat);

        void SaveClanInfo(AccountClanInfo clanInfo);

        void SaveLanguagesDictionary(List<DictionaryLanguage> languages);

        void SaveNationsDictionary(List<DictionaryNations> nations);

        void SaveVehicleTypesDictionary(List<DictionaryVehicleType> vehicleTypes);

        void SaveVehicleEncyclopedia(List<VehicleEncyclopedia> vehicles);

        void SaveAchievementsDictionary(List<Achievement> achievements);

        void SaveAccountAchievements(List<AccountInfoAchievment> accountInfoAchievments);

        void SaveAccountTankAchievements(List<AccountInfoTankAchievment> accountInfoTankAchievments);

        DateTime GetStaticDataLastUpdateDate();

	    DateTime? GetLastAccountUpdatedDate(long accountId);

	    Dictionary<long, double> GetVehicleTires();

	    DateTime? GetLastBattleTime(long accountId);

    }
}
