namespace WotBlitzStatician.WotApiClient.RequestStringBuilder
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	internal class RequestBuilder : IRequestBuilder
	{
		private readonly Uri _baseUri;
		private readonly Uri _wotBaseUri;
		private readonly Dictionary<RequestType, string> _requestPaths = new Dictionary<RequestType, string>();
		private readonly Dictionary<ParameterType, string> _parameterNames = new Dictionary<ParameterType, string>();
		private readonly List<RequestParameter> _defaultParameters = new List<RequestParameter>();

		public RequestBuilder(IWgApiConfiguration configuration)
		{
			_baseUri = new Uri(configuration.BaseAddress);
			_wotBaseUri = new Uri(configuration.WotBaseAddress);
			FillRequestPathsDictionary();
			FillParameterNamesDictionary();
			_defaultParameters.Add(new RequestParameter{ParameterType = ParameterType.ApplicationId, ParameterValue = configuration.ApplicationId});
			_defaultParameters.Add(new RequestParameter{ParameterType = ParameterType.Language, ParameterValue = configuration.Language});
		}

		public string BaseAddress => _baseUri.ToString();
		public string WotBaseAddress => _wotBaseUri.ToString();

		public string BuildRequestUrl(RequestType requestType, params RequestParameter[] parameters)
		{
			var query = new StringBuilder();

			// Common parameters
			_defaultParameters
				.Where(p => AddDefaultParametersRule(requestType, p))
				.ToList()
				.ForEach(p => query.Append($"{_parameterNames[p.ParameterType]}={p.ParameterValue}&"));

			// Method parameters
			parameters?
				.Where(p => !string.IsNullOrWhiteSpace(p.ParameterValue))
				.ToList()
				.ForEach(p => query.Append($"{_parameterNames[p.ParameterType]}={p.ParameterValue}&"));
			// Removing last ampersand
			query.Remove(query.Length - 1, 1);

			return $"{_requestPaths[requestType]}?{query}";
		}

		private static bool AddDefaultParametersRule(RequestType requestType, RequestParameter requestParameter)
		{
			// Exclude Language parameter for Prolongate request type
			return ((requestType == RequestType.Prolongate || requestType == RequestType.Login) 
						&& requestParameter.ParameterType != ParameterType.Language)
			       || (requestType != RequestType.Prolongate && requestType != RequestType.Login);
		}

		private void FillParameterNamesDictionary()
		{
			_parameterNames[ParameterType.Language] = "language";
			_parameterNames[ParameterType.ApplicationId] = "application_id";
			_parameterNames[ParameterType.Search] = "search";
			_parameterNames[ParameterType.AccountId] = "account_id";
            _parameterNames[ParameterType.ClanId] = "clan_id";
            _parameterNames[ParameterType.AccesToken] = "access_token";
            _parameterNames[ParameterType.Extra] = "extra";
            _parameterNames[ParameterType.Fields] = "fields";
            _parameterNames[ParameterType.TankId] = "tank_id";
			_parameterNames[ParameterType.RedirectUri] = "redirect_uri";
		}

		private void FillRequestPathsDictionary()
		{
			_requestPaths[RequestType.AccountList] = @"account/list/";
			_requestPaths[RequestType.AccountInfo] = @"account/info/";
			_requestPaths[RequestType.AccountAchievements] = @"account/achievements/";
			_requestPaths[RequestType.ClanAccountInfo] = @"clans/accountinfo/";
			_requestPaths[RequestType.ClanInfo] = @"clans/info/";
			_requestPaths[RequestType.ClanGlossary] = @"clans/glossary/";
			_requestPaths[RequestType.TanksAcievements] = @"tanks/achievements/";
			_requestPaths[RequestType.TanksStat] = @"tanks/stats/";
			_requestPaths[RequestType.EncyclopediaAchievements] = @"encyclopedia/achievements/";
			_requestPaths[RequestType.EncyclopediaInfo] = @"encyclopedia/info/";
			_requestPaths[RequestType.EncyclopediaVehicles] = @"encyclopedia/vehicles/";
			_requestPaths[RequestType.Prolongate] = @"auth/prolongate/";
			_requestPaths[RequestType.Login] = @"auth/login/";
		}

	}
}
