namespace WotBlitzStatician.WotApiClient.DTO
{
	using System.Collections.Generic;
	using WotBlitzStatician.Model;

	public class WotEncyclopediaInfo
	{
		public List<DictionaryLanguage> DictionaryLanguages { get; set; }
		public List<DictionaryNations> DictionaryNationses { get; set; }
		public List<DictionaryVehicleType> DictionaryVehicleTypes { get; set; }
	}
}