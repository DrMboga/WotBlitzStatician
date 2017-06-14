namespace WotBlitzStatician.WotApiClient
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using WotBlitzStaticitian.Model;

	public class WargamingApiClient : IWargamingApiClient
	{
		// ToDo: Get AutoMapper, create internal ctructures and mappers

		private readonly IWgApiConfiguration _configuration;

		public WargamingApiClient(IWgApiConfiguration configuration)
		{
			_configuration = configuration;
		}

		public Task<List<VehicleEncyclopedia>> GetWontEncyclopediaVehicles()
		{
//			var webClient = new WebApiClient();
//
//			var tankopedia = webClient.GetResponse<Dictionary<string, WotEncyclopediaVehiclesResponse>>(
//				_configuration.BaseAddress,
//				string.Format(_configuration.VehiclesEncyclopediaRequestAddressTemplate, _configuration.ApplicationId)
//			).Result;
//			return tankopedia.Values.ToList();

			throw new System.NotImplementedException();
		}

		public Task<AccountInfo> FindAccount(string nickName)
		{
			throw new System.NotImplementedException();
		}

		public Task<AccountInfo> GetAccountInfoAllStatistics(long accountId)
		{
			throw new System.NotImplementedException();
		}

		public Task<List<AccountTankStatistics>> GetTanksStatisticks(long accountId)
		{
			throw new System.NotImplementedException();
		}
	}
}