namespace WotBlitzStatician.WotApiClient.RequestStringBuilder
{
	public interface IRequestBuilder
	{
		string BaseAddress { get; }
		string BuildRequestUrl(RequestType requestType, params RequestParameter[] parameters);
	}
}