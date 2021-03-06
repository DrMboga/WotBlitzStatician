﻿namespace WotBlitzStatician.Model.MapperLogic
{
	/// <summary> 
	/// Common Mapper Interface.
	/// </summary>
	/// <typeparam name="TSource">
	/// Source mapping type.
	/// </typeparam>
	/// <typeparam name="TDestination">
	/// Destination mapping type.
	/// </typeparam>
	public interface IMapper<in TSource, TDestination>
	{
		/// <summary>
		/// Maps data from source to destination objects. 
		/// </summary>
		/// <param name="source">
		/// Source Object.
		/// </param>
		/// <returns>
		/// The <see cref="TDestination"/>.
		/// Destination object.
		/// </returns>
		TDestination Map(TSource source);
		TDestination Map(TSource source, TDestination destination);
	}
}