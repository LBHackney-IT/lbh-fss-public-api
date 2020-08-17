using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LBHFSSPublicAPI.V1.Infrastructure.Migrations
{
    public partial class APIBaseMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    created_at = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    created_at = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "synonym_groups",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    created_at = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_synonym_groups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "taxonomies",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    parent_id = table.Column<int>(nullable: true),
                    vocabulary = table.Column<string>(type: "character varying", nullable: true),
                    created_at = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_taxonomies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sub_id = table.Column<string>(type: "character varying", nullable: true),
                    email = table.Column<string>(type: "character varying", nullable: true),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    created_at = table.Column<DateTime>(nullable: true),
                    status = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "synonym_words",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    word = table.Column<string>(type: "character varying", nullable: true),
                    group_id = table.Column<int>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_synonym_words", x => x.id);
                    table.ForeignKey(
                        name: "synonym_words_group_id_fkey",
                        column: x => x.group_id,
                        principalTable: "synonym_groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sessions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(nullable: true),
                    ip_address = table.Column<string>(type: "character varying", nullable: true),
                    user_agent = table.Column<string>(nullable: true),
                    payload = table.Column<string>(nullable: true),
                    last_access_at = table.Column<DateTime>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sessions", x => x.id);
                    table.ForeignKey(
                        name: "sessions_user_id_fkey",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: true),
                    organization_id = table.Column<int>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "user_organizations_id_fkey",
                        column: x => x.id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "user_organizations_organization_id_fkey",
                        column: x => x.organization_id,
                        principalTable: "organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    id = table.Column<int>(nullable: true),
                    role_id = table.Column<int>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "user_roles_id_fkey",
                        column: x => x.id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "user_roles_role_id_fkey",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: true),
                    revision_id = table.Column<int>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: true),
                    RevisionId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services", x => x.id);
                    table.ForeignKey(
                        name: "services_organization_id_fkey",
                        column: x => x.organization_id,
                        principalTable: "organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "service_revisions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    service_id = table.Column<int>(nullable: true),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    description = table.Column<string>(type: "character varying", nullable: true),
                    website = table.Column<string>(type: "character varying", nullable: true),
                    telephone = table.Column<string>(type: "character varying", nullable: true),
                    facebook = table.Column<string>(type: "character varying", nullable: true),
                    twitter = table.Column<string>(type: "character varying", nullable: true),
                    instagram = table.Column<string>(type: "character varying", nullable: true),
                    linkedin = table.Column<string>(type: "character varying", nullable: true),
                    status = table.Column<string>(type: "character varying", nullable: true),
                    author_id = table.Column<int>(nullable: true),
                    reviewer_uid = table.Column<int>(nullable: true),
                    submitted_at = table.Column<DateTime>(nullable: true),
                    reviewed_at = table.Column<DateTime>(nullable: true),
                    reviewer_message = table.Column<string>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "service_locations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    revision_id = table.Column<int>(nullable: true),
                    latitude = table.Column<decimal>(type: "numeric", nullable: true),
                    longitude = table.Column<decimal>(type: "numeric", nullable: true),
                    uprn = table.Column<int>(nullable: true),
                    address_1 = table.Column<string>(type: "character varying", nullable: true),
                    city = table.Column<string>(type: "character varying", nullable: true),
                    state_province = table.Column<string>(type: "character varying", nullable: true),
                    postal_code = table.Column<string>(type: "character varying", nullable: true),
                    country = table.Column<string>(type: "character varying", nullable: true),
                    created_at = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_service_locations", x => x.id);
                    table.ForeignKey(
                        name: "service_locations_revision_id_fkey",
                        column: x => x.revision_id,
                        principalTable: "service_revisions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "service_taxonomies",
                columns: table => new
                {
                    revision_id = table.Column<int>(nullable: true),
                    taxonomy_id = table.Column<int>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "service_taxonomies_revision_id_fkey",
                        column: x => x.revision_id,
                        principalTable: "service_revisions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "service_taxonomies_taxonomy_id_fkey",
                        column: x => x.taxonomy_id,
                        principalTable: "taxonomies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "name", "status", "sub_id" },
                values: new object[,]
                {
                    { 1, new DateTime(2018, 8, 18, 11, 22, 16, 0, DateTimeKind.Unspecified), "jane.doe@blueyonder.co.uk", "Jane Doe", "active", "cc1b08f5-251c-4152-b367-640411dafd5f" },
                    { 2, new DateTime(2019, 3, 11, 15, 42, 55, 0, DateTimeKind.Unspecified), "mark.williams@bighouse.org", "Mark Williams", "active", "1b86bb42-c1be-40e8-a583-aca0b5df1976" },
                    { 3, new DateTime(2020, 4, 30, 14, 36, 32, 0, DateTimeKind.Unspecified), "janet.graham@grcdentists.co.uk", "Janet Graham", "unverified", "059a2d94-dc90-42b7-bcba-ae453dcf8d76" },
                    { 4, new DateTime(2020, 8, 11, 8, 28, 46, 0, DateTimeKind.Unspecified), "ronnie.osullivan@onefourseven.com", "Ronnie O'Sullivan", "verified", "00a50252-dca9-46fd-80c9-5233b637ef02" },
                    { 5, new DateTime(2020, 8, 13, 11, 46, 19, 0, DateTimeKind.Unspecified), "betty.davis@baesystems.co.uk", "Betty Davis", "verified", "08a88621-f215-436a-912f-64882909c59f" }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_service_taxonomies_revision_id",
                table: "service_taxonomies",
                column: "revision_id");

            migrationBuilder.CreateIndex(
                name: "IX_service_taxonomies_taxonomy_id",
                table: "service_taxonomies",
                column: "taxonomy_id");

            migrationBuilder.CreateIndex(
                name: "IX_services_organization_id",
                table: "services",
                column: "organization_id");

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
                name: "IX_sessions_user_id",
                table: "sessions",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_synonym_words_group_id",
                table: "synonym_words",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_organizations_id",
                table: "user_organizations",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_user_organizations_organization_id",
                table: "user_organizations",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_role_id",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "user_roles_id_role_id_idx",
                table: "user_roles",
                columns: new[] { "id", "role_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_services_service_revisions_RevisionId1",
                table: "services",
                column: "RevisionId1",
                principalTable: "service_revisions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_services_service_revisions_RevisionId1",
                table: "services");

            migrationBuilder.DropTable(
                name: "service_locations");

            migrationBuilder.DropTable(
                name: "service_taxonomies");

            migrationBuilder.DropTable(
                name: "sessions");

            migrationBuilder.DropTable(
                name: "synonym_words");

            migrationBuilder.DropTable(
                name: "user_organizations");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "taxonomies");

            migrationBuilder.DropTable(
                name: "synonym_groups");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "service_revisions");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "services");

            migrationBuilder.DropTable(
                name: "organizations");
        }
    }
}
