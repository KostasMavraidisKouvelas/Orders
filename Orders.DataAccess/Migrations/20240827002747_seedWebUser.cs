using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Orders.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedWebUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("41d819ed-0b13-4cc8-9b3c-fab3b977a004"), 0, "d7e...", "User", "admin@example.com", true, null, null, true, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAEAACcQAAAAEJzv...", "1234567890", true, "JZ6X...", false, "admin@example.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "ViewProducts", "WebUser", new Guid("41d819ed-0b13-4cc8-9b3c-fab3b977a004") },
                    { 2, "EditProducts", "WebUser", new Guid("41d819ed-0b13-4cc8-9b3c-fab3b977a004") },
                    { 3, "ViewOrders", "WebUser", new Guid("41d819ed-0b13-4cc8-9b3c-fab3b977a004") },
                    { 4, "EditOrders", "WebUser", new Guid("41d819ed-0b13-4cc8-9b3c-fab3b977a004") },
                    { 5, "ResendInvoice", "WebUser", new Guid("41d819ed-0b13-4cc8-9b3c-fab3b977a004") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("41d819ed-0b13-4cc8-9b3c-fab3b977a004"));
        }
    }
}
