using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clearance.Server.Migrations
{
    /// <inheritdoc />
    public partial class m14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "Clearances");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Clearances",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Clearances");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Clearances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
