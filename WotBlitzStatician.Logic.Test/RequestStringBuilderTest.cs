using System;
using WotBlitzStatician.Logic.Test.Mocking;
using WotBlitzStatician.WotApiClient;
using WotBlitzStatician.WotApiClient.RequestStringBuilder;
using Xunit;

namespace WotBlitzStatician.Logic.Test
{
    public class RequestStringBuilderTest
    {
        private readonly IWgApiConfiguration _configuration;
        private readonly IRequestBuilder _requestBuilder;

        public RequestStringBuilderTest()
        {
            _configuration = new ConfigurationStub();
            _requestBuilder = new RequestBuilder(_configuration);
        }

        [Fact]
        public void VehicleEncyclopediaQueryTest()
        {
            string expectedQuery =
            $"encyclopedia/vehicles/?application_id={_configuration.ApplicationId}&language={_configuration.Language}&fields=tank_id,name,tier,nation,type,description,is_premium,cost,images";
            string request = _requestBuilder.BuildRequestUrl(RequestType.EncyclopediaVehicles,
                new RequestParameter
                {
                    ParameterType = ParameterType.Fields,
                    ParameterValue = "tank_id,name,tier,nation,type,description,is_premium,cost,images"
                });
            Assert.Equal(expectedQuery, request);
        }

        [Fact]
        public void FindAccountQueryTest()
        {
            string testNickName = "WotAccountNick";
            string expectedQuery =
            $"account/list/?application_id={_configuration.ApplicationId}&language={_configuration.Language}&search={testNickName}";

            string request = _requestBuilder.BuildRequestUrl(RequestType.AccountList,
                new RequestParameter { ParameterType = ParameterType.Search, ParameterValue = testNickName });
            Assert.Equal(expectedQuery, request);
        }

        [Fact]
        public void GetAccountInfoQueryTest()
        {
            string accountId = "TestAccountId";
            string accessToken = "TestAccessToken";

            string expectedQuery =
            $"account/info/?application_id={_configuration.ApplicationId}&language={_configuration.Language}&account_id={accountId}&access_token={accessToken}&fields=-private";

            string request = _requestBuilder.BuildRequestUrl(
                    RequestType.AccountInfo,
                    new RequestParameter { ParameterType = ParameterType.AccountId, ParameterValue = accountId },
                    new RequestParameter { ParameterType = ParameterType.AccesToken, ParameterValue = accessToken },
                    new RequestParameter
                    {
                        ParameterType = ParameterType.Fields,
                        ParameterValue = "-private"
                    });
            Assert.Equal(expectedQuery, request);
        }

        [Fact]
        public void GetTanksStatistivsQueryTest()
        {
            string accountId = "TestAccountId";
            string accessToken = "TestAccessToken";

            string expectedQuery =
            $"tanks/stats/?application_id={_configuration.ApplicationId}&language={_configuration.Language}&account_id={accountId}&access_token={accessToken}";

            string request = _requestBuilder.BuildRequestUrl(
                RequestType.TanksStat,
                new RequestParameter { ParameterType = ParameterType.AccountId, ParameterValue = accountId.ToString() },
                new RequestParameter { ParameterType = ParameterType.AccesToken, ParameterValue = accessToken });
            Assert.Equal(expectedQuery, request);
        }

        [Fact]
        public void GetPrivateAccountInfoQueryTest()
        {
            string accountId = "TestAccountId";
            string accessToken = "TestAccessToken";

            string expectedQuery =
            $"account/info/?application_id={_configuration.ApplicationId}&language={_configuration.Language}&account_id={accountId}&access_token={accessToken}&extra=private.grouped_contacts&fields=private";

            string request = 
                _requestBuilder.BuildRequestUrl(
                    RequestType.AccountInfo,
                    new RequestParameter { ParameterType = ParameterType.AccountId, ParameterValue = accountId.ToString()},
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
                );
            Assert.Equal(expectedQuery, request);
        }



    }
}