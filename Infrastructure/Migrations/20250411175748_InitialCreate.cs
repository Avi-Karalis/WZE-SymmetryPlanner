using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Faction = table.Column<string>(type: "text", nullable: false),
                    UnitType = table.Column<string>(type: "text", nullable: false),
                    Designation = table.Column<string[]>(type: "text[]", nullable: false),
                    DesignationTypeLimit = table.Column<string>(type: "text", nullable: true),
                    DesignationLimitValue = table.Column<short>(type: "smallint", nullable: false),
                    DPCost = table.Column<short>(type: "smallint", nullable: false),
                    SPCost = table.Column<short>(type: "smallint", nullable: false),
                    MV = table.Column<short>(type: "smallint", nullable: false),
                    MW = table.Column<short>(type: "smallint", nullable: false),
                    CC = table.Column<short>(type: "smallint", nullable: false),
                    ST = table.Column<short>(type: "smallint", nullable: false),
                    DEF = table.Column<short>(type: "smallint", nullable: false),
                    AR = table.Column<short>(type: "smallint", nullable: false),
                    W = table.Column<short>(type: "smallint", nullable: false),
                    PW = table.Column<short>(type: "smallint", nullable: false),
                    LD = table.Column<short>(type: "smallint", nullable: false),
                    FactionAvailabilities = table.Column<string[]>(type: "text[]", nullable: true),
                    Base = table.Column<short>(type: "smallint", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weapons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CCMod = table.Column<short>(type: "smallint", nullable: false),
                    CCDam = table.Column<short>(type: "smallint", nullable: false),
                    ShortRange = table.Column<short>(type: "smallint", nullable: false),
                    ShortRangeMod = table.Column<short>(type: "smallint", nullable: false),
                    ShortRangeDam = table.Column<short>(type: "smallint", nullable: false),
                    LongRange = table.Column<short>(type: "smallint", nullable: false),
                    LongRangeMod = table.Column<short>(type: "smallint", nullable: false),
                    LongRangeDam = table.Column<short>(type: "smallint", nullable: false),
                    CritFail = table.Column<short>(type: "smallint", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitSpecialAbilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ValueX = table.Column<string>(type: "text", nullable: true),
                    ValueY = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    UnitId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitSpecialAbilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitSpecialAbilities_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnitWeapons",
                columns: table => new
                {
                    UnitId = table.Column<Guid>(type: "uuid", nullable: false),
                    WeaponId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitWeapons", x => new { x.UnitId, x.WeaponId });
                    table.ForeignKey(
                        name: "FK_UnitWeapons_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnitWeapons_Weapons_WeaponId",
                        column: x => x.WeaponId,
                        principalTable: "Weapons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeaponSpecialAbilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ValueX = table.Column<string>(type: "text", nullable: true),
                    ValueY = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    WeaponId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeaponSpecialAbilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeaponSpecialAbilities_Weapons_WeaponId",
                        column: x => x.WeaponId,
                        principalTable: "Weapons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitSpecialAbilities_UnitId",
                table: "UnitSpecialAbilities",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitWeapons_WeaponId",
                table: "UnitWeapons",
                column: "WeaponId");

            migrationBuilder.CreateIndex(
                name: "IX_WeaponSpecialAbilities_WeaponId",
                table: "WeaponSpecialAbilities",
                column: "WeaponId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitSpecialAbilities");

            migrationBuilder.DropTable(
                name: "UnitWeapons");

            migrationBuilder.DropTable(
                name: "WeaponSpecialAbilities");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Weapons");
        }
    }
}
