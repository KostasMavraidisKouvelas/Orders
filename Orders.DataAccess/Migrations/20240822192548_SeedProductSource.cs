using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orders.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductSource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductSources",
                columns: new[] { "Id", "Domain", "IsActive" },
                values: new object[] { 1, "https://fakestoreapi.com/products/", true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductSources",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
