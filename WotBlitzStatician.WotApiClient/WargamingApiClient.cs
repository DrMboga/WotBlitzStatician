namespace WotBlitzStatician.WotApiClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WotBlitzStatician.Model;
    using WotBlitzStatician.WotApiClient.DTO;
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
		
		public async Task<WotEncyclopediaInfo> GetStaticDictionariesAsync()
		{
			var webClient = new WebApiClient();
			var encyclopedia = await webClient.GetResponse<WotEncyclopediaInfoResponse>(
				_requestBuilder.BaseAddress,
				_requestBuilder.BuildRequestUrl(RequestType.EncyclopediaInfo));

			var responseInfo = new WotEncyclopediaInfo();
			if (encyclopedia?.Languages == null) return responseInfo;

			var languageMapper = new DictionaryLanguageMapper();
            responseInfo.DictionaryLanguages = new List<DictionaryLanguage>();
            foreach (var lan in encyclopedia.Languages)
            {
                responseInfo.DictionaryLanguages.Add(languageMapper.Map(lan));
            }

            var nationMapper = new DictionaryNationMapper();
            responseInfo.DictionaryNationses = new List<DictionaryNations>();
            foreach (var nation in encyclopedia.VehicleNations)
            {
                responseInfo.DictionaryNationses.Add(nationMapper.Map(nation));
            }

            var vehicleTypesMapper = new DictionaryVehicleTypeMapper();
            responseInfo.DictionaryVehicleTypes = new List<DictionaryVehicleType>();
            foreach (var vt in encyclopedia.VehicleTypes)
            {
                responseInfo.DictionaryVehicleTypes.Add(vehicleTypesMapper.Map(vt));
            }

            return responseInfo;
		}

        public async Task<AccountClanInfo> GetAccountClanInfoAsync(long accountId)
        {
            var webClient = new WebApiClient();
            var playerAccountInfoResponse = await webClient.GetResponse<Dictionary<string, WotClansAccountinfoResponse>>(
	            _requestBuilder.BaseAddress,
	            _requestBuilder.BuildRequestUrl(
	                    RequestType.ClanAccountInfo,
	                    new RequestParameter { ParameterType = ParameterType.AccountId, ParameterValue = accountId.ToString() }));

            var playerAccountInfo = playerAccountInfoResponse[accountId.ToString()];
            if (playerAccountInfo == null)
                return null;

            var clanAccountInfoMapper = new ClanAccountInfoMapper();
            var accountClanInfo = clanAccountInfoMapper.Map(playerAccountInfo);

            if (!playerAccountInfo.ClanId.HasValue)
                return accountClanInfo;

            string clanId = playerAccountInfo.ClanId.Value.ToString();

            var clanInfoResponse = await webClient.GetResponse<Dictionary<string, WotClanInfoResponse>>(
			    _requestBuilder.BaseAddress,
				_requestBuilder.BuildRequestUrl(
                    RequestType.ClanInfo,
                    new RequestParameter { ParameterType = ParameterType.ClanId, ParameterValue = clanId }));

            var clanInfo = clanInfoResponse[clanId];
            var clanInfoMapper = new ClanInfoResponseMapper();
            clanInfoMapper.Map(clanInfo, accountClanInfo);

            return accountClanInfo;
        }
	
	public async Task<List<Achievement>> GetAchievementsDictionaryAsync()
	{
		var webClient = new WebApiClient();
		var acievementsresponse = await webClient.GetResponse<Dictionary<string, WotEncyclopediaAchievementsResponse>>(
			_requestBuilder.BaseAddress,
			_requestBuilder.BuildRequestUrl(RequestType.EncyclopediaAchievements));

		var achievements = new List<Achievement>();

		foreach (var achievementsresponseValue in acievementsresponse.Values)
		{
			var achievementMapper = new DictionaryAchievementsMapper();
			var achievement = achievementMapper.Map(achievementsresponseValue);
			if (achievementsresponseValue.Options != null && achievementsresponseValue.Options.Length > 0)
			{
				achievement.Options = new List<AchievementOption>();
				var achievementOptionMapper = new DictionaryAchievementOptionMapper();
				foreach (var wotEncyclopediaAchievementsOption in achievementsresponseValue.Options)
				{
					var option = achievementOptionMapper.Map(wotEncyclopediaAchievementsOption);
					option.AchievementId = achievement.AchievementId;
					achievement.Options.Add(option);
				}
			}
			achievements.Add(achievement);
		}

		return achievements;
	}
    }
}
