namespace WotBlitzStatician.Data
{
    using Microsoft.EntityFrameworkCore;
    using WotBlitzStaticitian.Model;

    public class BlitzStaticianDataContext : DbContext
    {
        public DbSet<AccountInfo> Account { get; set; }
        public DbSet<AccountInfoPrivate> AccountInfoPrivate { get; set; }
        public DbSet<AccountInfoStatistics> AccountStatictics { get; set; }
        public DbSet<AccountTankStatistics> AccountTankStatistics { get; set; }
        public DbSet<VehicleEncyclopedia> VehiclesEncyclopedia { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=BlitzStatician.db");
		}
    }
}
