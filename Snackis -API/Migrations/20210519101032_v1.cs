using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "root-0c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "5671adaa-d4d8-4bce-9156-9aad2fd9a5f9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "user-2c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "8005669f-2289-4d23-8dfd-5e5c451da10a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e82e1ecb-530a-4152-afdd-729fcb77d6df", "AQAAAAEAACcQAAAAEKe9DkTL/bcvPRijnD4AdoiqGwmzlqnLVFVjkPhb6ppsA46y+txXDlCuxoWS22rEqw==", "c7f0b0d4-47ad-4ef8-a798-e2227d1fcd04" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "root-0c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "0f12207e-b02b-42ce-8cb9-2075185039cc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "user-2c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "ca3e573e-bd76-4ec5-a6e0-d9f0f9377d9c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a0c0f782-017a-4ae6-8f9b-98bd93278be2", "AQAAAAEAACcQAAAAECzFx2mZnMpImyKeRvspOtYwckaEd59GTbjTDEoo/dDsXodECG8TKGpJMgzf3O6V1g==", "dfc1e51c-76fb-4cc1-b86e-a43940c1a199" });
        }
    }
}
