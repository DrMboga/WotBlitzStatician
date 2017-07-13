namespace WotBlitzStatician.Data
{
	public class BlitzStaticianDataContextFactory : IBlitzStaticianDataContextFactory
	{
		private readonly string _connectionString;

		public BlitzStaticianDataContextFactory(string connectionString)
		{
			_connectionString = connectionString;
		}

		public BlitzStaticianDataContext CreateContext()
		{
			return new BlitzStaticianDataContext(_connectionString);
		}
	}
}