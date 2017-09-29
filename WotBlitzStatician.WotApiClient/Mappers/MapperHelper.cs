namespace WotBlitzStatician.WotApiClient.Mappers
{
	using Autofac;
	using WotBlitzStatician.Model.MapperLogic;

	public class MapperHelper : IMapperHelper
	{
		private readonly IComponentContext _container;

		public MapperHelper(IComponentContext container)
		{
			_container = container;
		}

		public TDestination Map<TSource, TDestination>(TSource source)
		{
			var mapper = _container.Resolve<IMapper<TSource, TDestination>>();
			return mapper.Map(source);
		}

		public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
		{
			var mapper = _container.Resolve<IMapper<TSource, TDestination>>();
			return mapper.Map(source, destination);
		}
	}
}