using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acme.BookStore.Migrations
{
    public partial class Added_BookId_To_Favourite_Book : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BookId",
                table: "AppFavouriteBooks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppFavouriteBooks_BookId",
                table: "AppFavouriteBooks",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppFavouriteBooks_AppBooks_BookId",
                table: "AppFavouriteBooks",
                column: "BookId",
                principalTable: "AppBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppFavouriteBooks_AppBooks_BookId",
                table: "AppFavouriteBooks");

            migrationBuilder.DropIndex(
                name: "IX_AppFavouriteBooks_BookId",
                table: "AppFavouriteBooks");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "AppFavouriteBooks");
        }
    }
}
