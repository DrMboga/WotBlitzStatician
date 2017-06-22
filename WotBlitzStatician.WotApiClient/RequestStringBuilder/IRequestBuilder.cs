namespace WotBlitzStatician.WotApiClient.RequestStringBuilder
{
	internal interface IRequestBuilder
	{
		string BaseAddress { get; }
		string BuildRequestUrl(RequestType requestType, params RequestParameter[] parameters);
	}
}