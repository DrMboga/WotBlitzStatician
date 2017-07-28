namespace WotBlitzStatician.WotApiClient
{
	using Autofac;
	using WotBlitzStatician.WotApiClient.RequestStringBuilder;

	public static class WargamingApiClientInstaller
	{
		public static void ConfigureWargamingApi(this ContainerBuilder containerBuilder)
		{
			// ToDo: register the parameters factory and inject it into RequestBuilder

			containerBuilder.RegisterType<RequestBuilder>().As<IRequestBuilder>();
			// Manually calling the internal constructor
			containerBuilder.Register(c => new WargamingApiClient(c.Resolve<IRequestBuilder>(), c.Resolve<IWgApiConfiguration>().ProxySettings))
				.As<IWargamingApiClient>();

		}

	}
}