using Microsoft.EntityFrameworkCore.Migrations;

namespace LBHFSSPublicAPI.V1.Infrastructure.Migrations
{
    public partial class AddDeleteConstraintsToServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "service_locations_service_id_fkey",
                table: "service_locations");

            migrationBuilder.AddForeignKey(
                name: "service_locations_service_id_fkey",
                table: "service_locations",
                column: "service_id",
                principalTable: "services",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "service_locations_service_id_fkey",
                table: "service_locations");

            migrationBuilder.AddForeignKey(
                name: "service_locations_service_id_fkey",
                table: "service_locations",
                column: "service_id",
                principalTable: "services",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
