using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoDash.Migrations
{
    public partial class DefaultAdminUserRoleSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a23d637f-a4fa-4941-a25a-0f198a912837");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d55ed442-b814-46c9-8df5-037508c8ff32");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "F6A4887A-D95E-4835-9DB4-6671CB299AD2", "5d1eb587-7ad8-4444-aa70-c55699996cf7", "Administrator", "ADMINISTRATOR" },
                    { "55f60ab1-d60b-46e5-888a-dec052836214", "7f8de71b-2cb5-4f8e-b3e6-2e83ca8ba6a0", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4E7E18D2-0208-4F4D-86DC-E86492A69806",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "202b9f38-71b6-4cb9-87f5-c769f07bf87d", "AQAAAAEAACcQAAAAEPsDJHGrCqhdTTuOD7B2GbjUsUIz5UbOzy6GIaavlr1FT09uDfbapmhFpx3rVCmtcQ==", "eb25eb2c-ae4f-4c5a-8220-5a6141430daf" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("a798b7f7-e78c-4f65-808c-84ab531b0ee0"),
                column: "Posted",
                value: new DateTime(2022, 3, 16, 22, 17, 25, 479, DateTimeKind.Local).AddTicks(6462));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "F6A4887A-D95E-4835-9DB4-6671CB299AD2", "4E7E18D2-0208-4F4D-86DC-E86492A69806" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55f60ab1-d60b-46e5-888a-dec052836214");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "F6A4887A-D95E-4835-9DB4-6671CB299AD2", "4E7E18D2-0208-4F4D-86DC-E86492A69806" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "F6A4887A-D95E-4835-9DB4-6671CB299AD2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d55ed442-b814-46c9-8df5-037508c8ff32", "2f00d55c-2b12-49c8-8ab8-cf1ab29eb68e", "Administrator", "ADMINISTRATOR" },
                    { "a23d637f-a4fa-4941-a25a-0f198a912837", "03fabf02-02f9-40c1-9329-285bdaf4f2f2", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4E7E18D2-0208-4F4D-86DC-E86492A69806",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "76c62ec0-8561-49a3-8f0d-5d1814dd192d", "AQAAAAEAACcQAAAAEO/AC9uFe8RJIe0Y6qC+xpJXwsYW1eIhORugDDM8gsWzwpSynaUVzeNXHbET/GGeIA==", "796095c0-5f08-47f3-9d5e-67c9bac27c6f" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("a798b7f7-e78c-4f65-808c-84ab531b0ee0"),
                column: "Posted",
                value: new DateTime(2022, 3, 15, 3, 43, 21, 907, DateTimeKind.Local).AddTicks(1292));
        }
    }
}
