using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShopMarket_Web_API.Migrations
{
    /// <inheritdoc />
    public partial class updateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "149f2592-8cc9-4575-a45b-9199fe6690ac");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "d16d3b2a-5fab-4a90-bcb4-f9ef3497cefb");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "45a58052-99d8-4563-96b6-b523c33a84ef", null, "Admin", "ADMIN" },
                    { "8099d2e9-7203-47c8-be80-634d8b69d916", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "45a58052-99d8-4563-96b6-b523c33a84ef");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "8099d2e9-7203-47c8-be80-634d8b69d916");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "149f2592-8cc9-4575-a45b-9199fe6690ac", null, "Admin", "ADMIN" },
                    { "d16d3b2a-5fab-4a90-bcb4-f9ef3497cefb", null, "User", "USER" }
                });
        }
    }
}
