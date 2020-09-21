using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LBHFSSPublicAPI.V1.Infrastructure.Migrations
{
    public partial class MakeIdsAutoIncrement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "service_locations_revision_id_fkey",
                table: "service_locations");

            migrationBuilder.DropForeignKey(
                name: "service_taxonomies_revision_id_fkey",
                table: "service_taxonomies");

            migrationBuilder.DropForeignKey(
                name: "FK_services_service_revisions_RevisionId1",
                table: "services");

            migrationBuilder.DropTable(
                name: "service_revisions");

            migrationBuilder.DropIndex(
                name: "services_revision_id_key",
                table: "services");

            migrationBuilder.DropIndex(
                name: "IX_services_RevisionId1",
                table: "services");

            migrationBuilder.DropIndex(
                name: "IX_service_taxonomies_revision_id",
                table: "service_taxonomies");

            migrationBuilder.DropIndex(
                name: "IX_service_locations_revision_id",
                table: "service_locations");

            migrationBuilder.DropColumn(
                name: "revision_id",
                table: "services");

            migrationBuilder.DropColumn(
                name: "RevisionId1",
                table: "services");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "service_taxonomies");

            migrationBuilder.DropColumn(
                name: "revision_id",
                table: "service_taxonomies");

            migrationBuilder.DropColumn(
                name: "revision_id",
                table: "service_locations");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "users",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "user_roles",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "user_organizations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "taxonomies",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "synonym_words",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "synonym_groups",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "sessions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "services",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "services",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "services",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "facebook",
                table: "services",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "image_id",
                table: "services",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "instagram",
                table: "services",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "keywords",
                table: "services",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "linkedin",
                table: "services",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "services",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "referral_email",
                table: "services",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "referral_link",
                table: "services",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "services",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "telephone",
                table: "services",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "twitter",
                table: "services",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "services",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "website",
                table: "services",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "service_taxonomies",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "service_taxonomies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "service_id",
                table: "service_taxonomies",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "service_locations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "service_id",
                table: "service_locations",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "roles",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "organizations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "adult_safeguarding_lead_first_name",
                table: "organizations",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "adult_safeguarding_lead_last_name",
                table: "organizations",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "adult_safeguarding_lead_training_month",
                table: "organizations",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "adult_safeguarding_lead_training_year",
                table: "organizations",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "charity_number",
                table: "organizations",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "child_safeguarding_lead_first_name",
                table: "organizations",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "child_safeguarding_lead_last_name",
                table: "organizations",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "child_safeguarding_lead_training_month",
                table: "organizations",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "child_safeguarding_lead_training_year",
                table: "organizations",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "funding_other",
                table: "organizations",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "has_adult_safeguarding_lead",
                table: "organizations",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "has_adult_support",
                table: "organizations",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "has_child_safeguarding_lead",
                table: "organizations",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "has_child_support",
                table: "organizations",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "has_enhanced_support",
                table: "organizations",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "has_hc_or_col_grant",
                table: "organizations",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "has_hcvs_or_hg_or_ael_grant",
                table: "organizations",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_hackney_based",
                table: "organizations",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_local_offer_listed",
                table: "organizations",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_lottery_funded",
                table: "organizations",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_registered_charity",
                table: "organizations",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_tra_registered",
                table: "organizations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lottery_funded_project",
                table: "organizations",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "reviewed_at",
                table: "organizations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "reviewer_message",
                table: "organizations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "reviewer_uid",
                table: "organizations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "rsl_or_ha_association",
                table: "organizations",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "organizations",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "submitted_at",
                table: "organizations",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "organizations",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_service_taxonomies",
                table: "service_taxonomies",
                column: "id");

            migrationBuilder.CreateTable(
                name: "files",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    url = table.Column<string>(type: "character varying", nullable: true),
                    created_at = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_files", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_services_image_id",
                table: "services",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "IX_service_taxonomies_service_id",
                table: "service_taxonomies",
                column: "service_id");

            migrationBuilder.CreateIndex(
                name: "IX_service_locations_revision_id",
                table: "service_locations",
                column: "service_id");

            migrationBuilder.CreateIndex(
                name: "IX_organizations_reviewer_uid",
                table: "organizations",
                column: "reviewer_uid");

            migrationBuilder.AddForeignKey(
                name: "organizations_reviewer_uid_fkey",
                table: "organizations",
                column: "reviewer_uid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "service_locations_service_id_fkey",
                table: "service_locations",
                column: "service_id",
                principalTable: "services",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "service_taxonomies_service_id_fkey",
                table: "service_taxonomies",
                column: "service_id",
                principalTable: "services",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "services_image_id_fkey",
                table: "services",
                column: "image_id",
                principalTable: "files",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "organizations_reviewer_uid_fkey",
                table: "organizations");

            migrationBuilder.DropForeignKey(
                name: "service_locations_service_id_fkey",
                table: "service_locations");

            migrationBuilder.DropForeignKey(
                name: "service_taxonomies_service_id_fkey",
                table: "service_taxonomies");

            migrationBuilder.DropForeignKey(
                name: "services_image_id_fkey",
                table: "services");

            migrationBuilder.DropTable(
                name: "files");

            migrationBuilder.DropIndex(
                name: "IX_services_image_id",
                table: "services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_service_taxonomies",
                table: "service_taxonomies");

            migrationBuilder.DropIndex(
                name: "IX_service_taxonomies_service_id",
                table: "service_taxonomies");

            migrationBuilder.DropIndex(
                name: "IX_service_locations_revision_id",
                table: "service_locations");

            migrationBuilder.DropIndex(
                name: "IX_organizations_reviewer_uid",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "description",
                table: "services");

            migrationBuilder.DropColumn(
                name: "email",
                table: "services");

            migrationBuilder.DropColumn(
                name: "facebook",
                table: "services");

            migrationBuilder.DropColumn(
                name: "image_id",
                table: "services");

            migrationBuilder.DropColumn(
                name: "instagram",
                table: "services");

            migrationBuilder.DropColumn(
                name: "keywords",
                table: "services");

            migrationBuilder.DropColumn(
                name: "linkedin",
                table: "services");

            migrationBuilder.DropColumn(
                name: "name",
                table: "services");

            migrationBuilder.DropColumn(
                name: "referral_email",
                table: "services");

            migrationBuilder.DropColumn(
                name: "referral_link",
                table: "services");

            migrationBuilder.DropColumn(
                name: "status",
                table: "services");

            migrationBuilder.DropColumn(
                name: "telephone",
                table: "services");

            migrationBuilder.DropColumn(
                name: "twitter",
                table: "services");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "services");

            migrationBuilder.DropColumn(
                name: "website",
                table: "services");

            migrationBuilder.DropColumn(
                name: "id",
                table: "service_taxonomies");

            migrationBuilder.DropColumn(
                name: "description",
                table: "service_taxonomies");

            migrationBuilder.DropColumn(
                name: "service_id",
                table: "service_taxonomies");

            migrationBuilder.DropColumn(
                name: "service_id",
                table: "service_locations");

            migrationBuilder.DropColumn(
                name: "adult_safeguarding_lead_first_name",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "adult_safeguarding_lead_last_name",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "adult_safeguarding_lead_training_month",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "adult_safeguarding_lead_training_year",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "charity_number",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "child_safeguarding_lead_first_name",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "child_safeguarding_lead_last_name",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "child_safeguarding_lead_training_month",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "child_safeguarding_lead_training_year",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "funding_other",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "has_adult_safeguarding_lead",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "has_adult_support",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "has_child_safeguarding_lead",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "has_child_support",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "has_enhanced_support",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "has_hc_or_col_grant",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "has_hcvs_or_hg_or_ael_grant",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "is_hackney_based",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "is_local_offer_listed",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "is_lottery_funded",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "is_registered_charity",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "is_tra_registered",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "lottery_funded_project",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "reviewed_at",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "reviewer_message",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "reviewer_uid",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "rsl_or_ha_association",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "status",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "submitted_at",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "organizations");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "user_roles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "user_organizations",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "taxonomies",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "synonym_words",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "synonym_groups",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "sessions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "services",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

            migrationBuilder.AddColumn<int>(
                name: "revision_id",
                table: "services",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RevisionId1",
                table: "services",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "service_taxonomies",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "revision_id",
                table: "service_taxonomies",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "service_locations",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

            migrationBuilder.AddColumn<int>(
                name: "revision_id",
                table: "service_locations",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "roles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "organizations",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

            migrationBuilder.CreateTable(
                name: "service_revisions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    author_id = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    description = table.Column<string>(type: "character varying", nullable: true),
                    facebook = table.Column<string>(type: "character varying", nullable: true),
                    instagram = table.Column<string>(type: "character varying", nullable: true),
                    linkedin = table.Column<string>(type: "character varying", nullable: true),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    reviewed_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    reviewer_message = table.Column<string>(type: "text", nullable: true),
                    reviewer_uid = table.Column<int>(type: "integer", nullable: true),
                    service_id = table.Column<int>(type: "integer", nullable: true),
                    status = table.Column<string>(type: "character varying", nullable: true),
                    submitted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    telephone = table.Column<string>(type: "character varying", nullable: true),
                    twitter = table.Column<string>(type: "character varying", nullable: true),
                    website = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_service_revisions", x => x.id);
                    table.ForeignKey(
                        name: "service_revisions_author_id_fkey",
                        column: x => x.author_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "service_revisions_reviewer_uid_fkey",
                        column: x => x.reviewer_uid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "service_revisions_service_id_fkey",
                        column: x => x.service_id,
                        principalTable: "services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "services_revision_id_key",
                table: "services",
                column: "revision_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_services_RevisionId1",
                table: "services",
                column: "RevisionId1");

            migrationBuilder.CreateIndex(
                name: "IX_service_taxonomies_revision_id",
                table: "service_taxonomies",
                column: "revision_id");

            migrationBuilder.CreateIndex(
                name: "IX_service_locations_revision_id",
                table: "service_locations",
                column: "revision_id");

            migrationBuilder.CreateIndex(
                name: "IX_service_revisions_author_id",
                table: "service_revisions",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_service_revisions_reviewer_uid",
                table: "service_revisions",
                column: "reviewer_uid");

            migrationBuilder.CreateIndex(
                name: "IX_service_revisions_service_id",
                table: "service_revisions",
                column: "service_id");

            migrationBuilder.AddForeignKey(
                name: "service_locations_revision_id_fkey",
                table: "service_locations",
                column: "revision_id",
                principalTable: "service_revisions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "service_taxonomies_revision_id_fkey",
                table: "service_taxonomies",
                column: "revision_id",
                principalTable: "service_revisions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_services_service_revisions_RevisionId1",
                table: "services",
                column: "RevisionId1",
                principalTable: "service_revisions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
