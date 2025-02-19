using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCoreTutorial.Migrations.ECommercePlatform
{
    /// <inheritdoc />
    public partial class UserModelUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Username_Email",
                table: "User");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Email",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_Username",
                table: "User");

            migrationBuilder.CreateIndex(
                name: "IX_User_Username_Email",
                table: "User",
                columns: new[] { "Username", "Email" },
                unique: true);
        }
    }
}
