using Microsoft.EntityFrameworkCore.Migrations;

namespace EvlampochkaWEB.Data.Migrations
{
    public partial class initialsetup2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Collection_CollectionId",
                table: "Item");

            migrationBuilder.RenameColumn(
                name: "CollectionId",
                table: "Item",
                newName: "CollectionID");

            migrationBuilder.RenameIndex(
                name: "IX_Item_CollectionId",
                table: "Item",
                newName: "IX_Item_CollectionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Collection_CollectionID",
                table: "Item",
                column: "CollectionID",
                principalTable: "Collection",
                principalColumn: "CollectionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Collection_CollectionID",
                table: "Item");

            migrationBuilder.RenameColumn(
                name: "CollectionID",
                table: "Item",
                newName: "CollectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Item_CollectionID",
                table: "Item",
                newName: "IX_Item_CollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Collection_CollectionId",
                table: "Item",
                column: "CollectionId",
                principalTable: "Collection",
                principalColumn: "CollectionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
