using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FACTORINGHONOR.PE.DATA.Migrations
{
    public partial class UpdateFees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "FeeReceipts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "TermEffectiveRate",
                table: "FeeReceipts",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalFinalCost",
                table: "FeeReceipts",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalInitialCost",
                table: "FeeReceipts",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "FeeReceipts");

            migrationBuilder.DropColumn(
                name: "TermEffectiveRate",
                table: "FeeReceipts");

            migrationBuilder.DropColumn(
                name: "TotalFinalCost",
                table: "FeeReceipts");

            migrationBuilder.DropColumn(
                name: "TotalInitialCost",
                table: "FeeReceipts");
        }
    }
}
