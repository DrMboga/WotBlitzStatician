namespace WotBlitzStatician.Model
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class AccountInfoAchievment
	{
		[Key]
		public string AccountInfoAchievmentId { get; set; }

		[ForeignKey("AccountInfo")]
		public long AccountId { get; set; }

		public int Count { get; set; }

		public bool IsMaxSeries { get; set; }
	}
}