using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "root-0c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "6ad63ec7-f0ba-466c-a38a-307c63865347");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "user-2c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "54ff54da-f1f9-4e61-9dc9-d4c6fa003adf");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aa10c997-64b2-4307-8a3b-db6d62c4e2ef", "AQAAAAEAACcQAAAAEDYMXlmApCbhj6ns7tFrHc2q59QYhmYnSgHcjh9Tdl9ztF233RTSUjfGg980RBnS3g==", "cf2c20d1-f08f-4a50-8a25-47b4abfd89c9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "root-0c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "45e92563-3b06-43ea-8507-530692e06766");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "user-2c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "4b098838-4a90-49e0-b560-93034e9ff86f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "43cdb64b-d269-414d-b679-203d72112032", "AQAAAAEAACcQAAAAEA8QODZfi/KBJB8QSvxUHHj6w4Z5htHXQnjmOvI+U8z2fld+HDk85AWVGFdk8aP64Q==", "8bea422f-f33e-48b0-a34d-d72eafb93807" });
        }
    }
}
