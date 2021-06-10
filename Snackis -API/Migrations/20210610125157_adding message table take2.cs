using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class addingmessagetabletake2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MessageReceiver = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_messages_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "root-0c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "40c15715-d7d5-4918-b170-5c181f4f7abd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "user-2c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "e6b6bc40-345b-401f-88da-99b8df984ebe");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "885743a6-993c-4d9a-a89e-3c980cfea0ad", "AQAAAAEAACcQAAAAEJW5LRwtXQ+bLDzgX+U0zG5oamzSQIzLji5fZwk6ZoLR/xzyrL1EIsZXcYk1jXFfuw==", "e5a2d964-e434-4efa-b629-64e4c2843e58" });

            migrationBuilder.CreateIndex(
                name: "IX_messages_UserId",
                table: "messages",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "messages");

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
    }
}
