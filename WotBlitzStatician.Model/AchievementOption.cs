namespace WotBlitzStatician.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

	public class AchievementOption
    {
        [Key]
        public int AcievementOptionId { get; set; }
        [ForeignKey("Achievement")]
        public string AchievementId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string ImageBig { get; set; }
    }
}
