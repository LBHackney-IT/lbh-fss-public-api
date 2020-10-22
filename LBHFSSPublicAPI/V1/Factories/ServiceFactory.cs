using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LBHFSSPublicAPI.V1.Boundary.Response;
using Response = LBHFSSPublicAPI.V1.Boundary.Response;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.V1.Infrastructure;
using Entity = LBHFSSPublicAPI.V1.Infrastructure;

namespace LBHFSSPublicAPI.V1.Factories
{
    public static class ServiceFactory
    {
        #region Domain to Response - Main objects

        public static GetServiceResponse ToResponse(this ServiceEntity serviceDomain)
        {
            if (serviceDomain != null)
                return new GetServiceResponse
                {
                    Service = serviceDomain.ToResponseService(),
                    Metadata = new Metadata
                    {
                        PostCode = null,
                        PostCodeLatitude = null,
                        PostCodeLongitude = null,
                        Error = null
                    }
                };
            else
                return null;
        }

        public static GetServiceResponseList ToResponse(this ICollection<ServiceEntity> serviceDomainList)
        {
            if (serviceDomainList != null)
                return new GetServiceResponseList()
                {
                    Services = serviceDomainList
                        .Select(s => s.ToResponseService())
                        .ToList(),
                    Metadata = new Metadata
                    {
                        PostCode = null,
                        PostCodeLatitude = null,
                        PostCodeLongitude = null,
                        Error = null
                    }
                };
            else
                return new GetServiceResponseList();
        }

        #endregion

        #region Domain to Response - Inner objects

        public static Response.Service ToResponseService(this ServiceEntity serviceDomain)
        {
            return new Response.Service
            {
                Id = serviceDomain.Id,

                Name = serviceDomain.Name,

                Categories = serviceDomain.ServiceTaxonomies.ToResponseCategoryList(),

                Contact = serviceDomain.ToResponseContact(),

                Demographic = serviceDomain.ServiceTaxonomies.ToResponseDemographicList(),

                Description = serviceDomain.Description,

                Images = serviceDomain.ToResponseImage(),

                Locations = serviceDomain.ServiceLocations.ToResponseServiceLocationList(),

                Organization = serviceDomain.Organization.ToResponseOrganization(),

                Referral = serviceDomain.ToResponseReferral(),

                Social = serviceDomain.ToResponseSocial(),

                Status = serviceDomain.Status
            };
        }

        private static List<Category> ToResponseCategoryList(this ICollection<ServiceTaxonomy> serviceTaxonomies)
        {
            if (serviceTaxonomies != null)
                return serviceTaxonomies
                    .Where(
                        st => st.Taxonomy != null &&
                        st.Taxonomy.Vocabulary == "category"
                    )
                    .Select(
                        st => new Category
                        {
                            Id = st.Taxonomy.Id,
                            Name = st.Taxonomy.Name,
                            Description = st.Taxonomy.Description,
                            Vocabulary = st.Taxonomy.Vocabulary,
                            Weight = st.Taxonomy.Weight
                        })
                    .ToList();
            else
                return new List<Category>();
        }

        private static Contact ToResponseContact(this ServiceEntity serviceDomain)
        {
            return
                new Contact
                {
                    Email = serviceDomain.Email,
                    Telephone = serviceDomain.Telephone,
                    Website = serviceDomain.Website
                };
        }

        private static List<Demographic> ToResponseDemographicList(this ICollection<ServiceTaxonomy> serviceTaxonomies)
        {
            if (serviceTaxonomies != null)
                return serviceTaxonomies
                    .Where(
                        st => st.Taxonomy != null &&
                        st.Taxonomy.Vocabulary == "demographic"
                    )
                    .Select(st => new Demographic
                    {
                        Id = st.Taxonomy.Id,
                        Name = st.Taxonomy.Name,
                        Vocabulary = st.Taxonomy.Vocabulary,
                    })
                    .ToList();
            else
                return new List<Demographic>();
        }

        private static Image ToResponseImage(this ServiceEntity serviceDomain)
        {
            return
                new Image
                {
                    // TODO:  We need to get the resized image uri for this property
                    Medium = "new_uri_to_be_provided",
                    Original = serviceDomain.Image?.Url
                };
        }

        private static List<Location> ToResponseServiceLocationList(this ICollection<ServiceLocation> serviceLocations)
        {
            return serviceLocations
                ?.Select(x => new Location
                {
                    Latitude = (double?) x.Latitude,
                    Longitude = (double?) x.Longitude,
                    //check if this is a string or integer (does it have preceding 0 or alpa characters)
                    Uprn = x.Uprn.ToString(),
                    Address1 = x.Address1,
                    Address2 = x.Address2,
                    City = x.City,
                    StateProvince = x.StateProvince,
                    PostalCode = x.PostalCode,
                    Country = x.Country,
                    Distance = null
                }).ToList();
        }

        private static Response.Organization ToResponseOrganization(this Entity.Organization organization) // yes! it's entity within a domain object
        {
            return
                new Response.Organization
                {
                    Id = organization.Id,
                    Name = organization.Name,
                    Status = organization.Status
                };
        }

        private static Referral ToResponseReferral(this ServiceEntity serviceDomain)
        {
            return
                new Referral
                {
                    Email = serviceDomain.ReferralEmail,
                    Website = serviceDomain.ReferralLink
                };
        }

        private static Social ToResponseSocial(this ServiceEntity serviceDomain)
        {
            return
                new Social
                {
                    Facebook = serviceDomain.Facebook,
                    Twitter = serviceDomain.Twitter,
                    Instagram = serviceDomain.Instagram,
                    Linkedin = serviceDomain.Linkedin
                };
        }

        #endregion

        #region Entity to Domain

        public static ServiceEntity ToDomain(this Entity.Service entity)
        {
            if (entity == null)
                return null;
            return new ServiceEntity
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                Telephone = entity.Telephone,
                Description = entity.Description,
                Image = entity.Image,
                ImageId = entity.ImageId,
                ServiceLocations = entity.ServiceLocations,
                Organization = entity.Organization,
                OrganizationId = entity.OrganizationId,
                Website = entity.Website,
                Facebook = entity.Facebook,
                Twitter = entity.Twitter,
                Instagram = entity.Instagram,
                Linkedin = entity.Linkedin,
                Status = entity.Status,
                ServiceTaxonomies = entity.ServiceTaxonomies,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                Keywords = entity.Keywords,
                ReferralEmail = entity.ReferralEmail,
                ReferralLink = entity.ReferralLink
            };
        }

        public static ICollection<ServiceEntity> ToDomain(this ICollection<Entity.Service> entities)
        {
            if (entities == null)
                return new List<ServiceEntity>();
            return entities.Select(e => e.ToDomain()).ToList();
        }

        #endregion
    }
}
