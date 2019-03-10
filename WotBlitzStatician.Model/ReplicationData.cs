using System.Collections.Generic;

namespace WotBlitzStatician.Model
{
    public class ReplicationData
    {
        public List<AccountInfo> AccountInfo { get; set; }
        public List<AccountInfoStatistics> AccountInfoStatistics { get; set; }
        public List<AccountClanHistory> AccountClanHistory { get; set; }
        public List<AccountClanInfo> AccountClanInfo { get; set; }
        public List<AccountInfoAchievement> AccountInfoAchievements { get; set; }
        public List<AccountInfoTankAchievement> AccountInfoTankAchievements { get; set; }
        public List<AccountTankStatistics> AccountTanksStatistics { get; set; }
        public List<PresentAccountTanks> PresentAccountTanks { get; set; }
        public List<FragListItem> Frags { get; set; }
  }
}