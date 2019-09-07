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
  using System.Text;
  using Microsoft.AspNet.OData.Extensions;
  using Microsoft.AspNetCore.Authentication.JwtBearer;
  using Microsoft.AspNetCore.Diagnostics.HealthChecks;
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Routing;
  using Microsoft.IdentityModel.Tokens;
  using WotBlitzStatician.Controllers;
  using WotBlitzStatician.HealthCheck;
  using WotBlitzStatician.JwtSecurity;
  using WotBlitzStatician.OdataConfiguration;

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
      services.AddMemoryCache();
      services.AddMvc()
        .AddJsonOptions(options =>
        {
          options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
          // 2019-02-17T14:36:24Z
          options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
        });

      services.AddHealthChecks()
        .AddCheck<DataBaseCheck>("Dtabase check")
        .AddCheck<WargamingCheck>("Wargaming check");

      // configure jwt authentication
      string secret = Configuration.GetSection("SecurityConfiguration").Get<SecurityConfiguration>().Secret;
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
             options.TokenValidationParameters = new TokenValidationParameters
             {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               ValidIssuer = "WotBlitzStatician.com",
               ValidAudience = "WotBlitzStatician.com",
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
             };
           });
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
      var wgApiConfig = new Appsettings();
      wgApiConfig.ProxySettings = new ProxySettings();
      wgApiConfig.SecurityConfiguration = new SecurityConfiguration();
      Configuration.GetSection("WgApi").Bind(wgApiConfig);
      Configuration.GetSection("ProxySettings").Bind(wgApiConfig.ProxySettings);
      Configuration.GetSection("SecurityConfiguration").Bind(wgApiConfig.SecurityConfiguration);

      builder.RegisterInstance<IProxySettings>(wgApiConfig.ProxySettings);
      builder.RegisterInstance<IWgApiConfiguration>(wgApiConfig);
      builder.RegisterInstance<ISecurityConfiguration>(wgApiConfig.SecurityConfiguration);
      builder.RegisterType<SecurityService>().As<ISecurityServise>();
      builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();
      builder.RegisterType<GuestAccountCache>();
      builder.ConfigureWargamingApi();
      builder.ConfigureDataAccessor(Configuration.GetConnectionString("BlitzStatician"));
      builder.ConfigureBlitzStaticianLogic();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {

      app.UseErrorHandler();
      app.UseStaticFiles();
      app.UseCors(builder =>
        builder.WithOrigins("http://localhost:4200")
          .AllowAnyHeader()
          .AllowAnyMethod());
      app.UseAuthentication();
      app.UseHealthChecks("/api/health", new HealthCheckOptions()
      {
        ResponseWriter = HealthCheckResponse.WriteResponse
      });
      app.UseMvc(routes =>
          {
            routes.MapRoute("default", "{controller}/{action}");
            routes.MapODataServiceRoute("odata", "api", OdataModelsConfiguration.GetEdmModel());
            routes.MapRoute("Spa", "{*url}", defaults: new { controller = "Home", action = "Spa" });
          }
  );
    }
  }
}
