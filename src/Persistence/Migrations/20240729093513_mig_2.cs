using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "GeneratedDate",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ReportType",
                table: "Reports");

            migrationBuilder.AddColumn<int>(
                name: "CustomerCount",
                table: "Reports",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SmsCount",
                table: "Reports",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Reports",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerCount",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "SmsCount",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Reports");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Reports",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "GeneratedDate",
                table: "Reports",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ReportType",
                table: "Reports",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
