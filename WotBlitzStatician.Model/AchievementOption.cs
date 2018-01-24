namespace WotBlitzStatician.Model
{
	public class AchievementOption
    {
        public int AcievementOptionId { get; set; }
		public Achievement Achievement { get; set; }
		public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string ImageBig { get; set; }
    }
}
