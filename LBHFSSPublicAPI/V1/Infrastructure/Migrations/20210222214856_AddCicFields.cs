using Microsoft.EntityFrameworkCore.Migrations;

namespace LBHFSSPublicAPI.V1.Infrastructure.Migrations
{
    public partial class AddCicFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "is_registered_charity",
                table: "organizations",
                newName: "is_registered_community_interest_company");

            migrationBuilder.RenameColumn(
                name: "charity_number",
                table: "organizations",
                newName: "community_interest_company_number");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "is_registered_community_interest_company",
                table: "organizations",
                newName: "is_registered_charity");

            migrationBuilder.RenameColumn(
                name: "community_interest_company_number",
                table: "organizations",
                newName: "charity_number");
        }
    }
}
