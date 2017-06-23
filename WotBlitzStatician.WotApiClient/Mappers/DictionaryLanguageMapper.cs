namespace WotBlitzStatician.WotApiClient.Mappers
{
	using System.Collections.Generic;
	using AutoMapper;
	using WotBlitzStatician.Model;

	internal class DictionaryLanguageMapper : IMapper<Dictionary<string, string>, List<DictionaryLanguage>>
	{
		public DictionaryLanguageMapper()
		{
			Mapper.Initialize(m => m.CreateMap<Dictionary<string, string>, List<DictionaryLanguage>>());
			// ToDo: read the documentation
		}

		public List<DictionaryLanguage> Map(Dictionary<string, string> source)
		{
			return Mapper.Map<Dictionary<string, string>, List<DictionaryLanguage>>(source);
		}

		public List<DictionaryLanguage> Map(Dictionary<string, string> source, List<DictionaryLanguage> destination)
		{
			return Mapper.Map(source, destination);
		}
	}
}