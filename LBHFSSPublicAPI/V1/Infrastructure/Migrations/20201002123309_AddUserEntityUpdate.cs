using Microsoft.EntityFrameworkCore.Migrations;

namespace LBHFSSPublicAPI.V1.Infrastructure.Migrations
{
    public partial class AddUserEntityUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "service_taxonomies_service_id_fkey",
                table: "service_taxonomies");

            migrationBuilder.DropForeignKey(
                name: "service_taxonomies_taxonomy_id_fkey",
                table: "service_taxonomies");

            migrationBuilder.DropForeignKey(
                name: "user_roles_id_fkey",
                table: "user_roles");

            migrationBuilder.DropForeignKey(
                name: "user_roles_role_id_fkey",
                table: "user_roles");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "user_roles",
                newName: "user_id");

            migrationBuilder.AlterColumn<int>(
                name: "role_id",
                table: "user_roles",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "user_roles",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "taxonomy_id",
                table: "service_taxonomies",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "service_id",
                table: "service_taxonomies",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_user_id",
                table: "user_roles",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "service_taxonomies_service_id_fkey",
                table: "service_taxonomies",
                column: "service_id",
                principalTable: "services",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "service_taxonomies_taxonomy_id_fkey",
                table: "service_taxonomies",
                column: "taxonomy_id",
                principalTable: "taxonomies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "user_roles_role_id_fkey",
                table: "user_roles",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "user_roles_user_id_fkey",
                table: "user_roles",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "service_taxonomies_service_id_fkey",
                table: "service_taxonomies");

            migrationBuilder.DropForeignKey(
                name: "service_taxonomies_taxonomy_id_fkey",
                table: "service_taxonomies");

            migrationBuilder.DropForeignKey(
                name: "user_roles_role_id_fkey",
                table: "user_roles");

            migrationBuilder.DropForeignKey(
                name: "user_roles_user_id_fkey",
                table: "user_roles");

            migrationBuilder.DropIndex(
                name: "IX_user_roles_user_id",
                table: "user_roles");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "user_roles",
                newName: "UserId");

            migrationBuilder.AlterColumn<int>(
                name: "role_id",
                table: "user_roles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "user_roles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "taxonomy_id",
                table: "service_taxonomies",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "service_id",
                table: "service_taxonomies",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "service_taxonomies_service_id_fkey",
                table: "service_taxonomies",
                column: "service_id",
                principalTable: "services",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "service_taxonomies_taxonomy_id_fkey",
                table: "service_taxonomies",
                column: "taxonomy_id",
                principalTable: "taxonomies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "user_roles_id_fkey",
                table: "user_roles",
                column: "id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "user_roles_role_id_fkey",
                table: "user_roles",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
