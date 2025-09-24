using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixingRelations3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitUnitSpecialAbility_UnitSpecialAbilities_UnitSpecialAbil~",
                table: "UnitUnitSpecialAbility");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitUnitSpecialAbility_Units_UnitId",
                table: "UnitUnitSpecialAbility");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnitUnitSpecialAbility",
                table: "UnitUnitSpecialAbility");

            migrationBuilder.RenameTable(
                name: "UnitUnitSpecialAbility",
                newName: "UnitUnitSpecialAbilities");

            migrationBuilder.RenameIndex(
                name: "IX_UnitUnitSpecialAbility_UnitSpecialAbilityId",
                table: "UnitUnitSpecialAbilities",
                newName: "IX_UnitUnitSpecialAbilities_UnitSpecialAbilityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnitUnitSpecialAbilities",
                table: "UnitUnitSpecialAbilities",
                columns: new[] { "UnitId", "UnitSpecialAbilityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UnitUnitSpecialAbilities_UnitSpecialAbilities_UnitSpecialAb~",
                table: "UnitUnitSpecialAbilities",
                column: "UnitSpecialAbilityId",
                principalTable: "UnitSpecialAbilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitUnitSpecialAbilities_Units_UnitId",
                table: "UnitUnitSpecialAbilities",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitUnitSpecialAbilities_UnitSpecialAbilities_UnitSpecialAb~",
                table: "UnitUnitSpecialAbilities");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitUnitSpecialAbilities_Units_UnitId",
                table: "UnitUnitSpecialAbilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnitUnitSpecialAbilities",
                table: "UnitUnitSpecialAbilities");

            migrationBuilder.RenameTable(
                name: "UnitUnitSpecialAbilities",
                newName: "UnitUnitSpecialAbility");

            migrationBuilder.RenameIndex(
                name: "IX_UnitUnitSpecialAbilities_UnitSpecialAbilityId",
                table: "UnitUnitSpecialAbility",
                newName: "IX_UnitUnitSpecialAbility_UnitSpecialAbilityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnitUnitSpecialAbility",
                table: "UnitUnitSpecialAbility",
                columns: new[] { "UnitId", "UnitSpecialAbilityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UnitUnitSpecialAbility_UnitSpecialAbilities_UnitSpecialAbil~",
                table: "UnitUnitSpecialAbility",
                column: "UnitSpecialAbilityId",
                principalTable: "UnitSpecialAbilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitUnitSpecialAbility_Units_UnitId",
                table: "UnitUnitSpecialAbility",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
