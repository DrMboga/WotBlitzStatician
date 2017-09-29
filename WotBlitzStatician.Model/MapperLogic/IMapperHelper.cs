namespace WotBlitzStatician.Model.MapperLogic
{
	public interface IMapperHelper
	{
		TDestination Map<TSource, TDestination>(TSource source);
		TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
	}
}