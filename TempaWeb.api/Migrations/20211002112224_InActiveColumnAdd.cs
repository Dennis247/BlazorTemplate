using Microsoft.EntityFrameworkCore.Migrations;

namespace TempaWeb.api.Migrations
{
    public partial class InActiveColumnAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0207dc4c-2fa4-40c3-9478-6dd35196d3f0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37e1ae8e-1b88-46ab-8ef9-ddd4c3da6b58");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dd2af1c4-f41f-41d0-85d8-40367c88c2bd", "5775e33e-32a0-4983-834d-f3b043a728e1", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ba8c0fe0-db2a-477c-acd4-ba3f81ce831f", "0357b455-6591-419a-8c69-7dc21acb8a27", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba8c0fe0-db2a-477c-acd4-ba3f81ce831f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd2af1c4-f41f-41d0-85d8-40367c88c2bd");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "37e1ae8e-1b88-46ab-8ef9-ddd4c3da6b58", "e0219d88-3e51-4678-8b1d-c4d8c4753df6", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0207dc4c-2fa4-40c3-9478-6dd35196d3f0", "09d425ef-c8b4-4dbd-9103-aa7a71cd7395", "Administrator", "ADMINISTRATOR" });
        }
    }
}
