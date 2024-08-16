using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShopMarket_Web_API.Migrations
{
    /// <inheritdoc />
    public partial class updateresetPasswordTokencoloumninusertabletobenullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "005ac395-8e9f-4aef-98d8-ed9644514079");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "db5101bd-b1f3-4aab-b09e-15c8ddf5b428");

            migrationBuilder.AlterColumn<string>(
                name: "ResetPasswordToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2aeb55ec-5a63-4f10-b509-3293cca9b83a", null, "Admin", "ADMIN" },
                    { "e03bb033-2463-44ec-ac30-8b1d3ccfeeb8", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "2aeb55ec-5a63-4f10-b509-3293cca9b83a");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "e03bb033-2463-44ec-ac30-8b1d3ccfeeb8");

            migrationBuilder.AlterColumn<string>(
                name: "ResetPasswordToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "005ac395-8e9f-4aef-98d8-ed9644514079", null, "Admin", "ADMIN" },
                    { "db5101bd-b1f3-4aab-b09e-15c8ddf5b428", null, "User", "USER" }
                });
        }
    }
}
