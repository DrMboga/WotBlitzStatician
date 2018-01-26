﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using WotBlitzStatician.Data;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.Migrations
{
    [DbContext(typeof(BlitzStaticianDbContext))]
    [Migration("20180126194955_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("wotb")
                .HasAnnotation("ProductVersion", "2.1.0-preview2-28184")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WotBlitzStatician.Model.AccountClanInfo", b =>
                {
                    b.Property<long>("AccountClanInfoId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("AccountInfoAccountId");

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

                    b.HasIndex("AccountInfoAccountId")
                        .IsUnique()
                        .HasFilter("[AccountInfoAccountId] IS NOT NULL");

                    b.ToTable("AccountClanInfo");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.AccountInfo", b =>
                {
                    b.Property<long>("AccountId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccessToken");

                    b.Property<DateTime?>("AccessTokenExpiration");

                    b.Property<DateTime>("AccountCreatedAt");

                    b.Property<DateTime?>("LastBattleTime");

                    b.Property<string>("NickName");

                    b.HasKey("AccountId");

                    b.HasIndex("LastBattleTime");

                    b.ToTable("AccountInfo");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.AccountInfoAchievement", b =>
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

                    b.HasIndex("AccountId", "AchievementId");

                    b.ToTable("AccountInfoAchievement");

                    b.HasDiscriminator<string>("Discriminator").HasValue("AccountInfoAchievement");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.AccountInfoPrivate", b =>
                {
                    b.Property<long>("AccountInfoPrivateId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AccountId");

                    b.Property<string>("BanInfo");

                    b.Property<DateTime?>("BanTime");

                    b.Property<TimeSpan>("BattleLifeTime");

                    b.Property<string>("ContactsBlocked");

                    b.Property<string>("ContactsGroups");

                    b.Property<string>("ContactsUngrouped");

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

                    b.Property<long?>("AccountInfoPrivateId");

                    b.Property<double>("AvgTier");

                    b.Property<long>("Battles");

                    b.Property<long>("CapturePoints");

                    b.Property<long>("DamageDealt");

                    b.Property<long>("DamageReceived");

                    b.Property<long>("DroppedCapturePoints");

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

                    b.Property<double>("Wn8");

                    b.Property<long>("Xp");

                    b.HasKey("AccountInfoStatisticsId");

                    b.HasIndex("AccountId");

                    b.HasIndex("AccountInfoPrivateId")
                        .IsUnique()
                        .HasFilter("[AccountInfoPrivateId] IS NOT NULL");

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

                    b.Property<long>("Frags");

                    b.Property<long>("Frags8P");

                    b.Property<long>("Hits");

                    b.Property<bool>("InGarage");

                    b.Property<DateTime?>("InGarageUpdated");

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

                    b.Property<double>("Wn8");

                    b.Property<long>("Xp");

                    b.HasKey("AccountTankStatisticId");

                    b.HasIndex("AccountId");

                    b.HasIndex("TankId");

                    b.ToTable("AccountTankStatistics");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.Achievement", b =>
                {
                    b.Property<string>("AchievementId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Condition");

                    b.Property<string>("Description");

                    b.Property<string>("Image");

                    b.Property<string>("ImageBig");

                    b.Property<string>("Name");

                    b.Property<long>("Order");

                    b.Property<string>("Section");

                    b.HasKey("AchievementId");

                    b.HasIndex("Section");

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

                    b.HasIndex("AchievementId");

                    b.ToTable("AchievementOption");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.AchievementSection", b =>
                {
                    b.Property<string>("Section")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Order");

                    b.Property<string>("SectionName");

                    b.HasKey("Section");

                    b.ToTable("AchievementSection");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.DictionaryClanRole", b =>
                {
                    b.Property<string>("ClanRoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RoleName");

                    b.HasKey("ClanRoleId");

                    b.ToTable("DictionaryClanRole");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.DictionaryLanguage", b =>
                {
                    b.Property<string>("LanguageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LanguageName");

                    b.HasKey("LanguageId");

                    b.ToTable("DictionaryLanguage");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.DictionaryNations", b =>
                {
                    b.Property<string>("NationId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("NationName");

                    b.HasKey("NationId");

                    b.ToTable("DictionaryNation");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.DictionaryVehicleType", b =>
                {
                    b.Property<string>("VehicleTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("VehicleTypeName");

                    b.HasKey("VehicleTypeId");

                    b.ToTable("DictionaryVehicleType");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.FragListItem", b =>
                {
                    b.Property<int>("FragListItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AccountId");

                    b.Property<int>("FragsCount");

                    b.Property<long>("KilledTankId");

                    b.Property<long?>("TankId");

                    b.HasKey("FragListItemId");

                    b.HasIndex("AccountId", "KilledTankId");

                    b.HasIndex("AccountId", "KilledTankId", "TankId");

                    b.ToTable("Frags");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.VehicleEncyclopedia", b =>
                {
                    b.Property<long>("TankId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("IsPremium");

                    b.Property<string>("Name");

                    b.Property<string>("Nation");

                    b.Property<string>("NormalImageUrl");

                    b.Property<string>("PreviewImageUrl");

                    b.Property<long>("PriceCredit");

                    b.Property<long>("PriceGold");

                    b.Property<long>("Tier");

                    b.Property<string>("Type");

                    b.HasKey("TankId");

                    b.HasIndex("Nation");

                    b.HasIndex("Tier");

                    b.HasIndex("Type");

                    b.ToTable("VehicleEncyclopedia");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.AccountInfoTankAchievement", b =>
                {
                    b.HasBaseType("WotBlitzStatician.Model.AccountInfoAchievement");

                    b.Property<long>("TankId");

                    b.HasIndex("AccountId", "AchievementId", "TankId");

                    b.ToTable("AccountInfoTankAchievement");

                    b.HasDiscriminator().HasValue("AccountInfoTankAchievement");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.AccountClanInfo", b =>
                {
                    b.HasOne("WotBlitzStatician.Model.AccountInfo", "AccountInfo")
                        .WithOne("AccountClanInfo")
                        .HasForeignKey("WotBlitzStatician.Model.AccountClanInfo", "AccountInfoAccountId");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.AccountInfoStatistics", b =>
                {
                    b.HasOne("WotBlitzStatician.Model.AccountInfo", "AccountInfo")
                        .WithMany("AccountInfoStatistics")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WotBlitzStatician.Model.AccountInfoPrivate", "AccountInfoPrivate")
                        .WithOne("AccountInfoStatistics")
                        .HasForeignKey("WotBlitzStatician.Model.AccountInfoStatistics", "AccountInfoPrivateId");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.Achievement", b =>
                {
                    b.HasOne("WotBlitzStatician.Model.AchievementSection", "AchievementSection")
                        .WithMany("Achievements")
                        .HasForeignKey("Section");
                });

            modelBuilder.Entity("WotBlitzStatician.Model.AchievementOption", b =>
                {
                    b.HasOne("WotBlitzStatician.Model.Achievement", "Achievement")
                        .WithMany("Options")
                        .HasForeignKey("AchievementId");
                });
#pragma warning restore 612, 618
        }
    }
}
