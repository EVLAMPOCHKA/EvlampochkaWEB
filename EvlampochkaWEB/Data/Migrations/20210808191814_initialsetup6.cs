using Microsoft.EntityFrameworkCore.Migrations;

namespace EvlampochkaWEB.Data.Migrations
{
    public partial class initialsetup6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Comment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LikedItemsId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "LikedItemsId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
