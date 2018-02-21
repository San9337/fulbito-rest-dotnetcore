using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace datalayer.Migrations
{
    public partial class Teams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RealTeam",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SkilledFoot",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "SkilledFoot",
                table: "Users",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "RealTeamId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CountryName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RealTeamId",
                table: "Users",
                column: "RealTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Name_CountryName",
                table: "Teams",
                columns: new[] { "Name", "CountryName" });

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Teams_RealTeamId",
                table: "Users",
                column: "RealTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Teams_RealTeamId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Users_RealTeamId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RealTeamId",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "SkilledFoot",
                table: "Users",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "RealTeam",
                table: "Users",
                nullable: true);
        }
    }
}
