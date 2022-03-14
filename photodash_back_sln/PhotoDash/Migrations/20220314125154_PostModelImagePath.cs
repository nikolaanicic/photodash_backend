using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoDash.Migrations
{
    public partial class PostModelImagePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("a798b7f7-e78c-4f65-808c-84ab531b0ee0"),
                columns: new[] { "ImagePath", "Posted" },
                values: new object[] { "Putanja", new DateTime(2022, 3, 14, 5, 51, 53, 967, DateTimeKind.Local).AddTicks(1954) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Posts");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Posts",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("a798b7f7-e78c-4f65-808c-84ab531b0ee0"),
                columns: new[] { "Image", "Posted" },
                values: new object[] { new byte[] { 0, 0 }, new DateTime(2022, 3, 14, 4, 6, 4, 871, DateTimeKind.Local).AddTicks(6974) });
        }
    }
}
