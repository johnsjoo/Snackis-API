using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class Addingmessagetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "root-0c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "6f751f0e-33e1-4b7c-9c5e-abe7e4313ee5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "user-2c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "b81aaad3-59af-4111-b461-fa2d246cf774");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "90a1e773-2fdd-4ac0-87ae-3f36ec3ea09d", "AQAAAAEAACcQAAAAEIu0Hwrw1vtC3WXabJog+qq2qQ3FJ+uuLu5jNzCBNC2zm2dYtNGVDyTO+7WwzIycgg==", "47f649f4-9d2e-42d3-b7f8-b875d9776a48" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "root-0c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "8c49f412-19e5-4b14-8e87-cd3da95e02af");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "user-2c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "b727e391-ec7d-4fa7-823b-0f5b3c046d81");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cde53e65-b069-4832-a875-823eb58cbf1c", "AQAAAAEAACcQAAAAEONDGDFonTSYOw+fFBS7gjdqQS1X5Pco9nsMtcym9dQEh8aw4zZ7RIPtKYQsA8x45w==", "a505a9e7-94e4-437e-8946-1063f7379400" });
        }
    }
}
