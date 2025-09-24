using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixingRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitSpecialAbilities_Units_UnitId",
                table: "UnitSpecialAbilities");

            migrationBuilder.DropForeignKey(
                name: "FK_WeaponSpecialAbilities_Weapons_WeaponId",
                table: "WeaponSpecialAbilities");

            migrationBuilder.DropIndex(
                name: "IX_WeaponSpecialAbilities_WeaponId",
                table: "WeaponSpecialAbilities");

            migrationBuilder.DropIndex(
                name: "IX_UnitSpecialAbilities_UnitId",
                table: "UnitSpecialAbilities");

            migrationBuilder.DropColumn(
                name: "WeaponId",
                table: "WeaponSpecialAbilities");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "UnitSpecialAbilities");

            migrationBuilder.CreateTable(
                name: "UnitUnitSpecialAbility",
                columns: table => new
                {
                    UnitId = table.Column<Guid>(type: "uuid", nullable: false),
                    UnitSpecialAbilityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitUnitSpecialAbility", x => new { x.UnitId, x.UnitSpecialAbilityId });
                    table.ForeignKey(
                        name: "FK_UnitUnitSpecialAbility_UnitSpecialAbilities_UnitSpecialAbil~",
                        column: x => x.UnitSpecialAbilityId,
                        principalTable: "UnitSpecialAbilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnitUnitSpecialAbility_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeaponWeaponSpecialAbility",
                columns: table => new
                {
                    WeaponId = table.Column<Guid>(type: "uuid", nullable: false),
                    WeaponSpecialAbilityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeaponWeaponSpecialAbility", x => new { x.WeaponId, x.WeaponSpecialAbilityId });
                    table.ForeignKey(
                        name: "FK_WeaponWeaponSpecialAbility_WeaponSpecialAbilities_WeaponSpe~",
                        column: x => x.WeaponSpecialAbilityId,
                        principalTable: "WeaponSpecialAbilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeaponWeaponSpecialAbility_Weapons_WeaponId",
                        column: x => x.WeaponId,
                        principalTable: "Weapons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitUnitSpecialAbility_UnitSpecialAbilityId",
                table: "UnitUnitSpecialAbility",
                column: "UnitSpecialAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_WeaponWeaponSpecialAbility_WeaponSpecialAbilityId",
                table: "WeaponWeaponSpecialAbility",
                column: "WeaponSpecialAbilityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitUnitSpecialAbility");

            migrationBuilder.DropTable(
                name: "WeaponWeaponSpecialAbility");

            migrationBuilder.AddColumn<Guid>(
                name: "WeaponId",
                table: "WeaponSpecialAbilities",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitId",
                table: "UnitSpecialAbilities",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WeaponSpecialAbilities_WeaponId",
                table: "WeaponSpecialAbilities",
                column: "WeaponId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitSpecialAbilities_UnitId",
                table: "UnitSpecialAbilities",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitSpecialAbilities_Units_UnitId",
                table: "UnitSpecialAbilities",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WeaponSpecialAbilities_Weapons_WeaponId",
                table: "WeaponSpecialAbilities",
                column: "WeaponId",
                principalTable: "Weapons",
                principalColumn: "Id");
        }
    }
}
