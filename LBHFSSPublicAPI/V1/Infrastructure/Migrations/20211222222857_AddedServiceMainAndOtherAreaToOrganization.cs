using Microsoft.EntityFrameworkCore.Migrations;

namespace LBHFSSPublicAPI.V1.Infrastructure.Migrations
{
    public partial class AddedServiceMainAndOtherAreaToOrganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "service_area_main",
                table: "organizations",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "service_area_other",
                table: "organizations",
                type: "character varying",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "service_area_main",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "service_area_other",
                table: "organizations");
        }
    }
}
