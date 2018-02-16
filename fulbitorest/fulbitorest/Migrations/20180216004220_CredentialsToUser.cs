using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FulbitoRest.Migrations
{
    public partial class CredentialsToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserCredentials_CredentialsId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CredentialsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CredentialsId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UserCredentials",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCredentials_Users_Id",
                table: "UserCredentials",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCredentials_Users_Id",
                table: "UserCredentials");

            migrationBuilder.AddColumn<int>(
                name: "CredentialsId",
                table: "Users",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UserCredentials",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CredentialsId",
                table: "Users",
                column: "CredentialsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserCredentials_CredentialsId",
                table: "Users",
                column: "CredentialsId",
                principalTable: "UserCredentials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
