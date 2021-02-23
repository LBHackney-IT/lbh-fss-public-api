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
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.AnalyticsEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<int>("ServiceId")
                        .HasColumnType("integer")
                        .HasColumnName("service_id");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("timestamp");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.ToTable("service_analytics");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Url")
                        .HasColumnType("character varying")
                        .HasColumnName("url");

                    b.HasKey("Id");

                    b.ToTable("files");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<string>("AdultSafeguardingLeadFirstName")
                        .HasColumnType("character varying")
                        .HasColumnName("adult_safeguarding_lead_first_name");

                    b.Property<string>("AdultSafeguardingLeadLastName")
                        .HasColumnType("character varying")
                        .HasColumnName("adult_safeguarding_lead_last_name");

                    b.Property<string>("AdultSafeguardingLeadTrainingMonth")
                        .HasColumnType("character varying")
                        .HasColumnName("adult_safeguarding_lead_training_month");

                    b.Property<string>("AdultSafeguardingLeadTrainingYear")
                        .HasColumnType("character varying")
                        .HasColumnName("adult_safeguarding_lead_training_year");

                    b.Property<string>("CharityNumber")
                        .HasColumnType("character varying")
                        .HasColumnName("community_interest_company_number");

                    b.Property<string>("ChildSafeguardingLeadFirstName")
                        .HasColumnType("character varying")
                        .HasColumnName("child_safeguarding_lead_first_name");

                    b.Property<string>("ChildSafeguardingLeadLastName")
                        .HasColumnType("character varying")
                        .HasColumnName("child_safeguarding_lead_last_name");

                    b.Property<string>("ChildSafeguardingLeadTrainingMonth")
                        .HasColumnType("character varying")
                        .HasColumnName("child_safeguarding_lead_training_month");

                    b.Property<string>("ChildSafeguardingLeadTrainingYear")
                        .HasColumnType("character varying")
                        .HasColumnName("child_safeguarding_lead_training_year");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("FundingOther")
                        .HasColumnType("text")
                        .HasColumnName("funding_other");

                    b.Property<bool?>("HasAdultSafeguardingLead")
                        .HasColumnType("boolean")
                        .HasColumnName("has_adult_safeguarding_lead");

                    b.Property<bool?>("HasAdultSupport")
                        .HasColumnType("boolean")
                        .HasColumnName("has_adult_support");

                    b.Property<bool?>("HasChildSafeguardingLead")
                        .HasColumnType("boolean")
                        .HasColumnName("has_child_safeguarding_lead");

                    b.Property<bool?>("HasChildSupport")
                        .HasColumnType("boolean")
                        .HasColumnName("has_child_support");

                    b.Property<bool?>("HasEnhancedSupport")
                        .HasColumnType("boolean")
                        .HasColumnName("has_enhanced_support");

                    b.Property<bool?>("HasHcOrColGrant")
                        .HasColumnType("boolean")
                        .HasColumnName("has_hc_or_col_grant");

                    b.Property<bool?>("HasHcvsOrHgOrAelGrant")
                        .HasColumnType("boolean")
                        .HasColumnName("has_hcvs_or_hg_or_ael_grant");

                    b.Property<bool>("InRevalidationProcess")
                        .HasColumnType("boolean")
                        .HasColumnName("in_revalidation_process");

                    b.Property<bool?>("IsHackneyBased")
                        .HasColumnType("boolean")
                        .HasColumnName("is_hackney_based");

                    b.Property<bool?>("IsLocalOfferListed")
                        .HasColumnType("boolean")
                        .HasColumnName("is_local_offer_listed");

                    b.Property<bool?>("IsLotteryFunded")
                        .HasColumnType("boolean")
                        .HasColumnName("is_lottery_funded");

                    b.Property<bool?>("IsRegisteredCharity")
                        .HasColumnType("boolean")
                        .HasColumnName("is_registered_community_interest_company");

                    b.Property<bool?>("IsTraRegistered")
                        .HasColumnType("boolean")
                        .HasColumnName("is_tra_registered");

                    b.Property<DateTime?>("LastRevalidation")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("last_revalidation");

                    b.Property<string>("LotteryFundedProject")
                        .HasColumnType("character varying")
                        .HasColumnName("lottery_funded_project");

                    b.Property<string>("Name")
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.Property<DateTime?>("ReviewedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("reviewed_at");

                    b.Property<string>("ReviewerMessage")
                        .HasColumnType("text")
                        .HasColumnName("reviewer_message");

                    b.Property<int?>("ReviewerUid")
                        .HasColumnType("integer")
                        .HasColumnName("reviewer_uid");

                    b.Property<string>("RslOrHaAssociation")
                        .HasColumnType("character varying")
                        .HasColumnName("rsl_or_ha_association");

                    b.Property<string>("Status")
                        .HasColumnType("character varying")
                        .HasColumnName("status");

                    b.Property<DateTime?>("SubmittedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("submitted_at");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("ReviewerUid");

                    b.ToTable("organizations");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .HasColumnType("character varying")
                        .HasColumnName("description");

                    b.Property<string>("Email")
                        .HasColumnType("character varying")
                        .HasColumnName("email");

                    b.Property<string>("Facebook")
                        .HasColumnType("character varying")
                        .HasColumnName("facebook");

                    b.Property<int?>("ImageId")
                        .HasColumnType("integer")
                        .HasColumnName("image_id");

                    b.Property<string>("Instagram")
                        .HasColumnType("character varying")
                        .HasColumnName("instagram");

                    b.Property<string>("Keywords")
                        .HasColumnType("text")
                        .HasColumnName("keywords");

                    b.Property<string>("Linkedin")
                        .HasColumnType("character varying")
                        .HasColumnName("linkedin");

                    b.Property<string>("Name")
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.Property<int?>("OrganizationId")
                        .HasColumnType("integer")
                        .HasColumnName("organization_id");

                    b.Property<string>("ReferralEmail")
                        .HasColumnType("character varying")
                        .HasColumnName("referral_email");

                    b.Property<string>("ReferralLink")
                        .HasColumnType("character varying")
                        .HasColumnName("referral_link");

                    b.Property<string>("Status")
                        .HasColumnType("character varying")
                        .HasColumnName("status");

                    b.Property<string>("Telephone")
                        .HasColumnType("character varying")
                        .HasColumnName("telephone");

                    b.Property<string>("Twitter")
                        .HasColumnType("character varying")
                        .HasColumnName("twitter");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("Website")
                        .HasColumnType("character varying")
                        .HasColumnName("website");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("services");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.ServiceLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<string>("Address1")
                        .HasColumnType("character varying")
                        .HasColumnName("address_1");

                    b.Property<string>("Address2")
                        .HasColumnType("character varying")
                        .HasColumnName("address_2");

                    b.Property<string>("City")
                        .HasColumnType("character varying")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .HasColumnType("character varying")
                        .HasColumnName("country");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<decimal?>("Latitude")
                        .HasColumnType("numeric")
                        .HasColumnName("latitude");

                    b.Property<decimal?>("Longitude")
                        .HasColumnType("numeric")
                        .HasColumnName("longitude");

                    b.Property<string>("NHSNeighbourhood")
                        .HasColumnType("text")
                        .HasColumnName("nhs_neighbourhood");

                    b.Property<string>("PostalCode")
                        .HasColumnType("character varying")
                        .HasColumnName("postal_code");

                    b.Property<int?>("ServiceId")
                        .HasColumnType("integer")
                        .HasColumnName("service_id");

                    b.Property<string>("StateProvince")
                        .HasColumnType("character varying")
                        .HasColumnName("state_province");

                    b.Property<string>("Uprn")
                        .HasColumnType("text")
                        .HasColumnName("uprn");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId")
                        .HasDatabaseName("IX_service_locations_revision_id");

                    b.ToTable("service_locations");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.ServiceTaxonomy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("ServiceId")
                        .HasColumnType("integer")
                        .HasColumnName("service_id");

                    b.Property<int>("TaxonomyId")
                        .HasColumnType("integer")
                        .HasColumnName("taxonomy_id");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.HasIndex("TaxonomyId");

                    b.ToTable("service_taxonomies");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("IpAddress")
                        .HasColumnType("character varying")
                        .HasColumnName("ip_address");

                    b.Property<DateTime?>("LastAccessAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_access_at");

                    b.Property<string>("Payload")
                        .HasColumnType("text")
                        .HasColumnName("payload");

                    b.Property<string>("UserAgent")
                        .HasColumnType("text")
                        .HasColumnName("user_agent");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("sessions");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.SynonymGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("synonym_groups");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.SynonymWord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<int?>("GroupId")
                        .HasColumnType("integer")
                        .HasColumnName("group_id");

                    b.Property<string>("Word")
                        .HasColumnType("character varying")
                        .HasColumnName("word");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("synonym_words");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.Taxonomy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .HasColumnType("character varying")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.Property<int?>("ParentId")
                        .HasColumnType("integer")
                        .HasColumnName("parent_id");

                    b.Property<string>("Vocabulary")
                        .HasColumnType("character varying")
                        .HasColumnName("vocabulary");

                    b.Property<int>("Weight")
                        .HasColumnType("integer")
                        .HasColumnName("weight");

                    b.HasKey("Id");

                    b.ToTable("taxonomies");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .HasColumnType("character varying")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.Property<string>("Status")
                        .HasColumnType("character varying")
                        .HasColumnName("status");

                    b.Property<string>("SubId")
                        .HasColumnType("character varying")
                        .HasColumnName("sub_id");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.UserOrganization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("OrganizationId")
                        .HasColumnType("integer")
                        .HasColumnName("organization_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

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
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<int?>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.HasIndex("Id", "RoleId")
                        .HasDatabaseName("user_roles_id_role_id_idx");

                    b.ToTable("user_roles");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.AnalyticsEvent", b =>
                {
                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.Service", "Service")
                        .WithMany("ServiceAnalytics")
                        .HasForeignKey("ServiceId")
                        .HasConstraintName("service_analytics_service_id_fkey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.Organization", b =>
                {
                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.User", "ReviewerU")
                        .WithMany("Organizations")
                        .HasForeignKey("ReviewerUid")
                        .HasConstraintName("organizations_reviewer_uid_fkey");

                    b.Navigation("ReviewerU");
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
                        .HasConstraintName("services_organization_id_fkey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Image");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.ServiceLocation", b =>
                {
                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.Service", "Service")
                        .WithMany("ServiceLocations")
                        .HasForeignKey("ServiceId")
                        .HasConstraintName("service_locations_service_id_fkey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Service");
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

                    b.Navigation("Service");

                    b.Navigation("Taxonomy");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.Session", b =>
                {
                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.User", "User")
                        .WithMany("Sessions")
                        .HasForeignKey("UserId")
                        .HasConstraintName("sessions_user_id_fkey");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.SynonymWord", b =>
                {
                    b.HasOne("LBHFSSPublicAPI.V1.Infrastructure.SynonymGroup", "Group")
                        .WithMany("SynonymWords")
                        .HasForeignKey("GroupId")
                        .HasConstraintName("synonym_words_group_id_fkey");

                    b.Navigation("Group");
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

                    b.Navigation("Organization");

                    b.Navigation("User");
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

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.File", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.Organization", b =>
                {
                    b.Navigation("Services");

                    b.Navigation("UserOrganizations");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.Service", b =>
                {
                    b.Navigation("ServiceAnalytics");

                    b.Navigation("ServiceLocations");

                    b.Navigation("ServiceTaxonomies");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.SynonymGroup", b =>
                {
                    b.Navigation("SynonymWords");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.Taxonomy", b =>
                {
                    b.Navigation("ServiceTaxonomies");
                });

            modelBuilder.Entity("LBHFSSPublicAPI.V1.Infrastructure.User", b =>
                {
                    b.Navigation("Organizations");

                    b.Navigation("Sessions");

                    b.Navigation("UserOrganizations");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
