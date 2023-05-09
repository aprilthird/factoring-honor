using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FACTORINGHONOR.PE.DATA.Migrations
{
    public partial class Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CurrencyTypes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "CurrencyTypes",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Banks",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "Banks",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyTypeId",
                table: "BankCosts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BankCosts_CurrencyTypeId",
                table: "BankCosts",
                column: "CurrencyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankCosts_CurrencyTypes_CurrencyTypeId",
                table: "BankCosts",
                column: "CurrencyTypeId",
                principalTable: "CurrencyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankCosts_CurrencyTypes_CurrencyTypeId",
                table: "BankCosts");

            migrationBuilder.DropIndex(
                name: "IX_BankCosts_CurrencyTypeId",
                table: "BankCosts");

            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "CurrencyTypes");

            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "CurrencyTypeId",
                table: "BankCosts");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CurrencyTypes",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Banks",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
