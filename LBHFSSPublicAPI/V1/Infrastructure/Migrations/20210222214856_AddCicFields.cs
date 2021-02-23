using Microsoft.EntityFrameworkCore.Migrations;

namespace LBHFSSPublicAPI.V1.Infrastructure.Migrations
{
    public partial class AddCicFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_registered_community_interest_company",
                table: "organizations",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "community_interest_company_number",
                table: "organizations",
                type: "character varying",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_registered_community_interest_company",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "community_interest_company_number",
                table: "organizations");
        }
    }
}
