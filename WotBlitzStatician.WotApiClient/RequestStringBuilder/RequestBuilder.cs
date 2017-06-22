namespace WotBlitzStatician.WotApiClient.RequestStringBuilder
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	internal class RequestBuilder
	{
		private readonly Uri _baseUri;
		private readonly Dictionary<RequestType, string> _requestPaths = new Dictionary<RequestType, string>();
		private readonly Dictionary<ParameterType, string> _parameterNames = new Dictionary<ParameterType, string>();
		private readonly List<RequestParameter> _requestParameters = new List<RequestParameter>();

		public RequestBuilder(string baseAddress, string applicationId, string language)
		{
			_baseUri = new Uri(baseAddress);
			FillRequestPathsDictionary();
			FillParameterNamesDictionary();
			_requestParameters.Add(new RequestParameter{ParameterType = ParameterType.ApplicationId, ParameterValue = applicationId});
			_requestParameters.Add(new RequestParameter{ParameterType = ParameterType.Language, ParameterValue = language});
		}

		public string BaseAddress => _baseUri.ToString();

		public string BuildRequestUrl(RequestType requestType, params RequestParameter[] parameters)
		{
			var query = new StringBuilder();

			// Common parameters
			_requestParameters.ForEach(p => query.Append($"{_parameterNames[p.ParameterType]}={p.ParameterValue}&"));
			// Method parameters
			parameters?.ToList().ForEach(p => query.Append($"{_parameterNames[p.ParameterType]}={p.ParameterValue}&"));
			// Removing last ampersand
			query.Remove(query.Length - 1, 1);

			return $"{_requestPaths[requestType]}?{query}";
		}

		private void FillParameterNamesDictionary()
		{
			_parameterNames[ParameterType.Language] = "language";
			_parameterNames[ParameterType.ApplicationId] = "application_id";
			_parameterNames[ParameterType.Search] = "search";
			_parameterNames[ParameterType.AccountId] = "account_id";

		}

		private void FillRequestPathsDictionary()
		{
			_requestPaths[RequestType.AccountList] = @"account/list/";
			_requestPaths[RequestType.AccountInfo] = @"account/info/";
			_requestPaths[RequestType.AccountAchievements] = @"account/achievements/";
			_requestPaths[RequestType.ClanAccountInfo] = @"clans/accountinfo/";
			_requestPaths[RequestType.ClanInfo] = @"clans/info/";
			_requestPaths[RequestType.TanksAcievements] = @"tanks/achievements/";
			_requestPaths[RequestType.TanksStat] = @"tanks/stats/";
			_requestPaths[RequestType.EncyclopediaAchievements] = @"encyclopedia/achievements/";
			_requestPaths[RequestType.EncyclopediaInfo] = @"encyclopedia/info/";
			_requestPaths[RequestType.EncyclopediaVehicles] = @"encyclopedia/vehicles/";
		}

	}
}