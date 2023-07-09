using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clearance.Server.Migrations
{
    /// <inheritdoc />
    public partial class m15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DoneDate",
                table: "ClearanceDirections",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ClearanceDirections",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ClearanceDirections_UserId",
                table: "ClearanceDirections",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClearanceDirections_AspNetUsers_UserId",
                table: "ClearanceDirections",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClearanceDirections_AspNetUsers_UserId",
                table: "ClearanceDirections");

            migrationBuilder.DropIndex(
                name: "IX_ClearanceDirections_UserId",
                table: "ClearanceDirections");

            migrationBuilder.DropColumn(
                name: "DoneDate",
                table: "ClearanceDirections");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ClearanceDirections");
        }
    }
}
