using System.Collections.Generic;

namespace WotBlitzStatician.Model.Dto
{
    public class GuestAccountInfo
    {
        public AccountInfoDto AccountInfo { get; set; }

        public List<AccountTankInfoDto> Tanks { get; set; }

        public List<AchievementDto> Achievements { get; set; }

        public List<AccountTanksInfoAggregationDto> AggregatedAccountInfo { get; set; }
    }
}