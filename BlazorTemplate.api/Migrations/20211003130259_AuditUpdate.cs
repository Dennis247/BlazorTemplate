using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorTemplate.api.Migrations
{
    public partial class AuditUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba8c0fe0-db2a-477c-acd4-ba3f81ce831f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd2af1c4-f41f-41d0-85d8-40367c88c2bd");

            migrationBuilder.AddColumn<string>(
                name: "AreaAccessed",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrowserInfo",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HttpMethod",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReasonForUpdate",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TraceId",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkStation",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e0f4364f-6b5f-41fc-8c4b-8221c3cde55f", "4388c495-58b4-4cfe-b874-690278bdbe96", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2b6c2487-5c92-4a78-aa84-39c6d8984c33", "695cb266-c3cc-478b-961b-a6866d36f75f", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2b6c2487-5c92-4a78-aa84-39c6d8984c33");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e0f4364f-6b5f-41fc-8c4b-8221c3cde55f");

            migrationBuilder.DropColumn(
                name: "AreaAccessed",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "BrowserInfo",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "HttpMethod",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "Ip",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "ReasonForUpdate",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "TraceId",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "WorkStation",
                table: "AuditLogs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dd2af1c4-f41f-41d0-85d8-40367c88c2bd", "5775e33e-32a0-4983-834d-f3b043a728e1", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ba8c0fe0-db2a-477c-acd4-ba3f81ce831f", "0357b455-6591-419a-8c69-7dc21acb8a27", "Administrator", "ADMINISTRATOR" });
        }
    }
}
