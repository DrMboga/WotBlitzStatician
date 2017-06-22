namespace WotBlitzStatician.WotApiClient
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using WotBlitzStatician.Model;
	using WotBlitzStatician.WotApiClient.InternalModel;
	using WotBlitzStatician.WotApiClient.Mappers;
	using WotBlitzStatician.WotApiClient.RequestStringBuilder;
	

	public class WargamingApiClient : IWargamingApiClient
	{
		private readonly IRequestBuilder _requestBuilder;

		internal WargamingApiClient(IRequestBuilder requestBuilder)
		{
			_requestBuilder = requestBuilder;
		}

		public async Task<List<VehicleEncyclopedia>> GetWotEncyclopediaVehiclesAsync()
		{
			var webClient = new WebApiClient();

			var tankopedia = await webClient.GetResponse<Dictionary<string, WotEncyclopediaVehiclesResponse>>(
				_requestBuilder.BaseAddress,
				_requestBuilder.BuildRequestUrl(RequestType.EncyclopediaVehicles));
			var allVehicles = tankopedia.Values.ToList();
			allVehicles.AddMarkI();
			allVehicles.AddHetzerKame();

			var mapper = new TankopediaMapper();
			return allVehicles.Select(t => mapper.Map(t)).ToList();
		}

		public async Task<List<AccountInfo>> FindAccountAsync(string nickName)
		{
			var webClient = new WebApiClient();
			var accountListResponse = await webClient.GetResponse<List<WotAccountListResponse>>(
				_requestBuilder.BaseAddress,
				_requestBuilder.BuildRequestUrl(RequestType.AccountList, new RequestParameter{ParameterType = ParameterType.Search, ParameterValue = nickName }));

			var accountFindResponseMapper = new AccounutFindResponseMapper();
			return accountListResponse.Select(a => accountFindResponseMapper.Map(a)).ToList();
		}

		public async Task<AccountInfo> GetAccountInfoAllStatisticsAsync(long accountId)
		{
			var webClient = new WebApiClient();

			var accountInfo = await webClient.GetResponse<Dictionary<string, WotAccountInfoResponse>>(
				_requestBuilder.BaseAddress,
				_requestBuilder.BuildRequestUrl(RequestType.AccountInfo, new RequestParameter { ParameterType = ParameterType.AccountId, ParameterValue = accountId.ToString() }));

			var accountInfoResponse = accountInfo[accountId.ToString()];
			var accountInfoMapper = new AccountInfoMapper();

			var accountInfoMapped = accountInfoMapper.Map(accountInfoResponse);

			var accountInfoStatMapper = new AccountInfoStatisticsMapper();
			accountInfoMapped.AccountInfoStatistics = accountInfoStatMapper.Map(accountInfoResponse);
			// ToDo: Map PrivateInfo

			return accountInfoMapped;
		}

		public async Task<List<AccountTankStatistics>> GetTanksStatisticsAsync(long accountId)
		{
			var webClient = new WebApiClient();

			var tanksStat = await webClient.GetResponse<Dictionary<string, List<WotAccountTanksStatResponse>>>(
				_requestBuilder.BaseAddress,
				_requestBuilder.BuildRequestUrl(RequestType.TanksStat, new RequestParameter { ParameterType = ParameterType.AccountId, ParameterValue = accountId.ToString() }));

			var mapper = new TanksStatMapper();
			return tanksStat[accountId.ToString()].Select(s => mapper.Map(s)).ToList();
		}
	}
}
