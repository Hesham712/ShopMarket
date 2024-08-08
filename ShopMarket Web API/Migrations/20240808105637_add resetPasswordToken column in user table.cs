using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShopMarket_Web_API.Migrations
{
    /// <inheritdoc />
    public partial class addresetPasswordTokencolumninusertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "274a4daf-fd0d-41dc-be23-da457b45c1c2");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "fa27348e-be87-4b6f-94d8-1aa2929c7e8f");

            migrationBuilder.AddColumn<string>(
                name: "ResetPasswordToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "005ac395-8e9f-4aef-98d8-ed9644514079", null, "Admin", "ADMIN" },
                    { "db5101bd-b1f3-4aab-b09e-15c8ddf5b428", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "005ac395-8e9f-4aef-98d8-ed9644514079");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "db5101bd-b1f3-4aab-b09e-15c8ddf5b428");

            migrationBuilder.DropColumn(
                name: "ResetPasswordToken",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "274a4daf-fd0d-41dc-be23-da457b45c1c2", null, "User", "USER" },
                    { "fa27348e-be87-4b6f-94d8-1aa2929c7e8f", null, "Admin", "ADMIN" }
                });
        }
    }
}
