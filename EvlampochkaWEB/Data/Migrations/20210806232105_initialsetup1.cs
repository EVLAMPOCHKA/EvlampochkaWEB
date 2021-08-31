using Microsoft.EntityFrameworkCore.Migrations;

namespace EvlampochkaWEB.Data.Migrations
{
    public partial class initialsetup1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Collection_CollectionId",
                table: "Item");

            migrationBuilder.AlterColumn<int>(
                name: "CollectionId",
                table: "Item",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Collection_CollectionId",
                table: "Item",
                column: "CollectionId",
                principalTable: "Collection",
                principalColumn: "CollectionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Collection_CollectionId",
                table: "Item");

            migrationBuilder.AlterColumn<int>(
                name: "CollectionId",
                table: "Item",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Collection_CollectionId",
                table: "Item",
                column: "CollectionId",
                principalTable: "Collection",
                principalColumn: "CollectionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
