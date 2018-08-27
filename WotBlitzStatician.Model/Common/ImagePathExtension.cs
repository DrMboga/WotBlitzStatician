namespace WotBlitzStatician.Model.Common
{
	public static class ImagePathExtension
    {
		public static string MakeImagePathLocal(this string wgApiImagePath)
		{
			return wgApiImagePath?.Replace("http://glossary-ru-static.gcdn.co/icons/wotb/current", "img");
		}
    }
}
