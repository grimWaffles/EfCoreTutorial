using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCoreTutorial.Migrations.ecommerce
{
    /// <inheritdoc />
    public partial class AccSummaryIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AccountSummaryHistories_TransactionDate_UserId",
                table: "AccountSummaryHistories",
                columns: new[] { "TransactionDate", "UserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AccountSummaryHistories_TransactionDate_UserId",
                table: "AccountSummaryHistories");
        }
    }
}
