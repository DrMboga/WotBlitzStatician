using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WotBlitzStatician.Data;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.Migrations
{
    [DbContext(typeof(BlitzStaticianDataContext))]
    [Migration("20170717094936_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.2");

            modelBuilder.Entity("WotBlitzStatician.Model.AccountClanInfo", b =>
                {
                    b.Property<long>("AccountClanInfoId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AccountId");

                    b.Property<DateTime>("ClanCreatedAt");

                    b.Property<string>("ClanDescription");

                    b.Property<long>("ClanId");

                    b.Property<string>("ClanLeaderName");

                    b.Property<string>("ClanMotto");

                    b.Property<string>("ClanName");

                    b.Property<string>("ClanTag");

                    b.Property<long>("MembersCount");

                    b.Property<DateTime>("PlayerJoinedAt");

                    b.Property<string>("PlayerRole");

                    b.HasKey("AccountClanInfoId");

                    b.ToTable("AccountClanInfo");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.AccountInfo", b =>
                {
                    b.Property<long>("AccountId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AccountCreatedAt");

                    b.Property<bool>("IsLastSession");

                    b.Property<DateTime?>("LastBattleTime");

                    b.Property<string>("NickName");

                    b.HasKey("AccountId");

                    b.ToTable("AccountInfo");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.AccountInfoAchievment", b =>
                {
                    b.Property<long>("AccountInfoAchievementId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AccountId");

                    b.Property<string>("AchievementId");

                    b.Property<int>("Count");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<bool>("IsMaxSeries");

                    b.HasKey("AccountInfoAchievementId");

                    b.ToTable("AccountInfoAchievment");

                    b.HasDiscriminator<string>("Discriminator").HasValue("AccountInfoAchievment");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.AccountInfoPrivate", b =>
                {
                    b.Property<long>("AccountInfoPrivateId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AccountId");

                    b.Property<string>("BanInfo");

                    b.Property<DateTime?>("BanTime");

                    b.Property<long>("BattleLifeTime");

                    b.Property<long>("Credits");

                    b.Property<long>("FreeXp");

                    b.Property<long>("Gold");

                    b.Property<bool>("IsPremium");

                    b.Property<DateTime?>("PremiumExpiresAt");

                    b.HasKey("AccountInfoPrivateId");

                    b.ToTable("AccountInfoPrivate");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.AccountInfoStatistics", b =>
                {
                    b.Property<long>("AccountInfoStatisticsId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AccountId");

                    b.Property<double>("AvgTier");

                    b.Property<long>("Battles");

                    b.Property<long>("CapturePoints");

                    b.Property<long>("DamageDealt");

                    b.Property<long>("DamageReceived");

                    b.Property<long>("DroppedCapturePoints");

                    b.Property<double>("Effectivity");

                    b.Property<long>("Frags");

                    b.Property<long>("Frags8P");

                    b.Property<long>("Hits");

                    b.Property<long>("Losses");

                    b.Property<long>("MaxFrags");

                    b.Property<long>("MaxFragsTankId");

                    b.Property<long>("MaxXp");

                    b.Property<long>("MaxXpTankId");

                    b.Property<long>("Shots");

                    b.Property<long>("Spotted");

                    b.Property<long>("SurvivedBattles");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<long>("WinAndSurvived");

                    b.Property<long>("Wins");

                    b.Property<double>("Wn7");

                    b.Property<long>("Xp");

                    b.HasKey("AccountInfoStatisticsId");

                    b.ToTable("AccountInfoStatistics");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.AccountTankStatistics", b =>
                {
                    b.Property<long>("AccountTankStatisticId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AccountId");

                    b.Property<TimeSpan>("BattleLifeTime");

                    b.Property<long>("Battles");

                    b.Property<long>("CapturePoints");

                    b.Property<long>("DamageDealt");

                    b.Property<long>("DamageReceived");

                    b.Property<long>("DroppedCapturePoints");

                    b.Property<double>("Effectivity");

                    b.Property<long>("Frags");

                    b.Property<long>("Frags8P");

                    b.Property<long>("Hits");

                    b.Property<bool>("InGarage");

                    b.Property<DateTime>("LastBattleTime");

                    b.Property<long>("Losses");

                    b.Property<int>("MarkOfMastery");

                    b.Property<long>("MaxFrags");

                    b.Property<long>("MaxXp");

                    b.Property<long>("Shots");

                    b.Property<long>("Spotted");

                    b.Property<long>("SurvivedBattles");

                    b.Property<long>("TankId");

                    b.Property<long>("WinAndSurvived");

                    b.Property<long>("Wins");

                    b.Property<double>("Wn7");

                    b.Property<long>("Xp");

                    b.HasKey("AccountTankStatisticId");

                    b.ToTable("AccountTankStatistics");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.Achievement", b =>
                {
                    b.Property<string>("AchievementId");

                    b.Property<string>("Condition");

                    b.Property<string>("Description");

                    b.Property<string>("Image");

                    b.Property<string>("ImageBig");

                    b.Property<string>("Name");

                    b.Property<long>("Order");

                    b.Property<string>("Section");

                    b.HasKey("AchievementId");

                    b.ToTable("Achievement");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.AchievementOption", b =>
                {
                    b.Property<int>("AcievementOptionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AchievementId");

                    b.Property<string>("Description");

                    b.Property<string>("Image");

                    b.Property<string>("ImageBig");

                    b.Property<string>("Name");

                    b.HasKey("AcievementOptionId");

                    b.ToTable("AchievementOption");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.DictionaryLanguage", b =>
                {
                    b.Property<string>("LanguageId");

                    b.Property<string>("LanguageName");

                    b.Property<DateTime>("LastUpdated");

                    b.HasKey("LanguageId");

                    b.ToTable("DictionaryLanguage");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.DictionaryNations", b =>
                {
                    b.Property<string>("NationId");

                    b.Property<string>("NationName");

                    b.HasKey("NationId");

                    b.ToTable("DictionaryNations");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.DictionaryVehicleType", b =>
                {
                    b.Property<string>("VehicleTypeId");

                    b.Property<string>("VehicleTypeName");

                    b.HasKey("VehicleTypeId");

                    b.ToTable("DictionaryVehicleType");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.VehicleEncyclopedia", b =>
                {
                    b.Property<long>("TankId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("IsGift");

                    b.Property<bool>("IsPremium");

                    b.Property<string>("Name");

                    b.Property<string>("Nation");

                    b.Property<string>("NormalImageUrl");

                    b.Property<string>("PreviewImageUrl");

                    b.Property<long>("PriceCredit");

                    b.Property<long>("PriceGold");

                    b.Property<string>("ShortName");

                    b.Property<string>("Tag");

                    b.Property<long>("Tier");

                    b.Property<string>("Type");

                    b.HasKey("TankId");

                    b.ToTable("VehicleEncyclopedia");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.AccountInfoTankAchievment", b =>
                {
                    b.HasBaseType("WotBlitzStatician.Model.AccountInfoAchievment");

                    b.Property<long>("TankId");

                    b.ToTable("AccountInfoTankAchievment");

                    b.HasDiscriminator().HasValue("AccountInfoTankAchievment");
                });
        }
    }
}
