namespace WotBlitzStatician.Data
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.Reflection;
	using System.Text;
	using Microsoft.EntityFrameworkCore;

	public static class DataAccessorHelpers
	{
		public static void Merge<T>(this DbContext context, DbSet<T> destination, IList<T> sourse) where T : class
		{
			//ToDo: Maybe it's better to use ON CONFLICT IGNORE sqllite feature?..
			var entityType = typeof(T);
			var keyProperty = FindKey(entityType);
			if (keyProperty == null)
			{
				//If KeyAttribute is omitted, just insert new values
				destination.AddRange(sourse);
				return;
			}
			foreach (var sourseEntity in sourse)
			{
				var keyValue = keyProperty.GetValue(sourseEntity);

				if (destination.FromSql(GenerateSql(entityType.Name, keyProperty, keyValue)).Any())
				{
					destination.Attach(sourseEntity);
					context.Entry(sourseEntity).State = EntityState.Modified;
				}
				else
				{
					destination.Add(sourseEntity);
				}
			}
		}

		private static PropertyInfo FindKey(Type entityType)
		{
			var properties = entityType.GetProperties();
			return properties.FirstOrDefault(propertyInfo => propertyInfo.CustomAttributes.Any(a => a.AttributeType == typeof(KeyAttribute)));
		}
		private static string GenerateSql(string entityName, PropertyInfo keyProperty, object keyValue)
		{
			// ToDo: Fast and dirty method. Think about ef expression..
			var sql = new StringBuilder("select * from ");
			sql.Append(entityName);
			sql.Append(" where ");
			sql.Append(keyProperty.Name);
			sql.Append(" = ");
			if (keyProperty.PropertyType == typeof(string))
			{
				sql.Append("'");
			}
			sql.Append(keyValue);
			if (keyProperty.PropertyType == typeof(string))
			{
				sql.Append("'");
			}

			return sql.ToString();
		}
	}
}