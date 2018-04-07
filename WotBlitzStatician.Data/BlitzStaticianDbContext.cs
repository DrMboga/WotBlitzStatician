using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WotBlitzStatician.Data.TypeConfigurations;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data
{
    public class BlitzStaticianDbContext : DbContext
	{
		private readonly bool _configureOutside;
		private readonly string _connectionString;
		private readonly ILoggerFactory _loggerFactory;

		public BlitzStaticianDbContext(DbContextOptions options) : base(options)
		{
			_configureOutside = true;
		}

		public BlitzStaticianDbContext(string connectionString, ILoggerFactory loggerFactory)
		{
			_configureOutside = false;
			_connectionString = connectionString;
			_loggerFactory = loggerFactory;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (_configureOutside)
				return;
			optionsBuilder.UseLoggerFactory(_loggerFactory);
			optionsBuilder.UseSqlServer(_connectionString);
		}



		public DbSet<AccountClanInfo> AccountClanInfo { get; set; }
		public DbSet<AccountClanHistory> AccountClanHistory { get; set; }
		public DbSet<AccountInfo> AccountInfo { get; set; }
		public DbSet<AccountInfoAchievement> AccountInfoAchievement { get; set; }
		public DbSet<AccountInfoPrivate> AccountInfoPrivate { get; set; }
		public DbSet<AccountInfoStatistics> AccountInfoStatistics { get; set; }
		public DbSet<AccountInfoTankAchievement> AccountInfoTankAchievement { get; set; }
		public DbSet<AccountTankStatistics> AccountTankStatistics { get; set; }
		public DbSet<PresentAccountTanks> PresentAccountTanks { get; set; }
		public DbSet<Achievement> Achievement { get; set; }
		public DbSet<AchievementOption> AchievementOption { get; set; }
		public DbSet<AchievementSection> AchievementSection { get; set; }
		public DbSet<DictionaryClanRole> DictionaryClanRole { get; set; }
		public DbSet<DictionaryLanguage> DictionaryLanguage { get; set; }
		public DbSet<DictionaryNations> DictionaryNation { get; set; }
		public DbSet<DictionaryVehicleType> DictionaryVehicleType { get; set; }
		public DbSet<FragListItem> Frags { get; set; }
		public DbSet<VehicleEncyclopedia> VehicleEncyclopedia { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("wotb");
			modelBuilder.Entity<VehicleEncyclopedia>(VehicleEncyclopediaTypeConfiguration.Configure);
			modelBuilder.Entity<FragListItem>(FragsTypeConfiguration.Configure);
			modelBuilder.Entity<DictionaryVehicleType>(e => e.HasKey(v => v.VehicleTypeId));
			modelBuilder.Entity<DictionaryNations>(e => e.HasKey(v => v.NationId));
			modelBuilder.Entity<DictionaryLanguage>(e => e.HasKey(v => v.LanguageId));
			modelBuilder.Entity<DictionaryClanRole>(e => e.HasKey(v => v.ClanRoleId));
			modelBuilder.Entity<AchievementSection>(e => e.HasKey(v => v.Section));
			modelBuilder.Entity<AchievementOption>(e => e.HasKey(v => v.AcievementOptionId));
			modelBuilder.Entity<Achievement>(AchievementTypeConfiguration.Configure);
			modelBuilder.Entity<AccountInfo>(AccountInfoTypeConfiguration.Configure);
			modelBuilder.Entity<AccountInfoStatistics>(AccountInfoStatisticsTypeConfiguration.Configure);
			modelBuilder.Entity<AccountInfoPrivate>(PrivateAccountInfoTypeConfiguration.Configure);
			modelBuilder.Entity<AccountClanInfo>(AccountClanInfoTypeConfiguration.Configure);
			modelBuilder.Entity<AccountClanHistory>(AccountClanHistoryTypeConfiguration.Configure);
			modelBuilder.Entity<AccountInfoAchievement>(AccountInfoAchevementTypeConfiguration.Configure);
			modelBuilder.Entity<AccountTankStatistics>(AccountTankStatisticsTypeConfiguration.Configure);
			modelBuilder.Entity<PresentAccountTanks>(PresentAccountTanksTypeConfiguration.Configure);
			modelBuilder.Entity<AccountInfoTankAchievement>(TankAchievementTypeConfiguration.Configure);
		}
	}
}
