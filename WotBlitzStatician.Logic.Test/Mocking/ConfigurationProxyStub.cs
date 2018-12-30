using WotBlitzStatician.WotApiClient;

namespace WotBlitzStatician.Logic.Test.Mocking
{
    public class ConfigurationProxyStub : IProxySettings
    {
        public bool UseProxy { get; set; } = false;
        public string ProxyAddress { get; set; }
        public string Domain { get; set; }
        public string User { get; set; }
        public string PwdHash { get; set; }
    }
}