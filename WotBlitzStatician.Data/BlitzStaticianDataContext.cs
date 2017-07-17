namespace WotBlitzStatician.Data
{
    using Microsoft.EntityFrameworkCore;
    using WotBlitzStatician.Model;

    // dotnet ef migrations add InitialCreate
    // dotnet ef database update

    public class BlitzStaticianDataContext : DbContext
    {
	    private readonly string _connectionString;

//	    public BlitzStaticianDataContext()
//			:this(@"Data Source=..\..\..\BlitzStatician.db")
//	    {
//	    }

	    public BlitzStaticianDataContext(string connectionString)
	    {
		    _connectionString = connectionString;
	    }

	    public DbSet<AccountInfo> AccountInfo { get; set; }
        public DbSet<AccountInfoPrivate> AccountInfoPrivate { get; set; }
        public DbSet<AccountInfoStatistics> AccountInfoStatistics { get; set; }
        public DbSet<AccountTankStatistics> AccountTankStatistics { get; set; }
        public DbSet<AccountClanInfo> AccountClanInfo { get; set; }

        public DbSet<AccountInfoAchievment> AccountInfoAchievment { get; set; }
        public DbSet<AccountInfoTankAchievment> AccountInfoTankAchievment { get; set; }

        public DbSet<VehicleEncyclopedia> VehicleEncyclopedia { get; set; }
        public DbSet<AchievementOption> AchievementOption { get; set; }
        public DbSet<Achievement> Achievement { get; set; }
        public DbSet<DictionaryLanguage> DictionaryLanguage { get; set; }
        public DbSet<DictionaryNations> DictionaryNations { get; set; }
        public DbSet<DictionaryVehicleType> DictionaryVehicleType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
	        optionsBuilder.UseSqlite(_connectionString);
        }
    }
}
