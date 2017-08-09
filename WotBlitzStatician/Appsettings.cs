namespace WotBlitzStatician
{
	using WotBlitzStatician.WotApiClient;
	public class Appsettings : IWgApiConfiguration
	{
		public string ApplicationId { get; set; }
		public string BaseAddress { get; set; }
		public string Language { get; set; }
		public int DictionariesUpdateFrequencyInDays { get; set; }
		public IProxySettings ProxySettings { get; set; }
	}

	public class ProxySettings : IProxySettings
	{
		public bool UseProxy { get; set; }
		public string Domain { get; set; }
		public string User { get; set; }
		public string PwdHash { get; set; }
	}
}