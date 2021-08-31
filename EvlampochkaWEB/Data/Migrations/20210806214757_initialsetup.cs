using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EvlampochkaWEB.Data.Migrations
{
    public partial class initialsetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collection",
                columns: table => new
                {
                    CollectionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectionName = table.Column<string>(nullable: true),
                    Theme = table.Column<int>(nullable: false),
                    ShortDiscription = table.Column<string>(nullable: true),
                    LongDiscrioption = table.Column<string>(nullable: true),
                    CollectionImage = table.Column<string>(nullable: true),
                    AddStringField0 = table.Column<string>(nullable: true),
                    AddStringField1 = table.Column<string>(nullable: true),
                    AddStringField2 = table.Column<string>(nullable: true),
                    AddDateTimeField0 = table.Column<string>(nullable: true),
                    AddDateTimeField1 = table.Column<string>(nullable: true),
                    AddDateTimeField2 = table.Column<string>(nullable: true),
                    AddBoolField0 = table.Column<string>(nullable: true),
                    AddBoolField1 = table.Column<string>(nullable: true),
                    AddBoolField2 = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection", x => x.CollectionId);
                    table.ForeignKey(
                        name: "FK_Collection_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(nullable: true),
                    LikesNumber = table.Column<int>(nullable: false),
                    ItemImage = table.Column<string>(nullable: true),
                    Tags = table.Column<string>(nullable: true),
                    AddStringField0 = table.Column<string>(nullable: true),
                    AddStringField1 = table.Column<string>(nullable: true),
                    AddStringField2 = table.Column<string>(nullable: true),
                    AddDateTimeField0 = table.Column<DateTime>(nullable: false),
                    AddDateTimeField1 = table.Column<DateTime>(nullable: false),
                    AddDateTimeField2 = table.Column<DateTime>(nullable: false),
                    AddBoolField0 = table.Column<bool>(nullable: false),
                    AddBoolField1 = table.Column<bool>(nullable: false),
                    AddBoolField2 = table.Column<bool>(nullable: false),
                    CollectionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Item_Collection_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collection",
                        principalColumn: "CollectionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentText = table.Column<string>(nullable: true),
                    DateCreation = table.Column<DateTime>(nullable: false),
                    ItemId = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comment_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collection_UserId",
                table: "Collection",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ItemId",
                table: "Comment",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_CollectionId",
                table: "Item",
                column: "CollectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Collection");
        }
    }
}
