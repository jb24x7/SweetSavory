using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryCatalog.Migrations
{
    public partial class booksRowtoApplicationUserAndFKUserIdtoBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Books",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Books_UserID",
                table: "Books",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_UserID",
                table: "Books",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_UserID",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_UserID",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Books");
        }
    }
}
