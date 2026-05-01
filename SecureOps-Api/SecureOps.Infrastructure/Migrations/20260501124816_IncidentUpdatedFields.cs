using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecureOps.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IncidentUpdatedFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Incidents",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Incidents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_UpdatedById",
                table: "Incidents",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_AspNetUsers_UpdatedById",
                table: "Incidents",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_AspNetUsers_UpdatedById",
                table: "Incidents");

            migrationBuilder.DropIndex(
                name: "IX_Incidents_UpdatedById",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Incidents");
        }
    }
}
