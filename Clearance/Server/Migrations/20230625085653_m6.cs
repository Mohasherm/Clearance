using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clearance.Server.Migrations
{
    public partial class m6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ClearanceDirections_ClearanceId",
                table: "ClearanceDirections",
                column: "ClearanceId");

            migrationBuilder.CreateIndex(
                name: "IX_ClearanceDirections_DirectionId",
                table: "ClearanceDirections",
                column: "DirectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClearanceDirections_Clearances_ClearanceId",
                table: "ClearanceDirections",
                column: "ClearanceId",
                principalTable: "Clearances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClearanceDirections_Directions_DirectionId",
                table: "ClearanceDirections",
                column: "DirectionId",
                principalTable: "Directions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClearanceDirections_Clearances_ClearanceId",
                table: "ClearanceDirections");

            migrationBuilder.DropForeignKey(
                name: "FK_ClearanceDirections_Directions_DirectionId",
                table: "ClearanceDirections");

            migrationBuilder.DropIndex(
                name: "IX_ClearanceDirections_ClearanceId",
                table: "ClearanceDirections");

            migrationBuilder.DropIndex(
                name: "IX_ClearanceDirections_DirectionId",
                table: "ClearanceDirections");
        }
    }
}
