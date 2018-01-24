using System.Collections.Generic;

namespace WotBlitzStatician.Model
{
    public class AchievementSection
    {
		public string Section { get; set; }
		public string SectionName { get; set; }
		public int Order { get; set; }
		public List<Achievement> Achievements { get; set; }
	}
}
