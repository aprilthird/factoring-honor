using Microsoft.EntityFrameworkCore.Migrations;

namespace FACTORINGHONOR.PE.DATA.Migrations
{
    public partial class CurrencyUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sign",
                table: "CurrencyTypes",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sign",
                table: "CurrencyTypes");
        }
    }
}
