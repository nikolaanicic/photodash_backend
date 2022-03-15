using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoDash.Migrations
{
    public partial class DefaultUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("a798b7f7-e78c-4f65-808c-84ab531b0ee0"),
                column: "Posted",
                value: new DateTime(2022, 3, 15, 1, 7, 48, 883, DateTimeKind.Local).AddTicks(3494));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("a798b7f7-e78c-4f65-808c-84ab531b0ee0"),
                column: "Posted",
                value: new DateTime(2022, 3, 14, 8, 33, 46, 448, DateTimeKind.Local).AddTicks(1876));
        }
    }
}
