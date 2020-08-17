using Microsoft.EntityFrameworkCore.Migrations;

namespace LBHFSSPublicAPI.V1.Infrastructure.Migrations
{
    public partial class AddKeyToUserOrganizations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "user_organizations_id_fkey",
                table: "user_organizations");

            migrationBuilder.DropIndex(
                name: "IX_user_organizations_id",
                table: "user_organizations");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "user_organizations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_organizations",
                table: "user_organizations",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "user_organizations_id_fkey",
                table: "user_organizations",
                column: "id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "user_organizations_id_fkey",
                table: "user_organizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_organizations",
                table: "user_organizations");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "user_organizations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_user_organizations_id",
                table: "user_organizations",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "user_organizations_id_fkey",
                table: "user_organizations",
                column: "id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
