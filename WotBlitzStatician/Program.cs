using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using WotBlitzStatician.NLogProvider;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNet.OData.Extensions;

namespace WotBlitzStatician
{
	public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseNLog("NLog.config")
                .ConfigureServices(services =>
                {
                  services.AddAutofac();
                  services.AddOData();
                })
                .UseStartup<Startup>()
                .Build();
    }
}
