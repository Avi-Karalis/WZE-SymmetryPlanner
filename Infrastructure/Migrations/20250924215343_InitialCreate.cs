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
                name: "unitspecialabilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ValueX = table.Column<string>(type: "text", nullable: true),
                    ValueY = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unitspecialabilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weapons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CCMod = table.Column<short>(type: "smallint", nullable: true),
                    CCDam = table.Column<short>(type: "smallint", nullable: true),
                    ShortRange = table.Column<short>(type: "smallint", nullable: true),
                    ShortRangeMod = table.Column<short>(type: "smallint", nullable: true),
                    ShortRangeDam = table.Column<short>(type: "smallint", nullable: true),
                    LongRange = table.Column<short>(type: "smallint", nullable: true),
                    LongRangeMod = table.Column<short>(type: "smallint", nullable: true),
                    LongRangeDam = table.Column<short>(type: "smallint", nullable: true),
                    CCDamageMultiplier = table.Column<short>(type: "smallint", nullable: false),
                    SRDamageMultiplier = table.Column<short>(type: "smallint", nullable: false),
                    LRDamageMultiplier = table.Column<short>(type: "smallint", nullable: false),
                    CritFail = table.Column<short>(type: "smallint", nullable: false),
                    DynamicDAM = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "weaponspecialabilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ValueX = table.Column<string>(type: "text", nullable: true),
                    ValueY = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weaponspecialabilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "unitunitspecialabilities",
                columns: table => new
                {
                    UnitId = table.Column<Guid>(type: "uuid", nullable: false),
                    UnitSpecialAbilityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unitunitspecialabilities", x => new { x.UnitId, x.UnitSpecialAbilityId });
                    table.ForeignKey(
                        name: "FK_unitunitspecialabilities_units_unitid",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_unitunitspecialabilities_unitspecialabilities_unitspecialabilityid",
                        column: x => x.UnitSpecialAbilityId,
                        principalTable: "unitspecialabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "weaponweaponspecialabilities",
                columns: table => new
                {
                    WeaponId = table.Column<Guid>(type: "uuid", nullable: false),
                    WeaponSpecialAbilityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weaponweaponspecialabilities", x => new { x.WeaponId, x.WeaponSpecialAbilityId });
                    table.ForeignKey(
                        name: "FK_weaponweaponspecialabilities_Weapons_WeaponId",
                        column: x => x.WeaponId,
                        principalTable: "Weapons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_weaponweaponspecialabilities_weaponspecialabilities_WeaponS~",
                        column: x => x.WeaponSpecialAbilityId,
                        principalTable: "weaponspecialabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_unitunitspecialabilities_UnitSpecialAbilityId",
                table: "unitunitspecialabilities",
                column: "UnitSpecialAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitWeapons_WeaponId",
                table: "UnitWeapons",
                column: "WeaponId");

            migrationBuilder.CreateIndex(
                name: "IX_weaponweaponspecialabilities_WeaponSpecialAbilityId",
                table: "weaponweaponspecialabilities",
                column: "WeaponSpecialAbilityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "unitunitspecialabilities");

            migrationBuilder.DropTable(
                name: "UnitWeapons");

            migrationBuilder.DropTable(
                name: "weaponweaponspecialabilities");

            migrationBuilder.DropTable(
                name: "unitspecialabilities");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Weapons");

            migrationBuilder.DropTable(
                name: "weaponspecialabilities");
        }
    }
}
