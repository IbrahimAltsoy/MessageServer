using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Customers",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Customers",
                newName: "NameSurname");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Customers",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "NameSurname",
                table: "Customers",
                newName: "Name");
        }
    }
}
