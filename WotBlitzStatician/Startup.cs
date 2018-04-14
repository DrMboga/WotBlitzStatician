using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WotBlitzStatician.Data;
using WotBlitzStatician.Logic;
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

            builder.RegisterInstance<IProxySettings>(wgApiConfig.ProxySettings);
            builder.RegisterInstance<IWgApiConfiguration>(wgApiConfig);
            builder.ConfigureWargamingApi();
            builder.ConfigureDataAccessor(Configuration.GetConnectionString("BlitzStatician"));
            builder.ConfigureBlitzStaticianLogic();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                app.UseErrorHandler();
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
