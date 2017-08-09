namespace WotBlitzStatician
{
	using System;
	using Autofac;
	using Autofac.Extensions.DependencyInjection;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;
	using WotBlitzStatician.Log4net;

	public class Startup
	{
//		public void ConfigureServices(IServiceCollection services)
//		{
//			services.AddLogging();
//			services.AddMvc();
//
//		}
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddLogging();
			services.AddMvc();

			var containerBuilder = new ContainerBuilder();
			containerBuilder.ConfigureWotBlitzStatician();
			containerBuilder.Populate(services);
			var container = containerBuilder.Build();
			return new AutofacServiceProvider(container);
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddLog4Net();

			app.UseErrorHandler();
//			app.UseDeveloperExceptionPage();

			app.UseMvcWithDefaultRoute();
		}
	}
}
