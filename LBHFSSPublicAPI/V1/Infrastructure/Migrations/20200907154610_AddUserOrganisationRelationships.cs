using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LBHFSSPublicAPI.V1.Infrastructure.Migrations
{
    public partial class AddUserOrganisationRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "user_organizations_id_fkey",
                table: "user_organizations");

            migrationBuilder.AlterColumn<int>(
                name: "organization_id",
                table: "user_organizations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "user_organizations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_access_at",
                table: "sessions",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "sessions",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_organizations_id",
                table: "user_organizations",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_user_organizations_user_id",
                table: "user_organizations",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "user_organizations_user_id_fkey",
                table: "user_organizations",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "user_organizations_user_id_fkey",
                table: "user_organizations");

            migrationBuilder.DropIndex(
                name: "IX_user_organizations_id",
                table: "user_organizations");

            migrationBuilder.DropIndex(
                name: "IX_user_organizations_user_id",
                table: "user_organizations");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "user_organizations");

            migrationBuilder.AlterColumn<int>(
                name: "organization_id",
                table: "user_organizations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_access_at",
                table: "sessions",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "sessions",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "user_organizations_id_fkey",
                table: "user_organizations",
                column: "id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
