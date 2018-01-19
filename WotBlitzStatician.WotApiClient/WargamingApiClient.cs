namespace WotBlitzStatician.WotApiClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WotBlitzStatician.Model;
    using WotBlitzStatician.Model.MapperLogic;
    using WotBlitzStatician.WotApiClient.DTO;
    using WotBlitzStatician.WotApiClient.InternalModel;
    using WotBlitzStatician.WotApiClient.RequestStringBuilder;


    public class WargamingApiClient : IWargamingApiClient
	{
		private readonly IRequestBuilder _requestBuilder;
		private readonly IProxySettings _proxySettings;
		private readonly IMapperHelper _mapper;

		internal WargamingApiClient(IRequestBuilder requestBuilder, IProxySettings proxySettings, IMapperHelper mapper)
		{
			_requestBuilder = requestBuilder;
			_proxySettings = proxySettings;
			_mapper = mapper;
		}

		public async Task<List<VehicleEncyclopedia>> GetWotEncyclopediaVehiclesAsync()
		{
			var webClient = new WebApiClient(_proxySettings);

			var tankopedia = await webClient.GetResponse<Dictionary<string, WotEncyclopediaVehiclesResponse>>(
				_requestBuilder.BaseAddress,
				_requestBuilder.BuildRequestUrl(RequestType.EncyclopediaVehicles));
			var allVehicles = tankopedia.Values.ToList();
			allVehicles.AddMarkI();
			allVehicles.AddHetzerKame();

			return _mapper.Map<List<WotEncyclopediaVehiclesResponse>, List<VehicleEncyclopedia>>(allVehicles);
		}

		public async Task<List<AccountInfo>> FindAccountAsync(string nickName)
		{
			var webClient = new WebApiClient(_proxySettings);
			var accountListResponse = await webClient.GetResponse<List<WotAccountListResponse>>(
				_requestBuilder.BaseAddress,
				_requestBuilder.BuildRequestUrl(RequestType.AccountList, new RequestParameter{ParameterType = ParameterType.Search, ParameterValue = nickName }));

			return _mapper.Map<List<WotAccountListResponse>, List<AccountInfo>>(accountListResponse);
		}

		public async Task<AccountInfo> GetAccountInfoAllStatisticsAsync(long accountId, string accessToken)
		{
			var webClient = new WebApiClient(_proxySettings);

			var accountInfo = await webClient.GetResponse<Dictionary<string, WotAccountInfoResponse>>(
				_requestBuilder.BaseAddress,
				_requestBuilder.BuildRequestUrl(
					RequestType.AccountInfo, 
					new RequestParameter {ParameterType = ParameterType.AccountId, ParameterValue = accountId.ToString() },
					new RequestParameter { ParameterType = ParameterType.AccesToken, ParameterValue = accessToken }));

			var accountInfoResponse = accountInfo[accountId.ToString()];

			// ToDo: AccountInfo.FragsList, stat.Battlelifetime (private), Private, Friends
			var accountInfoMapped = _mapper.Map<WotAccountInfoResponse, AccountInfo>(accountInfoResponse);

			accountInfoMapped.AccountInfoStatistics = _mapper.Map<WotAccountInfoResponse, AccountInfoStatistics>(accountInfoResponse);
			// ToDo: Map PrivateInfo

			return accountInfoMapped;
		}

		public async Task<List<AccountTankStatistics>> GetTanksStatisticsAsync(long accountId)
		{
			var webClient = new WebApiClient(_proxySettings);

			var tanksStat = await webClient.GetResponse<Dictionary<string, List<WotAccountTanksStatResponse>>>(
				_requestBuilder.BaseAddress,
				_requestBuilder.BuildRequestUrl(RequestType.TanksStat, new RequestParameter { ParameterType = ParameterType.AccountId, ParameterValue = accountId.ToString() }));

			return _mapper.Map<List<WotAccountTanksStatResponse>, List<AccountTankStatistics>>(tanksStat[accountId.ToString()]);
		}
		
		public async Task<WotEncyclopediaInfo> GetStaticDictionariesAsync()
		{
			var webClient = new WebApiClient(_proxySettings);
			var encyclopedia = await webClient.GetResponse<WotEncyclopediaInfoResponse>(
				_requestBuilder.BaseAddress,
				_requestBuilder.BuildRequestUrl(RequestType.EncyclopediaInfo));

			var responseInfo = new WotEncyclopediaInfo();
			if (encyclopedia?.Languages == null) return responseInfo;

			responseInfo.DictionaryLanguages = _mapper.Map<Dictionary<string, string>, List<DictionaryLanguage>>(encyclopedia.Languages);
			responseInfo.DictionaryNationses = _mapper.Map<Dictionary<string, string>, List<DictionaryNations>>(encyclopedia.VehicleNations);
			responseInfo.DictionaryVehicleTypes = _mapper.Map<Dictionary<string, string>, List<DictionaryVehicleType>>(encyclopedia.VehicleTypes);

            return responseInfo;
		}

        public async Task<AccountClanInfo> GetAccountClanInfoAsync(long accountId)
        {
            var webClient = new WebApiClient(_proxySettings);
            var playerAccountInfoResponse = await webClient.GetResponse<Dictionary<string, WotClansAccountinfoResponse>>(
	            _requestBuilder.BaseAddress,
	            _requestBuilder.BuildRequestUrl(
	                    RequestType.ClanAccountInfo,
	                    new RequestParameter { ParameterType = ParameterType.AccountId, ParameterValue = accountId.ToString() }));

            var playerAccountInfo = playerAccountInfoResponse[accountId.ToString()];
            if (playerAccountInfo == null)
                return null;

            var accountClanInfo = _mapper.Map<WotClansAccountinfoResponse, AccountClanInfo>(playerAccountInfo);

            if (!playerAccountInfo.ClanId.HasValue)
                return accountClanInfo;

            string clanId = playerAccountInfo.ClanId.Value.ToString();

            var clanInfoResponse = await webClient.GetResponse<Dictionary<string, WotClanInfoResponse>>(
			    _requestBuilder.BaseAddress,
				_requestBuilder.BuildRequestUrl(
                    RequestType.ClanInfo,
                    new RequestParameter { ParameterType = ParameterType.ClanId, ParameterValue = clanId }));

            var clanInfo = clanInfoResponse[clanId];
            _mapper.Map(clanInfo, accountClanInfo);

            return accountClanInfo;
        }
	
	public async Task<List<Achievement>> GetAchievementsDictionaryAsync()
	{
		var webClient = new WebApiClient(_proxySettings);
		var acievementsresponse = await webClient.GetResponse<Dictionary<string, WotEncyclopediaAchievementsResponse>>(
			_requestBuilder.BaseAddress,
			_requestBuilder.BuildRequestUrl(RequestType.EncyclopediaAchievements));

		var achievements = new List<Achievement>();

		foreach (var achievementsresponseValue in acievementsresponse.Values)
		{
			var achievement = _mapper.Map<WotEncyclopediaAchievementsResponse, Achievement>(achievementsresponseValue);
			if (achievementsresponseValue.Options != null && achievementsresponseValue.Options.Length > 0)
			{
				achievement.Options = new List<AchievementOption>();
				foreach (var wotEncyclopediaAchievementsOption in achievementsresponseValue.Options)
				{
					var option = _mapper.Map<WotEncyclopediaAchievementsOptions, AchievementOption>(wotEncyclopediaAchievementsOption);
					option.AchievementId = achievement.AchievementId;
					achievement.Options.Add(option);
				}
			}
			achievements.Add(achievement);
		}

		return achievements;
	}
	
		public async Task<List<AccountInfoAchievment>> GetAccountAchievementsAsync(long accountId)
		{
			var webClient = new WebApiClient(_proxySettings);
			var accountAchievementsResponse = await webClient.GetResponse <Dictionary<string, WotAccountAchievementResponse>>(
				_requestBuilder.BaseAddress,
				_requestBuilder.BuildRequestUrl(
					RequestType.AccountAchievements,
					new RequestParameter { ParameterType = ParameterType.AccountId, ParameterValue = accountId.ToString() }));

			var accountAchievements = accountAchievementsResponse[accountId.ToString()];

			var accountAchievementsInfo = accountAchievements
				.Achievements.Select(achievement => new AccountInfoAchievment
				{
					AccountId = accountId,
					AchievementId = achievement.Key,
					Count = Convert.ToInt32(achievement.Value)
				}).ToList();

			accountAchievementsInfo.AddRange(accountAchievements
					.MaxSeries.Select(achievement => new AccountInfoAchievment
					{
						AccountId = accountId,
						AchievementId = achievement.Key,
						Count = Convert.ToInt32(achievement.Value),
						IsMaxSeries = true
					}).ToList()
			);

			return accountAchievementsInfo;
		}

		public async Task<List<AccountInfoTankAchievment>> GetAccountTankAchievementsAsync(long accountId)
		{
			var webClient = new WebApiClient(_proxySettings);
			var accountTankAchievementsResponse = await webClient.GetResponse<Dictionary<string, WotAccountTankAchievementResponse[]>>(
				_requestBuilder.BaseAddress,
				_requestBuilder.BuildRequestUrl(
					RequestType.TanksAcievements,
					new RequestParameter { ParameterType = ParameterType.AccountId, ParameterValue = accountId.ToString() }));

			var accountTanks = accountTankAchievementsResponse[accountId.ToString()];

			var accountTankAchievements = new List<AccountInfoTankAchievment>();

			foreach (var tank in accountTanks)
			{
				accountTankAchievements.AddRange(
					tank.Achievements.Select(achievement => new AccountInfoTankAchievment
						{
							TankId = tank.TankId.Value,
							AccountId = accountId,
							AchievementId = achievement.Key,
							Count = Convert.ToInt32(achievement.Value)
						})
						.ToList()
				);
				accountTankAchievements.AddRange(
					tank.MaxSeries.Select(achievement => new AccountInfoTankAchievment
						{
							TankId = tank.TankId.Value,
							AccountId = accountId,
							AchievementId = achievement.Key,
							Count = Convert.ToInt32(achievement.Value),
							IsMaxSeries = true
						})
						.ToList()
				);
			}

			return accountTankAchievements;
		}	
	
    }
}
