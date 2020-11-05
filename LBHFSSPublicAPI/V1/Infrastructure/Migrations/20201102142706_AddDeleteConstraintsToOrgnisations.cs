using Microsoft.EntityFrameworkCore.Migrations;

namespace LBHFSSPublicAPI.V1.Infrastructure.Migrations
{
    public partial class AddDeleteConstraintsToOrgnisations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "services_organization_id_fkey",
                table: "services");

            migrationBuilder.AddForeignKey(
                name: "services_organization_id_fkey",
                table: "services",
                column: "organization_id",
                principalTable: "organizations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "services_organization_id_fkey",
                table: "services");

            migrationBuilder.AddForeignKey(
                name: "services_organization_id_fkey",
                table: "services",
                column: "organization_id",
                principalTable: "organizations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
