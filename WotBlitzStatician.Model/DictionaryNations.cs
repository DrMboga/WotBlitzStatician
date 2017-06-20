namespace WotBlitzStatician.Model
{
	using System.ComponentModel.DataAnnotations;

	public class DictionaryNations
    {
		[Key]
		public string NationId { get; set; }

		public string NationName { get; set; }

	}
}
