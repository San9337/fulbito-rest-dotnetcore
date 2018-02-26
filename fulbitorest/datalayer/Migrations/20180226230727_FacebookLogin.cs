using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace datalayer.Migrations
{
    public partial class FacebookLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthMethod",
                table: "UserCredentials",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthMethod",
                table: "UserCredentials");
        }
    }
}
