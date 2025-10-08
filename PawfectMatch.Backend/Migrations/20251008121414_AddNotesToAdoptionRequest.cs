using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawfectMatch.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddNotesToAdoptionRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "adoption_requests");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "adoption_requests",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "adoption_requests",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "adoption_requests");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "adoption_requests",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "adoption_requests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
