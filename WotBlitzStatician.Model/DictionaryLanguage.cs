namespace WotBlitzStatician.Model
{
    using System.ComponentModel.DataAnnotations;

    public class DictionaryLanguage
    {
        [Key]
        public string LanguageId { get; set; }

        public string LanguageName { get; set; }
        
        public DateTime LastUpdated { get; set; }
    }
}
