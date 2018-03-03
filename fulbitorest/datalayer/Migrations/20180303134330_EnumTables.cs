using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using model.Enums;
using model.Utils;
using System;
using System.Collections.Generic;

namespace datalayer.Migrations
{
    public partial class EnumTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Foot",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                });
            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                });
            migrationBuilder.CreateTable(
                name: "AuthenticationMethod",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                });

            foreach (var value in EnumUtils.Values<Foot>())
            {
                migrationBuilder.Sql("INSERT INTO Foot VALUES(" +(int)value+",'"+value.GetDescription()+"')");
            }

            foreach (var value in EnumUtils.Values<Gender>())
            {
                migrationBuilder.Sql("INSERT INTO Gender VALUES(" + (int)value + ",'" + value.GetDescription() + "')");
            }

            foreach (var value in EnumUtils.Values<AuthenticationMethod>())
            {
                migrationBuilder.Sql("INSERT INTO AuthenticationMethod VALUES(" + (int)value + ",'" + value.GetDescription() + "')");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Foot");
            migrationBuilder.DropTable(
                name: "Gender");
            migrationBuilder.DropTable(
                name: "AuthenticationMethod");

        }
    }
}
