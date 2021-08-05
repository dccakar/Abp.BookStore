using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acme.BookStore.Migrations
{
    public partial class Added_AuthorId_To_Favourite_Authors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "AppFavouriteAuthors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppFavouriteAuthors_AuthorId",
                table: "AppFavouriteAuthors",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppFavouriteAuthors_AppAuthors_AuthorId",
                table: "AppFavouriteAuthors",
                column: "AuthorId",
                principalTable: "AppAuthors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppFavouriteAuthors_AppAuthors_AuthorId",
                table: "AppFavouriteAuthors");

            migrationBuilder.DropIndex(
                name: "IX_AppFavouriteAuthors_AuthorId",
                table: "AppFavouriteAuthors");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "AppFavouriteAuthors");
        }
    }
}
