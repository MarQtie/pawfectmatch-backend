using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawfectMatch.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddTimestampsToAdoptionRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_adoption_requests_users_AdopterId",
                table: "adoption_requests");

            migrationBuilder.RenameColumn(
                name: "AdopterId",
                table: "adoption_requests",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_adoption_requests_AdopterId",
                table: "adoption_requests",
                newName: "IX_adoption_requests_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "adoption_requests",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_adoption_requests_users_UserId",
                table: "adoption_requests",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_adoption_requests_users_UserId",
                table: "adoption_requests");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "adoption_requests",
                newName: "AdopterId");

            migrationBuilder.RenameIndex(
                name: "IX_adoption_requests_UserId",
                table: "adoption_requests",
                newName: "IX_adoption_requests_AdopterId");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "adoption_requests",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_adoption_requests_users_AdopterId",
                table: "adoption_requests",
                column: "AdopterId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
