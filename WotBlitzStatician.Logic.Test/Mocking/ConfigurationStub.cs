using WotBlitzStatician.WotApiClient;

namespace WotBlitzStatician.Logic.Test.Mocking
{
    public class ConfigurationStub : IWgApiConfiguration
    {
        public string ApplicationId { get; set; } = "TestAppId";
        public string BaseAddress { get; set; } = "http://TestBaseAddress";
        public string WotBaseAddress { get; set; } = "http://TestWotBaseAddress";
        public string Language { get; set; } = "TestLang";
        public int DictionariesUpdateFrequencyInDays { get; set; }
        public IProxySettings ProxySettings { get; set; } = new ConfigurationProxyStub();
    }
}