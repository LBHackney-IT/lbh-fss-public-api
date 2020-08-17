using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LBHFSSPublicAPI.V1.Infrastructure.Migrations
{
    public partial class AddKeyToUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "user_roles_id_fkey",
                table: "user_roles");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "user_roles",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_roles",
                table: "user_roles",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "user_roles_id_fkey",
                table: "user_roles",
                column: "id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "user_roles_id_fkey",
                table: "user_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_roles",
                table: "user_roles");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "user_roles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "name", "status", "sub_id" },
                values: new object[,]
                {
                    { 1, new DateTime(2018, 8, 18, 11, 22, 16, 0, DateTimeKind.Unspecified), "jane.doe@blueyonder.co.uk", "Jane Doe", "active", "cc1b08f5-251c-4152-b367-640411dafd5f" },
                    { 2, new DateTime(2019, 3, 11, 15, 42, 55, 0, DateTimeKind.Unspecified), "mark.williams@bighouse.org", "Mark Williams", "active", "1b86bb42-c1be-40e8-a583-aca0b5df1976" },
                    { 3, new DateTime(2020, 4, 30, 14, 36, 32, 0, DateTimeKind.Unspecified), "janet.graham@grcdentists.co.uk", "Janet Graham", "unverified", "059a2d94-dc90-42b7-bcba-ae453dcf8d76" },
                    { 4, new DateTime(2020, 8, 11, 8, 28, 46, 0, DateTimeKind.Unspecified), "ronnie.osullivan@onefourseven.com", "Ronnie O'Sullivan", "verified", "00a50252-dca9-46fd-80c9-5233b637ef02" },
                    { 5, new DateTime(2020, 8, 13, 11, 46, 19, 0, DateTimeKind.Unspecified), "betty.davis@baesystems.co.uk", "Betty Davis", "verified", "08a88621-f215-436a-912f-64882909c59f" }
                });

            migrationBuilder.AddForeignKey(
                name: "user_roles_id_fkey",
                table: "user_roles",
                column: "id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
