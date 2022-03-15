using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoDash.Migrations
{
    public partial class AddingDefaultAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a336a4b-823b-4c5f-be96-f8794731c29b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d430440b-c826-480d-932e-d916282e60a5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d55ed442-b814-46c9-8df5-037508c8ff32", "2f00d55c-2b12-49c8-8ab8-cf1ab29eb68e", "Administrator", "ADMINISTRATOR" },
                    { "a23d637f-a4fa-4941-a25a-0f198a912837", "03fabf02-02f9-40c1-9329-285bdaf4f2f2", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserId", "UserName" },
                values: new object[] { "4E7E18D2-0208-4F4D-86DC-E86492A69806", 0, "76c62ec0-8561-49a3-8f0d-5d1814dd192d", null, false, "Ime", "Prezime", false, null, null, null, "AQAAAAEAACcQAAAAEO/AC9uFe8RJIe0Y6qC+xpJXwsYW1eIhORugDDM8gsWzwpSynaUVzeNXHbET/GGeIA==", null, false, "796095c0-5f08-47f3-9d5e-67c9bac27c6f", false, null, "admin" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("a798b7f7-e78c-4f65-808c-84ab531b0ee0"),
                column: "Posted",
                value: new DateTime(2022, 3, 15, 3, 43, 21, 907, DateTimeKind.Local).AddTicks(1292));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a23d637f-a4fa-4941-a25a-0f198a912837");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d55ed442-b814-46c9-8df5-037508c8ff32");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4E7E18D2-0208-4F4D-86DC-E86492A69806");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1a336a4b-823b-4c5f-be96-f8794731c29b", "6f1b98eb-9430-4071-8049-8484ec7e4dbf", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d430440b-c826-480d-932e-d916282e60a5", "403d45d1-639e-4860-8f0a-c3fce70797af", "User", "USER" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("a798b7f7-e78c-4f65-808c-84ab531b0ee0"),
                column: "Posted",
                value: new DateTime(2022, 3, 15, 2, 46, 43, 947, DateTimeKind.Local).AddTicks(2935));
        }
    }
}
