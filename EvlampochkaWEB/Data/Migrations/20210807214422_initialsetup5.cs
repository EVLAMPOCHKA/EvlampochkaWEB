using Microsoft.EntityFrameworkCore.Migrations;

namespace EvlampochkaWEB.Data.Migrations
{
    public partial class initialsetup5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Item_ItemId",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Comment",
                newName: "ItemID");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ItemId",
                table: "Comment",
                newName: "IX_Comment_ItemID");

            migrationBuilder.AlterColumn<int>(
                name: "ItemID",
                table: "Comment",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Item_ItemID",
                table: "Comment",
                column: "ItemID",
                principalTable: "Item",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Item_ItemID",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "ItemID",
                table: "Comment",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ItemID",
                table: "Comment",
                newName: "IX_Comment_ItemId");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "Comment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Item_ItemId",
                table: "Comment",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
