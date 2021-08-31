using Microsoft.EntityFrameworkCore.Migrations;

namespace EvlampochkaWEB.Data.Migrations
{
    public partial class initialsetup9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Like_Item_ItemId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_AspNetUsers_UserId1",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_Like_UserId1",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Like");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Like",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Like",
                newName: "ItemID");

            migrationBuilder.RenameIndex(
                name: "IX_Like_ItemId",
                table: "Like",
                newName: "IX_Like_ItemID");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Like",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Like_UserID",
                table: "Like",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Item_ItemID",
                table: "Like",
                column: "ItemID",
                principalTable: "Item",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_AspNetUsers_UserID",
                table: "Like",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Like_Item_ItemID",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_AspNetUsers_UserID",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_Like_UserID",
                table: "Like");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Like",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ItemID",
                table: "Like",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Like_ItemID",
                table: "Like",
                newName: "IX_Like_ItemId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Like",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Like",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Like_UserId1",
                table: "Like",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Item_ItemId",
                table: "Like",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_AspNetUsers_UserId1",
                table: "Like",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
