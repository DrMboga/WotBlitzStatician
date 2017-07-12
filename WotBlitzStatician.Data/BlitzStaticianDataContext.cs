namespace WotBlitzStatician.Data
{
    using Microsoft.EntityFrameworkCore;
    using WotBlitzStatician.Model;

    // dotnet ef migrations add InitialCreate
    // dotnet ef database update

    public class BlitzStaticianDataContext : DbContext
    {
        public DbSet<AccountInfo> Account { get; set; }
        public DbSet<AccountInfoPrivate> AccountInfoPrivate { get; set; }
        public DbSet<AccountInfoStatistics> AccountStatictics { get; set; }
        public DbSet<AccountTankStatistics> AccountTankStatistics { get; set; }
        public DbSet<AccountClanInfo> AccountClanInfo { get; set; }

        public DbSet<AccountInfoAchievment> AccountInfoAchievments { get; set; }
        public DbSet<AccountInfoTankAchievment> AccountInfoTankAchievments { get; set; }

        public DbSet<VehicleEncyclopedia> VehiclesEncyclopedia { get; set; }
        public DbSet<AchievementOption> AchievementOptions { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<DictionaryLanguage> DictionaryLanguage { get; set; }
        public DbSet<DictionaryNations> DictionaryNations { get; set; }
        public DbSet<DictionaryVehicleType> DictionaryVehicleType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite(@"Data Source=..\..\..\BlitzStatician.db");
            optionsBuilder.UseSqlite(@"Data Source=/Users/mike/Developer/WotBlitzStatician/WotBlitzStatician.Logic.Tests/BlitzStatician.db");
        }
    }
}
