using System.Linq;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WotBlitzStatician.Data;
using WotBlitzStatician.Logic;
using WotBlitzStatician.WotApiClient;

namespace WotBlitzStatician
{
  using Microsoft.AspNetCore.Routing;

  public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }


		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc()
				.AddJsonOptions(options =>
				{
					options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
				});
		}

		public void ConfigureContainer(ContainerBuilder builder)
		{
			var wgApiConfig = new Appsettings();
			wgApiConfig.ProxySettings = new ProxySettings();
			Configuration.GetSection("WgApi").Bind(wgApiConfig);
			Configuration.GetSection("ProxySettings").Bind(wgApiConfig.ProxySettings);

			builder.RegisterInstance<IProxySettings>(wgApiConfig.ProxySettings);
			builder.RegisterInstance<IWgApiConfiguration>(wgApiConfig);
			builder.ConfigureWargamingApi();
			builder.ConfigureDataAccessor(Configuration.GetConnectionString("BlitzStatician"));
			builder.ConfigureBlitzStaticianLogic();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{

			app.UseErrorHandler();
			app.UseStaticFiles();
			app.UseMvc(routes =>
			  {
			    routes.MapRoute("default", "{controller}/{action}");
			    routes.MapRoute("Spa", "{*url}", defaults: new {controller = "Home", action = "Spa"});
			  }
			);
		}
	}
}
