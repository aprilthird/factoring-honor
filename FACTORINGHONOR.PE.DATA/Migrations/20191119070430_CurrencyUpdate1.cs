using Microsoft.EntityFrameworkCore.Migrations;

namespace FACTORINGHONOR.PE.DATA.Migrations
{
    public partial class CurrencyUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sign",
                table: "CurrencyTypes",
                newName: "Symbol");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Symbol",
                table: "CurrencyTypes",
                newName: "Sign");
        }
    }
}
