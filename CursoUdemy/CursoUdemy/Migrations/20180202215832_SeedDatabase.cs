using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CursoUdemy.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Makes1')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Makes2')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Makes3')");

            migrationBuilder.Sql("INSERT INTO Model (Name, MakeID) VALUES ('Make1-ModelA', (SELECT ID FROM Makes WHERE Name = 'Makes1'))");
            migrationBuilder.Sql("INSERT INTO Model (Name, MakeID) VALUES ('Make1-ModelB', (SELECT ID FROM Makes WHERE Name = 'Makes1'))");
            migrationBuilder.Sql("INSERT INTO Model (Name, MakeID) VALUES ('Make1-ModelC', (SELECT ID FROM Makes WHERE Name = 'Makes1'))");

            migrationBuilder.Sql("INSERT INTO Model (Name, MakeID) VALUES ('Make2-ModelA', (SELECT ID FROM Makes WHERE Name = 'Makes2'))");
            migrationBuilder.Sql("INSERT INTO Model (Name, MakeID) VALUES ('Make2-ModelB', (SELECT ID FROM Makes WHERE Name = 'Makes2'))");
            migrationBuilder.Sql("INSERT INTO Model (Name, MakeID) VALUES ('Make2-ModelC', (SELECT ID FROM Makes WHERE Name = 'Makes2'))");

            migrationBuilder.Sql("INSERT INTO Model (Name, MakeID) VALUES ('Make3-ModelA', (SELECT ID FROM Makes WHERE Name = 'Makes3'))");
            migrationBuilder.Sql("INSERT INTO Model (Name, MakeID) VALUES ('Make3-ModelB', (SELECT ID FROM Makes WHERE Name = 'Makes3'))");
            migrationBuilder.Sql("INSERT INTO Model (Name, MakeID) VALUES ('Make3-ModelC', (SELECT ID FROM Makes WHERE Name = 'Makes3'))");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Makes WHERE Name IN ('Make1','Make2','Make3')");
            migrationBuilder.Sql("DELETE FROM Model");
        }
    }
}
