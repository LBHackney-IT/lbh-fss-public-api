using Microsoft.EntityFrameworkCore.Migrations;

namespace LBHFSSPublicAPI.V1.Infrastructure.Migrations
{
    public partial class AddSecondAddressColumnToServiceLocations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "user_roles_role_id_fkey",
                table: "user_roles");

            migrationBuilder.AlterColumn<int>(
                name: "role_id",
                table: "user_roles",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "user_roles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "address_2",
                table: "service_locations",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "user_roles_role_id_fkey",
                table: "user_roles",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "user_roles_role_id_fkey",
                table: "user_roles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "user_roles");

            migrationBuilder.DropColumn(
                name: "address_2",
                table: "service_locations");

            migrationBuilder.AlterColumn<int>(
                name: "role_id",
                table: "user_roles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "user_roles_role_id_fkey",
                table: "user_roles",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
