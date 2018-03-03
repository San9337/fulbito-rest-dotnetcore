using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using model.Model;
using System;
using System.Collections.Generic;

namespace datalayer.Migrations
{
    public partial class TeamFanTableRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Teams_RealTeamId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.CreateTable(
                name: "ProfessionalTeams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CountryName = table.Column<string>(nullable: true),
                    LogoUrl = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionalTeams", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionalTeams_Name_CountryName",
                table: "ProfessionalTeams",
                columns: new[] { "Name", "CountryName" });

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ProfessionalTeams_RealTeamId",
                table: "Users",
                column: "RealTeamId",
                principalTable: "ProfessionalTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql("INSERT INTO professionalteams VALUES(1,'" + ProfessionalTeam.UNDEFINED_NAME + "','" + Country.UNDEFINED_NAME + "','" + ProfessionalTeam.UNDEFINED_LOGO + "')");

            migrationBuilder.Sql(@"
INSERT INTO `professionalteams` VALUES ('2', 'Argentina', 'Argentino Juniors',null);
INSERT INTO `professionalteams` VALUES ('3', 'Argentina', 'Arsenal de Sarandi',null);
INSERT INTO `professionalteams` VALUES ('4', 'Argentina', 'Atletico Tucuman',null);
INSERT INTO `professionalteams` VALUES ('5', 'Argentina', 'Banfield',null);
INSERT INTO `professionalteams` VALUES ('6', 'Argentina', 'Belgrano de Cordoba',null);
INSERT INTO `professionalteams` VALUES ('7', 'Argentina', 'Boca Juniors',null);
INSERT INTO `professionalteams` VALUES ('8', 'Argentina', 'Chacarita Juniors',null);
INSERT INTO `professionalteams` VALUES ('9', 'Argentina', 'Colon de Santa Fe',null);
INSERT INTO `professionalteams` VALUES ('10', 'Argentina', 'Defensa y Justicia',null);
INSERT INTO `professionalteams` VALUES ('11', 'Argentina', 'Estudiantes de la Plata',null);
INSERT INTO `professionalteams` VALUES ('12', 'Argentina', 'Gimnasia La Plata',null);
INSERT INTO `professionalteams` VALUES ('13', 'Argentina', 'Godoy Cruz de Mendoza',null);
INSERT INTO `professionalteams` VALUES ('14', 'Argentina', 'Huracan',null);
INSERT INTO `professionalteams` VALUES ('15', 'Argentina', 'Independiente',null);
INSERT INTO `professionalteams` VALUES ('16', 'Argentina', 'Lanus',null);
INSERT INTO `professionalteams` VALUES ('17', 'Argentina', 'Newells Old Boys',null);
INSERT INTO `professionalteams` VALUES ('18', 'Argentina', 'Olimpo de Bahia Blanca',null);
INSERT INTO `professionalteams` VALUES ('19', 'Argentina', 'Patronato',null);
INSERT INTO `professionalteams` VALUES ('20', 'Argentina', 'Racing Club',null);
INSERT INTO `professionalteams` VALUES ('21', 'Argentina', 'River Plate',null);
INSERT INTO `professionalteams` VALUES ('22', 'Argentina', 'Rosario Central',null);
INSERT INTO `professionalteams` VALUES ('23', 'Argentina', 'San Lorenzo',null);
INSERT INTO `professionalteams` VALUES ('24', 'Argentina', 'San Martin de San Juan',null);
INSERT INTO `professionalteams` VALUES ('25', 'Argentina', 'Talleres de Cordoba',null);
INSERT INTO `professionalteams` VALUES ('26', 'Argentina', 'Temperley',null);
INSERT INTO `professionalteams` VALUES ('27', 'Argentina', 'Tigre',null);
INSERT INTO `professionalteams` VALUES ('28', 'Argentina', 'Union de Santa Fe',null);
INSERT INTO `professionalteams` VALUES ('29', 'Argentina', 'Velez Sarsfield',null);
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ProfessionalTeams_RealTeamId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ProfessionalTeams");

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
    }
}
