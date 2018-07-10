using WotBlitzStatician.Model.Common;

namespace WotBlitzStatician.Model.Dto
{
	public class AchievementDto
	{
		public string AchievementId { get; set; }
		public string Section { get; set; }
		public string SectionName { get; set; }
		public long Order { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Count { get; set; }
		public string Image { get; set; }
		public string LocalImage => Image.MakeImagePathLocal();

	}
}