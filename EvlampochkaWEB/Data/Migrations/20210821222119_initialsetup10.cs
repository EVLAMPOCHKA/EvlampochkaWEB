using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EvlampochkaWEB.Data.Migrations
{
    public partial class initialsetup10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikesNumber",
                table: "Item");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Item",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CollectionSize",
                table: "Collection",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "CollectionSize",
                table: "Collection");

            migrationBuilder.AddColumn<int>(
                name: "LikesNumber",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
