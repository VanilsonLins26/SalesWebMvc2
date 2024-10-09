using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesWebMvc.Migrations
{
    /// <inheritdoc />
    public partial class DepartmentForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Seller",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Seller",
                newName: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Seller",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Seller",
                newName: "email");
        }
    }
}
