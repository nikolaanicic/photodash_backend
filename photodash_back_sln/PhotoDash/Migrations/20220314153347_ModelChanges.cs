using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoDash.Migrations
{
    public partial class ModelChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("a798b7f7-e78c-4f65-808c-84ab531b0ee0"),
                column: "Posted",
                value: new DateTime(2022, 3, 14, 8, 33, 46, 448, DateTimeKind.Local).AddTicks(1876));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("a798b7f7-e78c-4f65-808c-84ab531b0ee0"),
                column: "Posted",
                value: new DateTime(2022, 3, 14, 5, 51, 53, 967, DateTimeKind.Local).AddTicks(1954));
        }
    }
}
