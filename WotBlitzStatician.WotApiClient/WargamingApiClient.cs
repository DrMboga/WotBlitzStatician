namespace WotBlitzStatician.WotApiClient
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using WotBlitzStatician.WotApiClient.InternalModel;
	using WotBlitzStatician.WotApiClient.Mappers;
	using WotBlitzStaticitian.Model;

	public class WargamingApiClient : IWargamingApiClient
	{
		private readonly IWgApiConfiguration _configuration;

		public WargamingApiClient(IWgApiConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task<List<VehicleEncyclopedia>> GetWotEncyclopediaVehiclesAsync()
		{
			var webClient = new WebApiClient();

			var tankopedia = await webClient.GetResponse<Dictionary<string, WotEncyclopediaVehiclesResponse>>(
				_configuration.BaseAddress,
				string.Format(_configuration.VehiclesEncyclopediaRequestAddressTemplate, _configuration.ApplicationId));
			var allVehicles = tankopedia.Values.ToList();
			allVehicles.AddMarkI();
			allVehicles.AddHetzerKame();

			var mapper = new TankopediaMapper();
			return allVehicles.Select(t => mapper.Map(t)).ToList();
		}

		public async Task<AccountInfo> FindAccountAsync(string nickName)
		{
			throw new System.NotImplementedException();
		}

		public async Task<AccountInfo> GetAccountInfoAllStatisticsAsync(string accountId)
		{
			var webClient = new WebApiClient();

			var accountInfo = await webClient.GetResponse<Dictionary<string, WotAccountInfoResponse>>(
					_configuration.BaseAddress,
					string.Format(_configuration.AccountStatRequestAddressTemplate, _configuration.ApplicationId, accountId));

			var accountInfoResponse = accountInfo[accountId];
			var accountInfoMapper = new AccountInfoMapper();

			var accountInfoMapped = accountInfoMapper.Map(accountInfoResponse);

			var accountInfoStatMapper = new AccountInfoStatisticsMapper();
			accountInfoMapped.AccountInfoStatistics = accountInfoStatMapper.Map(accountInfoResponse);
			// ToDo: Map PrivateInfo

			return accountInfoMapped;
		}

		public async Task<List<AccountTankStatistics>> GetTanksStatisticsAsync(string accountId)
		{
			var webClient = new WebApiClient();

			var tanksStat = await webClient.GetResponse<Dictionary<string, List<WotAccountTanksStatResponse>>>(
				_configuration.BaseAddress,
				string.Format(_configuration.AccountTanksStatisticRequestAddressTemplate, _configuration.ApplicationId, accountId));

			var mapper = new TanksStatMapper();
			return tanksStat[accountId].Select(s => mapper.Map(s)).ToList();
		}
	}
}
