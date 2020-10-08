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

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Url")
                        .HasColumnName("url")
                        .HasColumnType("character varying");

                    b.HasKey("Id");

                    b.ToTable("files");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<string>("AdultSafeguardingLeadFirstName")
                        .HasColumnName("adult_safeguarding_lead_first_name")
                        .HasColumnType("character varying");

                    b.Property<string>("AdultSafeguardingLeadLastName")
                        .HasColumnName("adult_safeguarding_lead_last_name")
                        .HasColumnType("character varying");

                    b.Property<string>("AdultSafeguardingLeadTrainingMonth")
                        .HasColumnName("adult_safeguarding_lead_training_month")
                        .HasColumnType("character varying");

                    b.Property<string>("AdultSafeguardingLeadTrainingYear")
                        .HasColumnName("adult_safeguarding_lead_training_year")
                        .HasColumnType("character varying");

                    b.Property<string>("CharityNumber")
                        .HasColumnName("charity_number")
                        .HasColumnType("character varying");

                    b.Property<string>("ChildSafeguardingLeadFirstName")
                        .HasColumnName("child_safeguarding_lead_first_name")
                        .HasColumnType("character varying");

                    b.Property<string>("ChildSafeguardingLeadLastName")
                        .HasColumnName("child_safeguarding_lead_last_name")
                        .HasColumnType("character varying");

                    b.Property<string>("ChildSafeguardingLeadTrainingMonth")
                        .HasColumnName("child_safeguarding_lead_training_month")
                        .HasColumnType("character varying");

                    b.Property<string>("ChildSafeguardingLeadTrainingYear")
                        .HasColumnName("child_safeguarding_lead_training_year")
                        .HasColumnType("character varying");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FundingOther")
                        .HasColumnName("funding_other")
                        .HasColumnType("text");

                    b.Property<bool?>("HasAdultSafeguardingLead")
                        .HasColumnName("has_adult_safeguarding_lead")
                        .HasColumnType("boolean");

                    b.Property<bool?>("HasAdultSupport")
                        .HasColumnName("has_adult_support")
                        .HasColumnType("boolean");

                    b.Property<bool?>("HasChildSafeguardingLead")
                        .HasColumnName("has_child_safeguarding_lead")
                        .HasColumnType("boolean");

                    b.Property<bool?>("HasChildSupport")
                        .HasColumnName("has_child_support")
                        .HasColumnType("boolean");

                    b.Property<bool?>("HasEnhancedSupport")
                        .HasColumnName("has_enhanced_support")
                        .HasColumnType("boolean");

                    b.Property<bool?>("HasHcOrColGrant")
                        .HasColumnName("has_hc_or_col_grant")
                        .HasColumnType("boolean");

                    b.Property<bool?>("HasHcvsOrHgOrAelGrant")
                        .HasColumnName("has_hcvs_or_hg_or_ael_grant")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsHackneyBased")
                        .HasColumnName("is_hackney_based")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsLocalOfferListed")
                        .HasColumnName("is_local_offer_listed")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsLotteryFunded")
                        .HasColumnName("is_lottery_funded")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsRegisteredCharity")
                        .HasColumnName("is_registered_charity")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsTraRegistered")
                        .HasColumnName("is_tra_registered")
                        .HasColumnType("boolean");

                    b.Property<string>("LotteryFundedProject")
                        .HasColumnName("lottery_funded_project")
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

                    b.Property<string>("RslOrHaAssociation")
                        .HasColumnName("rsl_or_ha_association")
                        .HasColumnType("character varying");

                    b.Property<string>("Status")
                        .HasColumnName("status")
                        .HasColumnType("character varying");

                    b.Property<DateTime?>("SubmittedAt")
                        .HasColumnName("submitted_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("updated_at")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ReviewerUid");

                    b.ToTable("organizations");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

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
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("character varying");

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasColumnType("character varying");

                    b.Property<string>("Facebook")
                        .HasColumnName("facebook")
                        .HasColumnType("character varying");

                    b.Property<int?>("ImageId")
                        .HasColumnName("image_id")
                        .HasColumnType("integer");

                    b.Property<string>("Instagram")
                        .HasColumnName("instagram")
                        .HasColumnType("character varying");

                    b.Property<string>("Keywords")
                        .HasColumnName("keywords")
                        .HasColumnType("text");

                    b.Property<string>("Linkedin")
                        .HasColumnName("linkedin")
                        .HasColumnType("character varying");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("character varying");

                    b.Property<int?>("OrganizationId")
                        .HasColumnName("organization_id")
                        .HasColumnType("integer");

                    b.Property<string>("ReferralEmail")
                        .HasColumnName("referral_email")
                        .HasColumnType("character varying");

                    b.Property<string>("ReferralLink")
                        .HasColumnName("referral_link")
                        .HasColumnType("character varying");

                    b.Property<string>("Status")
                        .HasColumnName("status")
                        .HasColumnType("character varying");

                    b.Property<string>("Telephone")
                        .HasColumnName("telephone")
                        .HasColumnType("character varying");

                    b.Property<string>("Twitter")
                        .HasColumnName("twitter")
                        .HasColumnType("character varying");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("updated_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Website")
                        .HasColumnName("website")
                        .HasColumnType("character varying");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("services");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.ServiceLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<string>("Address1")
                        .HasColumnName("address_1")
                        .HasColumnType("character varying");

                    b.Property<string>("Address2")
                        .HasColumnName("address_2")
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

                    b.Property<int?>("ServiceId")
                        .HasColumnName("service_id")
                        .HasColumnType("integer");

                    b.Property<string>("StateProvince")
                        .HasColumnName("state_province")
                        .HasColumnType("character varying");

                    b.Property<string>("Uprn")
                        .HasColumnName("uprn")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId")
                        .HasName("IX_service_locations_revision_id");

                    b.ToTable("service_locations");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.ServiceTaxonomy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("text");

                    b.Property<int>("ServiceId")
                        .HasColumnName("service_id")
                        .HasColumnType("integer");

                    b.Property<int>("TaxonomyId")
                        .HasColumnName("taxonomy_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.HasIndex("TaxonomyId");

                    b.ToTable("service_taxonomies");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("IpAddress")
                        .HasColumnName("ip_address")
                        .HasColumnType("character varying");

                    b.Property<DateTime?>("LastAccessAt")
                        .HasColumnName("last_access_at")
                        .HasColumnType("timestamp with time zone");

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
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

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
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

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
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("character varying");

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
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("OrganizationId")
                        .HasColumnName("organization_id")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("UserId");

                    b.ToTable("user_organizations");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("RoleId")
                        .HasColumnName("role_id")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.HasIndex("Id", "RoleId")
                        .HasName("user_roles_id_role_id_idx");

                    b.ToTable("user_roles");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.Organization", b =>
                {
                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.User", "ReviewerU")
                        .WithMany("Organizations")
                        .HasForeignKey("ReviewerUid")
                        .HasConstraintName("organizations_reviewer_uid_fkey");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.Service", b =>
                {
                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.File", "Image")
                        .WithMany("Services")
                        .HasForeignKey("ImageId")
                        .HasConstraintName("services_image_id_fkey");

                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.Organization", "Organization")
                        .WithMany("Services")
                        .HasForeignKey("OrganizationId")
                        .HasConstraintName("services_organization_id_fkey");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.ServiceLocation", b =>
                {
                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.Service", "Service")
                        .WithMany("ServiceLocations")
                        .HasForeignKey("ServiceId")
                        .HasConstraintName("service_locations_service_id_fkey");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.ServiceTaxonomy", b =>
                {
                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.Service", "Service")
                        .WithMany("ServiceTaxonomies")
                        .HasForeignKey("ServiceId")
                        .HasConstraintName("service_taxonomies_service_id_fkey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.Taxonomy", "Taxonomy")
                        .WithMany("ServiceTaxonomies")
                        .HasForeignKey("TaxonomyId")
                        .HasConstraintName("service_taxonomies_taxonomy_id_fkey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.Organization", "Organization")
                        .WithMany("UserOrganizations")
                        .HasForeignKey("OrganizationId")
                        .HasConstraintName("user_organizations_organization_id_fkey")
                        .IsRequired();

                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.User", "User")
                        .WithMany("UserOrganizations")
                        .HasForeignKey("UserId")
                        .HasConstraintName("user_organizations_user_id_fkey")
                        .IsRequired();
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.UserRole", b =>
                {
                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("user_roles_role_id_fkey");

                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .HasConstraintName("user_roles_user_id_fkey");
                });
#pragma warning restore 612, 618
        }
    }
}
