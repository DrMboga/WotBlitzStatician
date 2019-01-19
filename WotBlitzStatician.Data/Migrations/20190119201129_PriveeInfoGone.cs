using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WotBlitzStatician.Data.Migrations
{
    public partial class PriveeInfoGone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountInfoStatistics_AccountInfoPrivate_AccountInfoPrivateId",
                schema: "wotb",
                table: "AccountInfoStatistics");

            migrationBuilder.DropTable(
                name: "AccountInfoPrivate",
                schema: "wotb");

            migrationBuilder.DropIndex(
                name: "IX_AccountInfoStatistics_AccountInfoPrivateId",
                schema: "wotb",
                table: "AccountInfoStatistics");

            migrationBuilder.DropColumn(
                name: "Wn8",
                schema: "wotb",
                table: "AccountTankStatistics");

            migrationBuilder.DropColumn(
                name: "Wn8",
                schema: "wotb",
                table: "AccountInfoStatistics");

            migrationBuilder.AddColumn<string>(
                name: "NextTanksList",
                schema: "wotb",
                table: "VehicleEncyclopedia",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AccountInfoPrivateId",
                schema: "wotb",
                table: "AccountInfoStatistics",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextTanksList",
                schema: "wotb",
                table: "VehicleEncyclopedia");

            migrationBuilder.AddColumn<double>(
                name: "Wn8",
                schema: "wotb",
                table: "AccountTankStatistics",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<long>(
                name: "AccountInfoPrivateId",
                schema: "wotb",
                table: "AccountInfoStatistics",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<double>(
                name: "Wn8",
                schema: "wotb",
                table: "AccountInfoStatistics",
                nullable: false,
                defaultValue: 0.0);

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
                    ContactsBlocked = table.Column<string>(nullable: true),
                    ContactsGroups = table.Column<string>(nullable: true),
                    ContactsUngrouped = table.Column<string>(nullable: true),
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

            migrationBuilder.CreateIndex(
                name: "IX_AccountInfoStatistics_AccountInfoPrivateId",
                schema: "wotb",
                table: "AccountInfoStatistics",
                column: "AccountInfoPrivateId",
                unique: true,
                filter: "[AccountInfoPrivateId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountInfoStatistics_AccountInfoPrivate_AccountInfoPrivateId",
                schema: "wotb",
                table: "AccountInfoStatistics",
                column: "AccountInfoPrivateId",
                principalSchema: "wotb",
                principalTable: "AccountInfoPrivate",
                principalColumn: "AccountInfoPrivateId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
