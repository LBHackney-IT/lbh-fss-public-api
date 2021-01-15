using Microsoft.EntityFrameworkCore.Migrations;

namespace LBHFSSPublicAPI.V1.Infrastructure.Migrations
{
    public partial class AddNHSNeighbourhoodToServiceLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "nhs_neighbourhood",
                table: "service_locations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nhs_neighbourhood",
                table: "service_locations");
        }
    }
}
