namespace WotBlitzStatician
{
	using System.IO;
	using Microsoft.AspNetCore.Hosting;

	public class Program
	{
		public static void Main(string[] args)
		{
			var host = new WebHostBuilder()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseStartup<Startup>()
				//.UseApplicationInsights()
				.UseIISIntegration()
				.UseKestrel()
				.Build();

			host.Run();
		}
	}
}