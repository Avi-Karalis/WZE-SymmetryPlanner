using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ForceListId",
                table: "Units",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ForceLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Faction = table.Column<string>(type: "text", nullable: false),
                    Allegiance = table.Column<string>(type: "text", nullable: false),
                    MaxDp = table.Column<int>(type: "integer", nullable: false),
                    MaxSp = table.Column<int>(type: "integer", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForceLists", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Units_ForceListId",
                table: "Units",
                column: "ForceListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_ForceLists_ForceListId",
                table: "Units",
                column: "ForceListId",
                principalTable: "ForceLists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_ForceLists_ForceListId",
                table: "Units");

            migrationBuilder.DropTable(
                name: "ForceLists");

            migrationBuilder.DropIndex(
                name: "IX_Units_ForceListId",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "ForceListId",
                table: "Units");
        }
    }
}
