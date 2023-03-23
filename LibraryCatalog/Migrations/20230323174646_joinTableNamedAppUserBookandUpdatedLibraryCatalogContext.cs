using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryCatalog.Migrations
{
    public partial class joinTableNamedAppUserBookandUpdatedLibraryCatalogContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserBook_AspNetUsers_UserID",
                table: "AppUserBook");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserBook_Books_BookId",
                table: "AppUserBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserBook",
                table: "AppUserBook");

            migrationBuilder.RenameTable(
                name: "AppUserBook",
                newName: "AppUserBooks");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserBook_UserID",
                table: "AppUserBooks",
                newName: "IX_AppUserBooks_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserBook_BookId",
                table: "AppUserBooks",
                newName: "IX_AppUserBooks_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserBooks",
                table: "AppUserBooks",
                column: "AppUserBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserBooks_AspNetUsers_UserID",
                table: "AppUserBooks",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserBooks_Books_BookId",
                table: "AppUserBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserBooks_AspNetUsers_UserID",
                table: "AppUserBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserBooks_Books_BookId",
                table: "AppUserBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserBooks",
                table: "AppUserBooks");

            migrationBuilder.RenameTable(
                name: "AppUserBooks",
                newName: "AppUserBook");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserBooks_UserID",
                table: "AppUserBook",
                newName: "IX_AppUserBook_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserBooks_BookId",
                table: "AppUserBook",
                newName: "IX_AppUserBook_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserBook",
                table: "AppUserBook",
                column: "AppUserBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserBook_AspNetUsers_UserID",
                table: "AppUserBook",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserBook_Books_BookId",
                table: "AppUserBook",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
