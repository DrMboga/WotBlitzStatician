namespace WotBlitzStatician.WotApiClient
{
	public interface IWgApiConfiguration
	{
		/// <summary>
		/// WG Application id
		/// </summary>
		string ApplicationId { get; set; }

		string BaseAddress { get; set; }

		string WotBaseAddress { get; set; }

		string Language { get; set; }
		
		int DictionariesUpdateFrequencyInDays { get; set; }

		IProxySettings ProxySettings { get; set; }

		int HttpTimeoutInMinutes { get; set; }	
	}
}
