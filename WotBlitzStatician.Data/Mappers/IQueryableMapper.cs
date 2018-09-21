using System.Linq;
using WotBlitzStatician.Model.MapperLogic;

public interface IQueryableMapper<in TSource, TDestination> : IMapper<TSource, TDestination>
{
    IQueryable<TDestination> ProjectTo(IQueryable<TSource> source);
}