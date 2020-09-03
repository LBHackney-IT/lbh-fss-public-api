﻿// <auto-generated />
using System;
using LBHFSSPublicAPI.V1.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LBHFSSPublicAPI.V1.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("character varying");

                    b.HasKey("Id");

                    b.ToTable("organizations");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("character varying");

                    b.HasKey("Id");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("OrganizationId")
                        .HasColumnName("organization_id")
                        .HasColumnType("integer");

                    b.Property<int>("RevisionId")
                        .HasColumnName("revision_id")
                        .HasColumnType("integer");

                    b.Property<int?>("RevisionId1")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("RevisionId")
                        .IsUnique()
                        .HasName("services_revision_id_key");

                    b.HasIndex("RevisionId1");

                    b.ToTable("services");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.ServiceLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address1")
                        .HasColumnName("address_1")
                        .HasColumnType("character varying");

                    b.Property<string>("City")
                        .HasColumnName("city")
                        .HasColumnType("character varying");

                    b.Property<string>("Country")
                        .HasColumnName("country")
                        .HasColumnType("character varying");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal?>("Latitude")
                        .HasColumnName("latitude")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("Longitude")
                        .HasColumnName("longitude")
                        .HasColumnType("numeric");

                    b.Property<string>("PostalCode")
                        .HasColumnName("postal_code")
                        .HasColumnType("character varying");

                    b.Property<int?>("RevisionId")
                        .HasColumnName("revision_id")
                        .HasColumnType("integer");

                    b.Property<string>("StateProvince")
                        .HasColumnName("state_province")
                        .HasColumnType("character varying");

                    b.Property<int?>("Uprn")
                        .HasColumnName("uprn")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RevisionId");

                    b.ToTable("service_locations");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.ServiceRevision", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("AuthorId")
                        .HasColumnName("author_id")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("character varying");

                    b.Property<string>("Facebook")
                        .HasColumnName("facebook")
                        .HasColumnType("character varying");

                    b.Property<string>("Instagram")
                        .HasColumnName("instagram")
                        .HasColumnType("character varying");

                    b.Property<string>("Linkedin")
                        .HasColumnName("linkedin")
                        .HasColumnType("character varying");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("character varying");

                    b.Property<DateTime?>("ReviewedAt")
                        .HasColumnName("reviewed_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ReviewerMessage")
                        .HasColumnName("reviewer_message")
                        .HasColumnType("text");

                    b.Property<int?>("ReviewerUid")
                        .HasColumnName("reviewer_uid")
                        .HasColumnType("integer");

                    b.Property<int?>("ServiceId")
                        .HasColumnName("service_id")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .HasColumnName("status")
                        .HasColumnType("character varying");

                    b.Property<DateTime?>("SubmittedAt")
                        .HasColumnName("submitted_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Telephone")
                        .HasColumnName("telephone")
                        .HasColumnType("character varying");

                    b.Property<string>("Twitter")
                        .HasColumnName("twitter")
                        .HasColumnType("character varying");

                    b.Property<string>("Website")
                        .HasColumnName("website")
                        .HasColumnType("character varying");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ReviewerUid");

                    b.HasIndex("ServiceId");

                    b.ToTable("service_revisions");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.ServiceTaxonomy", b =>
                {
                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("RevisionId")
                        .HasColumnName("revision_id")
                        .HasColumnType("integer");

                    b.Property<int?>("TaxonomyId")
                        .HasColumnName("taxonomy_id")
                        .HasColumnType("integer");

                    b.HasIndex("RevisionId");

                    b.HasIndex("TaxonomyId");

                    b.ToTable("service_taxonomies");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("IpAddress")
                        .HasColumnName("ip_address")
                        .HasColumnType("character varying");

                    b.Property<DateTime?>("LastAccessAt")
                        .HasColumnName("last_access_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Payload")
                        .HasColumnName("payload")
                        .HasColumnType("text");

                    b.Property<string>("UserAgent")
                        .HasColumnName("user_agent")
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("sessions");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.SynonymGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("character varying");

                    b.HasKey("Id");

                    b.ToTable("synonym_groups");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.SynonymWord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("GroupId")
                        .HasColumnName("group_id")
                        .HasColumnType("integer");

                    b.Property<string>("Word")
                        .HasColumnName("word")
                        .HasColumnType("character varying");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("synonym_words");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.Taxonomy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("character varying");

                    b.Property<int?>("ParentId")
                        .HasColumnName("parent_id")
                        .HasColumnType("integer");

                    b.Property<string>("Vocabulary")
                        .HasColumnName("vocabulary")
                        .HasColumnType("character varying");

                    b.Property<int>("Weight")
                        .HasColumnName("weight")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("taxonomies");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasColumnType("character varying");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("character varying");

                    b.Property<string>("Status")
                        .HasColumnName("status")
                        .HasColumnType("character varying");

                    b.Property<string>("SubId")
                        .HasColumnName("sub_id")
                        .HasColumnType("character varying");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.UserOrganization", b =>
                {
                    b.Property<int?>("Id")
                        .HasColumnName("id")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("OrganizationId")
                        .HasColumnName("organization_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("user_organizations");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.UserRole", b =>
                {
                    b.Property<int?>("Id")
                        .HasColumnName("id")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("RoleId")
                        .HasColumnName("role_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("Id", "RoleId")
                        .HasName("user_roles_id_role_id_idx");

                    b.ToTable("user_roles");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.Service", b =>
                {
                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.Organization", "Organization")
                        .WithMany("Services")
                        .HasForeignKey("OrganizationId")
                        .HasConstraintName("services_organization_id_fkey");

                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.ServiceRevision", "Revision")
                        .WithMany()
                        .HasForeignKey("RevisionId1");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.ServiceLocation", b =>
                {
                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.ServiceRevision", "Revision")
                        .WithMany("ServiceLocations")
                        .HasForeignKey("RevisionId")
                        .HasConstraintName("service_locations_revision_id_fkey");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.ServiceRevision", b =>
                {
                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.User", "Author")
                        .WithMany("ServiceRevisionsAuthor")
                        .HasForeignKey("AuthorId")
                        .HasConstraintName("service_revisions_author_id_fkey");

                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.User", "ReviewerU")
                        .WithMany("ServiceRevisionsReviewerU")
                        .HasForeignKey("ReviewerUid")
                        .HasConstraintName("service_revisions_reviewer_uid_fkey");

                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.Service", "Service")
                        .WithMany("ServiceRevisions")
                        .HasForeignKey("ServiceId")
                        .HasConstraintName("service_revisions_service_id_fkey");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.ServiceTaxonomy", b =>
                {
                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.ServiceRevision", "Revision")
                        .WithMany()
                        .HasForeignKey("RevisionId")
                        .HasConstraintName("service_taxonomies_revision_id_fkey");

                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.Taxonomy", "Taxonomy")
                        .WithMany()
                        .HasForeignKey("TaxonomyId")
                        .HasConstraintName("service_taxonomies_taxonomy_id_fkey");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.Session", b =>
                {
                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.User", "User")
                        .WithMany("Sessions")
                        .HasForeignKey("UserId")
                        .HasConstraintName("sessions_user_id_fkey");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.SynonymWord", b =>
                {
                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.SynonymGroup", "Group")
                        .WithMany("SynonymWords")
                        .HasForeignKey("GroupId")
                        .HasConstraintName("synonym_words_group_id_fkey");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.UserOrganization", b =>
                {
                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.User", "IdNavigation")
                        .WithMany()
                        .HasForeignKey("Id")
                        .HasConstraintName("user_organizations_id_fkey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .HasConstraintName("user_organizations_organization_id_fkey");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.UserRole", b =>
                {
                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.User", "IdNavigation")
                        .WithMany()
                        .HasForeignKey("Id")
                        .HasConstraintName("user_roles_id_fkey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("user_roles_role_id_fkey");
                });
#pragma warning restore 612, 618
        }
    }
}
