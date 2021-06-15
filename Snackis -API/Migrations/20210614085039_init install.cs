using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class initinstall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "root-0c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "ba30204d-8bde-4881-a522-09008abd03f7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "user-2c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "572fa588-4d6e-4489-9622-825b15b747bc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "77c6b9b1-2c99-4da2-821a-9129d12e95fc", "AQAAAAEAACcQAAAAEAp27VzPiAwXT/hh2Jw3QOXXwMrjT9vsjKD6ccY0sbfszFu/vh/kw+FKbGiVQ2vRIw==", "004e1255-473d-41d6-9912-393b0f29eaf3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "root-0c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "8ff40bed-b9f3-461f-8f05-a1d5d3e0d602");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "user-2c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "0af2142f-cae2-4324-a1c5-08653567ddaa");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "74fa8d5f-9e8c-4b69-aa7d-7a41efb1fe7a", "AQAAAAEAACcQAAAAEIiBtY3jhgW+tg52ru03RuuUz2xWaORlVJJowvqy4pYsn/oFb7s7q/PAU4fFoLMMLg==", "dc4f3fae-41cd-4a82-a54d-5f642033028e" });
        }
    }
}
