namespace WotBlitzStatician.Model
{

	public class AccountInfoAchievement
	{
		public long AccountInfoAchievementId { get; set; }

		public string AchievementId { get; set; }

		public long AccountId { get; set; }

		public int Count { get; set; }

		public bool IsMaxSeries { get; set; }
	}
}