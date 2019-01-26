using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WotBlitzStatician.Logic.Test")]
namespace WotBlitzStatician.WotApiClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using WotBlitzStatician.Model;
    using WotBlitzStatician.Model.MapperLogic;
    using WotBlitzStatician.WotApiClient.InternalModel;
    using WotBlitzStatician.WotApiClient.RequestStringBuilder;


    public class WargamingApiClient : IWargamingApiClient
    {
        private readonly IRequestBuilder _requestBuilder;
        private readonly IMapperHelper _mapper;
        private readonly WebApiClient _webApiClient;

        public WargamingApiClient(
            IRequestBuilder requestBuilder,
            IMapperHelper mapper,
            WebApiClient webApiClient)
        {
            _requestBuilder = requestBuilder;
            _mapper = mapper;
            _webApiClient = webApiClient;
        }

        public async Task<List<VehicleEncyclopedia>> GetWotEncyclopediaVehiclesAsync()
        {
            var tankopedia = await _webApiClient.GetResponse<Dictionary<string, WotEncyclopediaVehiclesResponse>>(
                _requestBuilder.BaseAddress,
                _requestBuilder.BuildRequestUrl(RequestType.EncyclopediaVehicles,
                new RequestParameter
                {
                    ParameterType = ParameterType.Fields,
                    ParameterValue = "tank_id,name,tier,nation,type,description,is_premium,cost,images,next_tanks"
                }));
            var allVehicles = tankopedia.Values.ToList();
            allVehicles.AddMarkI();
            allVehicles.AddHetzerKame();

            return _mapper.Map<List<WotEncyclopediaVehiclesResponse>, List<VehicleEncyclopedia>>(allVehicles);
        }

        public async Task<List<AccountInfo>> FindAccountAsync(string nickName)
        {
            var accountListResponse = await _webApiClient.GetResponse<List<WotAccountListResponse>>(
                _requestBuilder.BaseAddress,
                _requestBuilder.BuildRequestUrl(RequestType.AccountList, new RequestParameter { ParameterType = ParameterType.Search, ParameterValue = nickName }));

            return _mapper.Map<List<WotAccountListResponse>, List<AccountInfo>>(accountListResponse);
        }

        public async Task<AccountInfo> GetAccountInfoAllStatisticsAsync(long accountId, string accessToken)
        {
            var accountInfo = await _webApiClient.GetResponse<Dictionary<string, WotAccountInfoResponse>>(
                _requestBuilder.BaseAddress,
                _requestBuilder.BuildRequestUrl(
                    RequestType.AccountInfo,
                    new RequestParameter { ParameterType = ParameterType.AccountId, ParameterValue = accountId.ToString() },
                    new RequestParameter { ParameterType = ParameterType.AccesToken, ParameterValue = accessToken },
                    new RequestParameter
                    {
                        ParameterType = ParameterType.Fields,
                        ParameterValue = "-private"
                    }));

            var accountInfoResponse = accountInfo[accountId.ToString()];

            var accountInfoMapped = _mapper.Map<WotAccountInfoResponse, AccountInfo>(accountInfoResponse);

            var statistics = _mapper.Map<WotAccountInfoResponse, AccountInfoStatistics>(accountInfoResponse);

            accountInfoMapped.AccountInfoStatistics = new List<AccountInfoStatistics> { statistics };

            return accountInfoMapped;
        }

        public async Task<List<AccountTankStatistics>> GetTanksStatisticsAsync(long accountId, string accessToken)
        {
            var tanksStat = await _webApiClient.GetResponse<Dictionary<string, List<WotAccountTanksStatResponse>>>(
                _requestBuilder.BaseAddress,
                _requestBuilder.BuildRequestUrl(
                    RequestType.TanksStat,
                    new RequestParameter { ParameterType = ParameterType.AccountId, ParameterValue = accountId.ToString() },
                    new RequestParameter { ParameterType = ParameterType.AccesToken, ParameterValue = accessToken }));

            return _mapper.Map<List<WotAccountTanksStatResponse>, List<AccountTankStatistics>>(tanksStat[accountId.ToString()]);
        }

        public async Task<PlayerPrivateInfoDto> GetAccountPrivateInfo(long accountId, string accessToken)
        {
            var accountInfo = await _webApiClient.GetResponse<Dictionary<string, WotAccountInfoResponse>>(
                _requestBuilder.BaseAddress,
                _requestBuilder.BuildRequestUrl(
                    RequestType.AccountInfo,
                    new RequestParameter { ParameterType = ParameterType.AccountId, ParameterValue = accountId.ToString() },
                    new RequestParameter { ParameterType = ParameterType.AccesToken, ParameterValue = accessToken },
                    new RequestParameter
                    {
                        ParameterType = ParameterType.Extra,
                        ParameterValue = "private.grouped_contacts"
                    },
                    new RequestParameter
                    {
                        ParameterType = ParameterType.Fields,
                        ParameterValue = "private"
                    }
                )
            );
            var accountInfoResponse = accountInfo[accountId.ToString()];
            return _mapper.Map<WotAccountInfoPrivate, PlayerPrivateInfoDto>(accountInfoResponse.Private);
        }

        public async Task<(
            List<DictionaryLanguage>,
            List<DictionaryNations>,
            List<DictionaryVehicleType>,
            List<AchievementSection>,
            List<DictionaryClanRole>)> GetStaticDictionariesAsync()
        {
            var encyclopedia = await _webApiClient.GetResponse<WotEncyclopediaInfoResponse>(
                _requestBuilder.BaseAddress,
                _requestBuilder.BuildRequestUrl(RequestType.EncyclopediaInfo));

            var languages = _mapper.Map<Dictionary<string, string>, List<DictionaryLanguage>>(encyclopedia.Languages);
            var nations = _mapper.Map<Dictionary<string, string>, List<DictionaryNations>>(encyclopedia.VehicleNations);
            var vehicleTypes = _mapper.Map<Dictionary<string, string>, List<DictionaryVehicleType>>(encyclopedia.VehicleTypes);
            var AchievementSections = _mapper.Map<Dictionary<string, WotEncyclopediaInfoAchievement_section>,
                List<AchievementSection>>(encyclopedia.AchievementSections);

            var clanGlossaryResponse = await _webApiClient.GetResponse<WotClanGlossaryResponse>(
                _requestBuilder.BaseAddress,
                _requestBuilder.BuildRequestUrl(RequestType.ClanGlossary));
            var clanGlossary = _mapper.Map<Dictionary<string, string>, List<DictionaryClanRole>>(clanGlossaryResponse.ClanRoles);

            return (languages, nations, vehicleTypes, AchievementSections, clanGlossary);
        }

        public async Task<AccountClanInfo> GetAccountClanInfoAsync(long accountId)
        {
            var playerAccountInfoResponse = await _webApiClient.GetResponse<Dictionary<string, WotClansAccountinfoResponse>>(
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

            var clanInfoResponse = await _webApiClient.GetResponse<Dictionary<string, WotClanInfoResponse>>(
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
            var acievementsresponse = await _webApiClient.GetResponse<Dictionary<string, WotEncyclopediaAchievementsResponse>>(
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
                        achievement.Options.Add(option);
                    }
                }
                achievements.Add(achievement);
            }

            return achievements;
        }

        public async Task<List<AccountInfoAchievement>> GetAccountAchievementsAsync(long accountId)
        {
            var accountAchievementsResponse = await _webApiClient.GetResponse<Dictionary<string, WotAccountAchievementResponse>>(
                _requestBuilder.BaseAddress,
                _requestBuilder.BuildRequestUrl(
                    RequestType.AccountAchievements,
                    new RequestParameter { ParameterType = ParameterType.AccountId, ParameterValue = accountId.ToString() }));

            var accountAchievements = accountAchievementsResponse[accountId.ToString()];

            var accountAchievementsInfo = accountAchievements
                .Achievements.Select(achievement => new AccountInfoAchievement
                {
                    AccountId = accountId,
                    AchievementId = achievement.Key,
                    Count = Convert.ToInt32(achievement.Value)
                }).ToList();

            accountAchievementsInfo.AddRange(accountAchievements
                    .MaxSeries.Select(achievement => new AccountInfoAchievement
                    {
                        AccountId = accountId,
                        AchievementId = achievement.Key,
                        Count = Convert.ToInt32(achievement.Value),
                        IsMaxSeries = true
                    }).ToList()
            );

            return accountAchievementsInfo;
        }

        public async Task<List<AccountInfoTankAchievement>> GetAccountTankAchievementsAsync(long accountId, string accessToken, List<int> tankIds = null)
        {
            var accountTankAchievements = new List<AccountInfoTankAchievement>();

            if (tankIds == null || tankIds.Count <= 100)
            {
                await FillAchievements(accountId, accountTankAchievements, accessToken, tankIds);
            }
            else
            {
                for (int i = 0; i < tankIds.Count; i += 100)
                {
                    int begin = i;
                    int count = i + 100 < tankIds.Count ? 100 : tankIds.Count - i;
                    await FillAchievements(accountId, accountTankAchievements, accessToken, tankIds.GetRange(begin, count));
                }
            }
            return accountTankAchievements;
        }

        private async Task FillAchievements(long accountId, List<AccountInfoTankAchievement> accountTankAchievements, string accessToken, List<int> tankIds = null)
        {
            var accountTankAchievementsResponse = await _webApiClient.GetResponse<Dictionary<string, WotAccountTankAchievementResponse[]>>(
                _requestBuilder.BaseAddress,
                _requestBuilder.BuildRequestUrl(
                    RequestType.TanksAcievements,
                    new RequestParameter { ParameterType = ParameterType.AccountId, ParameterValue = accountId.ToString() },
                    new RequestParameter { ParameterType = ParameterType.AccesToken, ParameterValue = accessToken },
                    new RequestParameter
                    {
                        ParameterType = ParameterType.TankId,
                        ParameterValue = tankIds == null
                        ? string.Empty
                        : string.Join(",", tankIds)
                    }));

            var accountTanks = accountTankAchievementsResponse[accountId.ToString()];

            foreach (var tank in accountTanks)
            {
                accountTankAchievements.AddRange(
                    tank.Achievements.Select(achievement => new AccountInfoTankAchievement
                    {
                        TankId = tank.TankId.Value,
                        AccountId = accountId,
                        AchievementId = achievement.Key,
                        Count = Convert.ToInt32(achievement.Value)
                    })
                        .ToList()
                );
                accountTankAchievements.AddRange(
                    tank.MaxSeries.Select(achievement => new AccountInfoTankAchievement
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
        }

        public async Task<AccountInfo> ProlongateAccount(string accessToken)
        {
            var accountInfo = await _webApiClient.GetResponse<WotAuthProlongateResponse>(
                _requestBuilder.WotBaseAddress,
                _requestBuilder.BuildRequestUrl(
                    RequestType.Prolongate,
                    new RequestParameter { ParameterType = ParameterType.AccesToken, ParameterValue = accessToken }
                    ),
                true);

            return _mapper.Map<WotAuthProlongateResponse, AccountInfo>(accountInfo);
        }

        public void DownloadFile(string uri, string fileName)
        {
            _webApiClient.DownloadFile(uri, fileName);
        }
    }
}
