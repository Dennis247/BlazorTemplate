using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorTemplate.api.Migrations
{
    public partial class AuditNewUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2b6c2487-5c92-4a78-aa84-39c6d8984c33");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e0f4364f-6b5f-41fc-8c4b-8221c3cde55f");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "10d25e25-86e7-4211-a78e-360a3c9c3239", "cdad8eed-2275-4f66-87cf-4ca0a3426e4a", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b197c6dc-b11e-4ab6-9223-3ca896ce3368", "bcdc3d42-34a1-467e-ab00-c348787f76ad", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "10d25e25-86e7-4211-a78e-360a3c9c3239");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b197c6dc-b11e-4ab6-9223-3ca896ce3368");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "AuditLogs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e0f4364f-6b5f-41fc-8c4b-8221c3cde55f", "4388c495-58b4-4cfe-b874-690278bdbe96", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2b6c2487-5c92-4a78-aa84-39c6d8984c33", "695cb266-c3cc-478b-961b-a6866d36f75f", "Administrator", "ADMINISTRATOR" });
        }
    }
}
