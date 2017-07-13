namespace WotBlitzStatician.Logic.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using log4net;
	using WotBlitzStatician.Data;
	using WotBlitzStatician.Model;
	using Xunit;

	public class TestDatabase
    {
	    private const string ConnectionString = @"Data Source=..\..\..\BlitzStatician.db";
		private static readonly ILog _log = LogManager.GetLogger(typeof(WargamingClientTest));

		public TestDatabase()
        {
            Log4NetHelper.ConfigureLog4Net();
        }

		[Fact]
	    public void TestLanguageDictionaryMerge()
		{
			List<DictionaryLanguage> languagesFormDb;

			using (var context = new BlitzStaticianDataContext(ConnectionString))
		    {
			    languagesFormDb = context.DictionaryLanguage.ToList();
		    }

			_log.Debug($"Got {languagesFormDb.Count} languages from DB first time");

			using (var context = new BlitzStaticianDataContext(ConnectionString))
			{
				context.Merge(context.DictionaryLanguage, GetLanguagesStub());
				context.SaveChanges();
			}

			using (var context = new BlitzStaticianDataContext(ConnectionString))
			{
				languagesFormDb = context.DictionaryLanguage.ToList();
			}

			_log.Debug($"Got {languagesFormDb.Count} languages from DB second time");
		}

		private List<DictionaryLanguage> GetLanguagesStub()
	    {
		    var dictionaries = new List<DictionaryLanguage>
		    {
			    new DictionaryLanguage {LanguageId = "cs", LanguageName = "Čeština", LastUpdated = DateTime.Now},
			    new DictionaryLanguage {LanguageId = "de", LanguageName = "Deutsch", LastUpdated = DateTime.Now},
			    new DictionaryLanguage {LanguageId = "en", LanguageName = "English", LastUpdated = DateTime.Now},
			    new DictionaryLanguage {LanguageId = "es", LanguageName = "Español", LastUpdated = DateTime.Now},
			    new DictionaryLanguage {LanguageId = "fr", LanguageName = "Français", LastUpdated = DateTime.Now},
			    new DictionaryLanguage {LanguageId = "ko", LanguageName = "한국어", LastUpdated = DateTime.Now},
			    new DictionaryLanguage {LanguageId = "pl", LanguageName = "Polski", LastUpdated = DateTime.Now},
			    new DictionaryLanguage {LanguageId = "ru", LanguageName = "Русский", LastUpdated = DateTime.Now},
			    new DictionaryLanguage {LanguageId = "th", LanguageName = "ไทย", LastUpdated = DateTime.Now},
			    new DictionaryLanguage {LanguageId = "tr", LanguageName = "Türkçe", LastUpdated = DateTime.Now},
			    new DictionaryLanguage {LanguageId = "vi", LanguageName = "Tiếng Việt", LastUpdated = DateTime.Now},
			    new DictionaryLanguage {LanguageId = "zh-cn", LanguageName = "简体中文", LastUpdated = DateTime.Now},
			    new DictionaryLanguage {LanguageId = "zh-tw", LanguageName = "繁體中文", LastUpdated = DateTime.Now},
		    };

		    return dictionaries;
	    }
	}
}

