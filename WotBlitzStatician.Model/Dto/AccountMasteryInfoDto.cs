namespace WotBlitzStatician.Model.Dto
{
	public class AccountMasteryInfoDto
	{
		public MarkOfMastery MarkOfMastery { get; set; }
		public int TanksCount { get; set; }
		public int AllTanksCount { get; set; }
		public decimal MasteryTanksRatio => AllTanksCount == 0 ? 0m : (decimal) TanksCount / AllTanksCount;
		public string Image { get; set; }
		public string Description { get; set; }
	}
}