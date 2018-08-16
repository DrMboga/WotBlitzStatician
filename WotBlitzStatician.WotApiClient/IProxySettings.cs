namespace WotBlitzStatician.WotApiClient
{
	public interface IProxySettings
	{
		bool UseProxy { get; set; }
		string ProxyAddress { get; set; }
		string Domain { get; set; }
		string User { get; set; }
		string PwdHash { get; set; }
	}
}