using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clearance.Server.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Direction_Id",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Father",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Directions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Direction_Id",
                table: "AspNetUsers",
                column: "Direction_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Directions_Direction_Id",
                table: "AspNetUsers",
                column: "Direction_Id",
                principalTable: "Directions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Directions_Direction_Id",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Directions");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Direction_Id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Direction_Id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Father",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
