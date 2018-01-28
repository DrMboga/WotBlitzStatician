using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WotBlitzStatician.WotApiClient;

namespace WotBlitzStatician
{
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
			services.AddMvc();


		}

		public void ConfigureContainer(ContainerBuilder builder)
		{
			var wgApiConfig = new Appsettings();
			wgApiConfig.ProxySettings = new ProxySettings();
			Configuration.GetSection("WgApi").Bind(wgApiConfig);
			Configuration.GetSection("ProxySettings").Bind(wgApiConfig.ProxySettings);

			builder.RegisterInstance<IWgApiConfiguration>(wgApiConfig);
			builder.ConfigureWargamingApi();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			app.UseMvc();
		}
	}
}
