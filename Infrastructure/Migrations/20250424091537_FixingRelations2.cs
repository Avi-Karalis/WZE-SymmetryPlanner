using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixingRelations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeaponWeaponSpecialAbility_WeaponSpecialAbilities_WeaponSpe~",
                table: "WeaponWeaponSpecialAbility");

            migrationBuilder.DropForeignKey(
                name: "FK_WeaponWeaponSpecialAbility_Weapons_WeaponId",
                table: "WeaponWeaponSpecialAbility");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeaponWeaponSpecialAbility",
                table: "WeaponWeaponSpecialAbility");

            migrationBuilder.RenameTable(
                name: "WeaponWeaponSpecialAbility",
                newName: "WeaponWeaponSpecialAbilities");

            migrationBuilder.RenameIndex(
                name: "IX_WeaponWeaponSpecialAbility_WeaponSpecialAbilityId",
                table: "WeaponWeaponSpecialAbilities",
                newName: "IX_WeaponWeaponSpecialAbilities_WeaponSpecialAbilityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeaponWeaponSpecialAbilities",
                table: "WeaponWeaponSpecialAbilities",
                columns: new[] { "WeaponId", "WeaponSpecialAbilityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WeaponWeaponSpecialAbilities_WeaponSpecialAbilities_WeaponS~",
                table: "WeaponWeaponSpecialAbilities",
                column: "WeaponSpecialAbilityId",
                principalTable: "WeaponSpecialAbilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeaponWeaponSpecialAbilities_Weapons_WeaponId",
                table: "WeaponWeaponSpecialAbilities",
                column: "WeaponId",
                principalTable: "Weapons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeaponWeaponSpecialAbilities_WeaponSpecialAbilities_WeaponS~",
                table: "WeaponWeaponSpecialAbilities");

            migrationBuilder.DropForeignKey(
                name: "FK_WeaponWeaponSpecialAbilities_Weapons_WeaponId",
                table: "WeaponWeaponSpecialAbilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeaponWeaponSpecialAbilities",
                table: "WeaponWeaponSpecialAbilities");

            migrationBuilder.RenameTable(
                name: "WeaponWeaponSpecialAbilities",
                newName: "WeaponWeaponSpecialAbility");

            migrationBuilder.RenameIndex(
                name: "IX_WeaponWeaponSpecialAbilities_WeaponSpecialAbilityId",
                table: "WeaponWeaponSpecialAbility",
                newName: "IX_WeaponWeaponSpecialAbility_WeaponSpecialAbilityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeaponWeaponSpecialAbility",
                table: "WeaponWeaponSpecialAbility",
                columns: new[] { "WeaponId", "WeaponSpecialAbilityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WeaponWeaponSpecialAbility_WeaponSpecialAbilities_WeaponSpe~",
                table: "WeaponWeaponSpecialAbility",
                column: "WeaponSpecialAbilityId",
                principalTable: "WeaponSpecialAbilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeaponWeaponSpecialAbility_Weapons_WeaponId",
                table: "WeaponWeaponSpecialAbility",
                column: "WeaponId",
                principalTable: "Weapons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
