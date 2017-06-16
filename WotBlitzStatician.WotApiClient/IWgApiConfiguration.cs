namespace WotBlitzStatician.WotApiClient
{
	public interface IWgApiConfiguration
	{
		/// <summary>
		/// WG Application id
		/// </summary>
		string ApplicationId { get; set; }

		string BaseAddress { get; set; }
		
		string AccountListFinderAddressTemplate { get; set; }

		string AccountStatRequestAddressTemplate { get; set; }

		string AccountTanksStatisticRequestAddressTemplate { get; set; }

		string VehiclesEncyclopediaRequestAddressTemplate { get; set; }
	}
}
