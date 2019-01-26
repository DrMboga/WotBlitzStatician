namespace WotBlitzStatician.Model.Dto
{
    public class AccountTankByAchievementDto
    {
        public string AchievementId { get; set; }

        public int AchievementsCount { get; set; }

        public AccountTankInfoDto TankInfo { get; set; }
    }
}