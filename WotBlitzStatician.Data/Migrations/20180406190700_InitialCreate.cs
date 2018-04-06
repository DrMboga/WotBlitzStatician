using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WotBlitzStatician.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "wotb");

            migrationBuilder.CreateTable(
                name: "AccountClanHistory",
                schema: "wotb",
                columns: table => new
                {
                    AccountClanHistoryId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<long>(nullable: false),
                    ClanId = table.Column<long>(nullable: true),
                    PlayerJoinedAt = table.Column<DateTime>(nullable: false),
                    ClanTag = table.Column<string>(nullable: true),
                    ClanName = table.Column<string>(nullable: true),
                    PlayerRole = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountClanHistory", x => x.AccountClanHistoryId);
                });

            migrationBuilder.CreateTable(
                name: "AccountInfo",
                schema: "wotb",
                columns: table => new
                {
                    AccountId = table.Column<long>(nullable: false),
                    NickName = table.Column<string>(nullable: true),
                    AccountCreatedAt = table.Column<DateTime>(nullable: true),
                    LastBattleTime = table.Column<DateTime>(nullable: true),
                    AccessToken = table.Column<string>(nullable: true),
                    AccessTokenExpiration = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountInfo", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "AccountInfoAchievement",
                schema: "wotb",
                columns: table => new
                {
                    AccountInfoAchievementId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AchievementId = table.Column<string>(nullable: true),
                    AccountId = table.Column<long>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    IsMaxSeries = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    TankId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountInfoAchievement", x => x.AccountInfoAchievementId);
                });

            migrationBuilder.CreateTable(
                name: "AccountInfoPrivate",
                schema: "wotb",
                columns: table => new
                {
                    AccountInfoPrivateId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<long>(nullable: false),
                    BanInfo = table.Column<string>(nullable: true),
                    BanTime = table.Column<DateTime>(nullable: true),
                    BattleLifeTimeInSeconds = table.Column<int>(nullable: false),
                    Credits = table.Column<long>(nullable: false),
                    FreeXp = table.Column<long>(nullable: false),
                    Gold = table.Column<long>(nullable: false),
                    IsPremium = table.Column<bool>(nullable: false),
                    PremiumExpiresAt = table.Column<DateTime>(nullable: true),
                    ContactsUngrouped = table.Column<string>(nullable: true),
                    ContactsBlocked = table.Column<string>(nullable: true),
                    ContactsGroups = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountInfoPrivate", x => x.AccountInfoPrivateId);
                });

            migrationBuilder.CreateTable(
                name: "AccountTankStatistics",
                schema: "wotb",
                columns: table => new
                {
                    AccountTankStatisticId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<long>(nullable: false),
                    TankId = table.Column<long>(nullable: false),
                    BattleLifeTimeInSeconds = table.Column<int>(nullable: false),
                    LastBattleTime = table.Column<DateTime>(nullable: false),
                    MarkOfMastery = table.Column<int>(nullable: false),
                    InGarage = table.Column<bool>(nullable: false),
                    InGarageUpdated = table.Column<DateTime>(nullable: true),
                    Battles = table.Column<long>(nullable: false),
                    CapturePoints = table.Column<long>(nullable: false),
                    DamageDealt = table.Column<long>(nullable: false),
                    DamageReceived = table.Column<long>(nullable: false),
                    DroppedCapturePoints = table.Column<long>(nullable: false),
                    Frags = table.Column<long>(nullable: false),
                    Frags8P = table.Column<long>(nullable: false),
                    Hits = table.Column<long>(nullable: false),
                    Losses = table.Column<long>(nullable: false),
                    MaxFrags = table.Column<long>(nullable: false),
                    MaxXp = table.Column<long>(nullable: false),
                    Shots = table.Column<long>(nullable: false),
                    Spotted = table.Column<long>(nullable: false),
                    SurvivedBattles = table.Column<long>(nullable: false),
                    WinAndSurvived = table.Column<long>(nullable: false),
                    Wins = table.Column<long>(nullable: false),
                    Xp = table.Column<long>(nullable: false),
                    Wn7 = table.Column<double>(nullable: false),
                    Wn8 = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTankStatistics", x => x.AccountTankStatisticId);
                });

            migrationBuilder.CreateTable(
                name: "AchievementSection",
                schema: "wotb",
                columns: table => new
                {
                    Section = table.Column<string>(nullable: false),
                    SectionName = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementSection", x => x.Section);
                });

            migrationBuilder.CreateTable(
                name: "DictionaryClanRole",
                schema: "wotb",
                columns: table => new
                {
                    ClanRoleId = table.Column<string>(nullable: false),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictionaryClanRole", x => x.ClanRoleId);
                });

            migrationBuilder.CreateTable(
                name: "DictionaryLanguage",
                schema: "wotb",
                columns: table => new
                {
                    LanguageId = table.Column<string>(nullable: false),
                    LanguageName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictionaryLanguage", x => x.LanguageId);
                });

            migrationBuilder.CreateTable(
                name: "DictionaryNation",
                schema: "wotb",
                columns: table => new
                {
                    NationId = table.Column<string>(nullable: false),
                    NationName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictionaryNation", x => x.NationId);
                });

            migrationBuilder.CreateTable(
                name: "DictionaryVehicleType",
                schema: "wotb",
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
                name: "Frags",
                schema: "wotb",
                columns: table => new
                {
                    FragListItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<long>(nullable: false),
                    KilledTankId = table.Column<long>(nullable: false),
                    FragsCount = table.Column<int>(nullable: false),
                    TankId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frags", x => x.FragListItemId);
                });

            migrationBuilder.CreateTable(
                name: "VehicleEncyclopedia",
                schema: "wotb",
                columns: table => new
                {
                    TankId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Tier = table.Column<long>(nullable: false),
                    Nation = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsPremium = table.Column<bool>(nullable: false),
                    PriceCredit = table.Column<long>(nullable: false),
                    PriceGold = table.Column<long>(nullable: false),
                    PreviewImageUrl = table.Column<string>(nullable: true),
                    NormalImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleEncyclopedia", x => x.TankId);
                });

            migrationBuilder.CreateTable(
                name: "AccountClanInfo",
                schema: "wotb",
                columns: table => new
                {
                    AccountClanInfoId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<long>(nullable: false),
                    ClanId = table.Column<long>(nullable: false),
                    PlayerJoinedAt = table.Column<DateTime>(nullable: false),
                    PlayerRole = table.Column<string>(nullable: true),
                    ClanCreatedAt = table.Column<DateTime>(nullable: false),
                    ClanLeaderName = table.Column<string>(nullable: true),
                    MembersCount = table.Column<long>(nullable: false),
                    ClanTag = table.Column<string>(nullable: true),
                    ClanName = table.Column<string>(nullable: true),
                    ClanMotto = table.Column<string>(nullable: true),
                    ClanDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountClanInfo", x => x.AccountClanInfoId);
                    table.ForeignKey(
                        name: "FK_AccountClanInfo_AccountInfo_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "wotb",
                        principalTable: "AccountInfo",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountInfoStatistics",
                schema: "wotb",
                columns: table => new
                {
                    AccountInfoStatisticsId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<long>(nullable: false),
                    Battles = table.Column<long>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CapturePoints = table.Column<long>(nullable: false),
                    DamageDealt = table.Column<long>(nullable: false),
                    DamageReceived = table.Column<long>(nullable: false),
                    DroppedCapturePoints = table.Column<long>(nullable: false),
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
                    WinAndSurvived = table.Column<long>(nullable: false),
                    Wins = table.Column<long>(nullable: false),
                    Xp = table.Column<long>(nullable: false),
                    AvgTier = table.Column<double>(nullable: false),
                    Wn7 = table.Column<double>(nullable: false),
                    Wn8 = table.Column<double>(nullable: false),
                    AccountInfoPrivateId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountInfoStatistics", x => x.AccountInfoStatisticsId);
                    table.ForeignKey(
                        name: "FK_AccountInfoStatistics_AccountInfo_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "wotb",
                        principalTable: "AccountInfo",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountInfoStatistics_AccountInfoPrivate_AccountInfoPrivateId",
                        column: x => x.AccountInfoPrivateId,
                        principalSchema: "wotb",
                        principalTable: "AccountInfoPrivate",
                        principalColumn: "AccountInfoPrivateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Achievement",
                schema: "wotb",
                columns: table => new
                {
                    AchievementId = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Condition = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    ImageBig = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Section = table.Column<string>(nullable: true),
                    Order = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievement", x => x.AchievementId);
                    table.ForeignKey(
                        name: "FK_Achievement_AchievementSection_Section",
                        column: x => x.Section,
                        principalSchema: "wotb",
                        principalTable: "AchievementSection",
                        principalColumn: "Section",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AchievementOption",
                schema: "wotb",
                columns: table => new
                {
                    AcievementOptionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AchievementId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    ImageBig = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementOption", x => x.AcievementOptionId);
                    table.ForeignKey(
                        name: "FK_AchievementOption_Achievement_AchievementId",
                        column: x => x.AchievementId,
                        principalSchema: "wotb",
                        principalTable: "Achievement",
                        principalColumn: "AchievementId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountClanHistory_AccountId",
                schema: "wotb",
                table: "AccountClanHistory",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountClanInfo_AccountId",
                schema: "wotb",
                table: "AccountClanInfo",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountClanInfo_AccountId_ClanId",
                schema: "wotb",
                table: "AccountClanInfo",
                columns: new[] { "AccountId", "ClanId" });

            migrationBuilder.CreateIndex(
                name: "IX_AccountInfo_LastBattleTime",
                schema: "wotb",
                table: "AccountInfo",
                column: "LastBattleTime");

            migrationBuilder.CreateIndex(
                name: "IX_AccountInfoAchievement_AccountId_AchievementId",
                schema: "wotb",
                table: "AccountInfoAchievement",
                columns: new[] { "AccountId", "AchievementId" });

            migrationBuilder.CreateIndex(
                name: "IX_AccountInfoAchievement_AccountId_AchievementId_TankId",
                schema: "wotb",
                table: "AccountInfoAchievement",
                columns: new[] { "AccountId", "AchievementId", "TankId" });

            migrationBuilder.CreateIndex(
                name: "IX_AccountInfoStatistics_AccountId",
                schema: "wotb",
                table: "AccountInfoStatistics",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountInfoStatistics_AccountInfoPrivateId",
                schema: "wotb",
                table: "AccountInfoStatistics",
                column: "AccountInfoPrivateId",
                unique: true,
                filter: "[AccountInfoPrivateId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTankStatistics_AccountId",
                schema: "wotb",
                table: "AccountTankStatistics",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTankStatistics_TankId",
                schema: "wotb",
                table: "AccountTankStatistics",
                column: "TankId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievement_Section",
                schema: "wotb",
                table: "Achievement",
                column: "Section");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementOption_AchievementId",
                schema: "wotb",
                table: "AchievementOption",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_Frags_AccountId_KilledTankId",
                schema: "wotb",
                table: "Frags",
                columns: new[] { "AccountId", "KilledTankId" });

            migrationBuilder.CreateIndex(
                name: "IX_Frags_AccountId_KilledTankId_TankId",
                schema: "wotb",
                table: "Frags",
                columns: new[] { "AccountId", "KilledTankId", "TankId" });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleEncyclopedia_Nation",
                schema: "wotb",
                table: "VehicleEncyclopedia",
                column: "Nation");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleEncyclopedia_Tier",
                schema: "wotb",
                table: "VehicleEncyclopedia",
                column: "Tier");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleEncyclopedia_Type",
                schema: "wotb",
                table: "VehicleEncyclopedia",
                column: "Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountClanHistory",
                schema: "wotb");

            migrationBuilder.DropTable(
                name: "AccountClanInfo",
                schema: "wotb");

            migrationBuilder.DropTable(
                name: "AccountInfoAchievement",
                schema: "wotb");

            migrationBuilder.DropTable(
                name: "AccountInfoStatistics",
                schema: "wotb");

            migrationBuilder.DropTable(
                name: "AccountTankStatistics",
                schema: "wotb");

            migrationBuilder.DropTable(
                name: "AchievementOption",
                schema: "wotb");

            migrationBuilder.DropTable(
                name: "DictionaryClanRole",
                schema: "wotb");

            migrationBuilder.DropTable(
                name: "DictionaryLanguage",
                schema: "wotb");

            migrationBuilder.DropTable(
                name: "DictionaryNation",
                schema: "wotb");

            migrationBuilder.DropTable(
                name: "DictionaryVehicleType",
                schema: "wotb");

            migrationBuilder.DropTable(
                name: "Frags",
                schema: "wotb");

            migrationBuilder.DropTable(
                name: "VehicleEncyclopedia",
                schema: "wotb");

            migrationBuilder.DropTable(
                name: "AccountInfo",
                schema: "wotb");

            migrationBuilder.DropTable(
                name: "AccountInfoPrivate",
                schema: "wotb");

            migrationBuilder.DropTable(
                name: "Achievement",
                schema: "wotb");

            migrationBuilder.DropTable(
                name: "AchievementSection",
                schema: "wotb");
        }
    }
}
