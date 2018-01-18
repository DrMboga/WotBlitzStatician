namespace WotBlitzStatician.Model
{
    public class FragListItem
    {
		public long AccountId { get; set; }
		public long KilledTankId { get; set; }
		public int FragsCount { get; set; }
		public long? TankId { get; set; }
	}
}
