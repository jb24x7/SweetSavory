using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryCatalog.Migrations
{
    public partial class booksCopiesProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Copies",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Copies",
                table: "Books");
        }
    }
}
