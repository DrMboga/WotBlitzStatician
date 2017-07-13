using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WotBlitzStatician.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountClanInfo",
                columns: table => new
                {
                    AccountClanInfoId = table.Column<long>(nullable: false)
                        .Annotation("Autoincrement", true),
                    AccountId = table.Column<long>(nullable: false),
                    ClanCreatedAt = table.Column<DateTime>(nullable: false),
                    ClanDescription = table.Column<string>(nullable: true),
                    ClanId = table.Column<long>(nullable: false),
                    ClanLeaderName = table.Column<string>(nullable: true),
                    ClanMotto = table.Column<string>(nullable: true),
                    ClanName = table.Column<string>(nullable: true),
                    ClanTag = table.Column<string>(nullable: true),
                    MembersCount = table.Column<long>(nullable: false),
                    PlayerJoinedAt = table.Column<DateTime>(nullable: false),
                    PlayerRole = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountClanInfo", x => x.AccountClanInfoId);
                });

            migrationBuilder.CreateTable(
                name: "AccountInfo",
                columns: table => new
                {
                    AccountId = table.Column<long>(nullable: false)
                        .Annotation("Autoincrement", true),
                    AccountCreatedAt = table.Column<DateTime>(nullable: false),
                    IsLastSession = table.Column<bool>(nullable: false),
                    LastBattleTime = table.Column<DateTime>(nullable: true),
                    NickName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountInfo", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "AccountInfoAchievment",
                columns: table => new
                {
                    AccountInfoAchievmentId = table.Column<string>(nullable: false),
                    AccountId = table.Column<long>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    IsMaxSeries = table.Column<bool>(nullable: false),
                    TankId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountInfoAchievment", x => x.AccountInfoAchievmentId);
                });

            migrationBuilder.CreateTable(
                name: "AccountInfoPrivate",
                columns: table => new
                {
                    AccountInfoPrivateId = table.Column<long>(nullable: false)
                        .Annotation("Autoincrement", true),
                    AccountId = table.Column<long>(nullable: false),
                    BanInfo = table.Column<string>(nullable: true),
                    BanTime = table.Column<DateTime>(nullable: true),
                    BattleLifeTime = table.Column<long>(nullable: false),
                    Credits = table.Column<long>(nullable: false),
                    FreeXp = table.Column<long>(nullable: false),
                    Gold = table.Column<long>(nullable: false),
                    IsPremium = table.Column<bool>(nullable: false),
                    PremiumExpiresAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountInfoPrivate", x => x.AccountInfoPrivateId);
                });

            migrationBuilder.CreateTable(
                name: "AccountInfoStatistics",
                columns: table => new
                {
                    AccountInfoStatisticsId = table.Column<long>(nullable: false)
                        .Annotation("Autoincrement", true),
                    AccountId = table.Column<long>(nullable: false),
                    AvgTier = table.Column<double>(nullable: false),
                    Battles = table.Column<long>(nullable: false),
                    CapturePoints = table.Column<long>(nullable: false),
                    DamageDealt = table.Column<long>(nullable: false),
                    DamageReceived = table.Column<long>(nullable: false),
                    DroppedCapturePoints = table.Column<long>(nullable: false),
                    Effectivity = table.Column<double>(nullable: false),
                    Frags = table.Column<long>(nullable: false),
                    Frags8P = table.Column<long>(nullable: false),
                    Hits = table.Column<long>(nullable: false),
                    Losses = table.Column<long>(nullable: false),
                    MaxFrags = table.Column<long>(nullable: false),
                    MaxFragsTankId = table.Column<long>(nullable: false),
                    MaxXp = table.Column<long>(nullable: false),
                    MaxXpTankId = table.Column<long>(nullable: false),
                    Shots = table.Column<long>(nullable: false),
                    Spotted = table.Column<long>(nullable: false),
                    SurvivedBattles = table.Column<long>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    WinAndSurvived = table.Column<long>(nullable: false),
                    Wins = table.Column<long>(nullable: false),
                    Wn7 = table.Column<double>(nullable: false),
                    Xp = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountInfoStatistics", x => x.AccountInfoStatisticsId);
                });

            migrationBuilder.CreateTable(
                name: "AccountTankStatistics",
                columns: table => new
                {
                    AccountTankStatisticId = table.Column<long>(nullable: false)
                        .Annotation("Autoincrement", true),
                    AccountId = table.Column<long>(nullable: false),
                    BattleLifeTime = table.Column<TimeSpan>(nullable: false),
                    Battles = table.Column<long>(nullable: false),
                    CapturePoints = table.Column<long>(nullable: false),
                    DamageDealt = table.Column<long>(nullable: false),
                    DamageReceived = table.Column<long>(nullable: false),
                    DroppedCapturePoints = table.Column<long>(nullable: false),
                    Effectivity = table.Column<double>(nullable: false),
                    Frags = table.Column<long>(nullable: false),
                    Frags8P = table.Column<long>(nullable: false),
                    Hits = table.Column<long>(nullable: false),
                    InGarage = table.Column<bool>(nullable: false),
                    LastBattleTime = table.Column<DateTime>(nullable: false),
                    Losses = table.Column<long>(nullable: false),
                    MarkOfMastery = table.Column<int>(nullable: false),
                    MaxFrags = table.Column<long>(nullable: false),
                    MaxXp = table.Column<long>(nullable: false),
                    Shots = table.Column<long>(nullable: false),
                    Spotted = table.Column<long>(nullable: false),
                    SurvivedBattles = table.Column<long>(nullable: false),
                    TankId = table.Column<long>(nullable: false),
                    WinAndSurvived = table.Column<long>(nullable: false),
                    Wins = table.Column<long>(nullable: false),
                    Wn7 = table.Column<double>(nullable: false),
                    Xp = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTankStatistics", x => x.AccountTankStatisticId);
                });

            migrationBuilder.CreateTable(
                name: "Achievement",
                columns: table => new
                {
                    AchievementId = table.Column<string>(nullable: false),
                    Condition = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    ImageBig = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<long>(nullable: false),
                    Section = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievement", x => x.AchievementId);
                });

            migrationBuilder.CreateTable(
                name: "AchievementOption",
                columns: table => new
                {
                    AcievementOptionId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    AchievementId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    ImageBig = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementOption", x => x.AcievementOptionId);
                });

            migrationBuilder.CreateTable(
                name: "DictionaryLanguage",
                columns: table => new
                {
                    LanguageId = table.Column<string>(nullable: false),
                    LanguageName = table.Column<string>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictionaryLanguage", x => x.LanguageId);
                });

            migrationBuilder.CreateTable(
                name: "DictionaryNations",
                columns: table => new
                {
                    NationId = table.Column<string>(nullable: false),
                    NationName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictionaryNations", x => x.NationId);
                });

            migrationBuilder.CreateTable(
                name: "DictionaryVehicleType",
                columns: table => new
                {
                    VehicleTypeId = table.Column<string>(nullable: false),
                    VehicleTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictionaryVehicleType", x => x.VehicleTypeId);
                });

            migrationBuilder.CreateTable(
                name: "VehicleEncyclopedia",
                columns: table => new
                {
                    TankId = table.Column<long>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Description = table.Column<string>(nullable: true),
                    IsGift = table.Column<bool>(nullable: false),
                    IsPremium = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Nation = table.Column<string>(nullable: true),
                    NormalImageUrl = table.Column<string>(nullable: true),
                    PreviewImageUrl = table.Column<string>(nullable: true),
                    PriceCredit = table.Column<long>(nullable: false),
                    PriceGold = table.Column<long>(nullable: false),
                    ShortName = table.Column<string>(nullable: true),
                    Tag = table.Column<string>(nullable: true),
                    Tier = table.Column<long>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleEncyclopedia", x => x.TankId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountClanInfo");

            migrationBuilder.DropTable(
                name: "AccountInfo");

            migrationBuilder.DropTable(
                name: "AccountInfoAchievment");

            migrationBuilder.DropTable(
                name: "AccountInfoPrivate");

            migrationBuilder.DropTable(
                name: "AccountInfoStatistics");

            migrationBuilder.DropTable(
                name: "AccountTankStatistics");

            migrationBuilder.DropTable(
                name: "Achievement");

            migrationBuilder.DropTable(
                name: "AchievementOption");

            migrationBuilder.DropTable(
                name: "DictionaryLanguage");

            migrationBuilder.DropTable(
                name: "DictionaryNations");

            migrationBuilder.DropTable(
                name: "DictionaryVehicleType");

            migrationBuilder.DropTable(
                name: "VehicleEncyclopedia");
        }
    }
}
