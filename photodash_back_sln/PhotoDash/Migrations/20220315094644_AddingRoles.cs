using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoDash.Migrations
{
    public partial class AddingRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a336a4b-823b-4c5f-be96-f8794731c29b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d430440b-c826-480d-932e-d916282e60a5");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("a798b7f7-e78c-4f65-808c-84ab531b0ee0"),
                column: "Posted",
                value: new DateTime(2022, 3, 15, 1, 7, 48, 883, DateTimeKind.Local).AddTicks(3494));
        }
    }
}
