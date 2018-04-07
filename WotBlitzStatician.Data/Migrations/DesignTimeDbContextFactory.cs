using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WotBlitzStatician.Data.Migrations
{
	public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BlitzStaticianDbContext>
	{
		// PM> Add-Migration InitialCreate
		// PM> Update-Database
		// PM> Script-Migration

		public BlitzStaticianDbContext CreateDbContext(string[] args)
		{
			var configuration = new ConfigurationBuilder()
						.SetBasePath(Directory.GetCurrentDirectory())
						.AddJsonFile("appsettings.json")
						.Build();
			var builder = new DbContextOptionsBuilder<BlitzStaticianDbContext>();
			var connectionString = configuration.GetConnectionString("BlitzStatician");
			builder.UseSqlServer(connectionString);
			return new BlitzStaticianDbContext(builder.Options);
		}
	}
}
