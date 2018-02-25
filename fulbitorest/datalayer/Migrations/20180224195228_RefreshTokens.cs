using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace datalayer.Migrations
{
    public partial class RefreshTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthContexts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    AuthMethod = table.Column<int>(nullable: false),
                    ExpireDate = table.Column<DateTime>(nullable: false),
                    RefreshToken = table.Column<string>(nullable: true),
                    Revoked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthContexts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthContexts_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthContexts_RefreshToken",
                table: "AuthContexts",
                column: "RefreshToken");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthContexts");
        }
    }
}
