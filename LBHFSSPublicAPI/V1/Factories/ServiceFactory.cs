using System;
using System.Collections.Generic;
using System.Linq;
using LBHFSSPublicAPI.V1.Boundary.Response;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.V1.Infrastructure;
using org = LBHFSSPublicAPI.V1.Boundary.Response;
using Organization = LBHFSSPublicAPI.V1.Infrastructure.Organization;

namespace LBHFSSPublicAPI.V1.Factories
{
    public static class ServiceFactory
    {
        // More information on this can be found here https://github.com/LBHackney-IT/lbh-base-api/wiki/Factory-object-mappings
        public static GetServiceResponse ToResponse(this ServiceEntity domain)
        {
            if (domain == null)
                return null;
            var response = domain == null ? null : new GetServiceResponse
            {
                Id = domain.Id,
                Name = domain.Name,
                Categories = domain.ServiceTaxonomies == null
                    ? new List<Category>()
                    : domain.ServiceTaxonomies
                        .Where(x => x.Taxonomy != null && x.Taxonomy.Vocabulary == "category")
                        .Select(x => new Category
                        {
                            Id = x.Taxonomy.Id,
                            Name = x.Taxonomy.Name,
                            Description = x.Taxonomy.Description,
                            Vocabulary = x.Taxonomy.Vocabulary,
                            Weight = x.Taxonomy.Weight
                        }).ToList(),
                Contact = new Contact
                {
                    Email = domain.Email,
                    Telephone = domain.Telephone,
                    Website = domain.Website
                },
                Demographic = domain.ServiceTaxonomies == null
                    ? new List<Demographic>()
                    : domain.ServiceTaxonomies
                        .Where(x => x.Taxonomy.Vocabulary == "demographic")
                        .Select(x => new Demographic
                        {
                            Id = x.Taxonomy.Id, Name = x.Taxonomy.Name, Vocabulary = x.Taxonomy.Vocabulary,
                        }).ToList(),
                Description = domain.Description,
                Images = new Image
                {
                    // TODO:  We need to get the resized image uri for this property
                    Medium = "new_uri_to_be_provided",
                    Original = domain.Image.Url
                },
                Locations = domain.ServiceLocations
                    .Select(x => new Location
                    {
                        Latitude = x.Latitude,
                        Longitude = x.Longitude,
                        //check if this is a string or integer (does it have preceding 0 or alpa characters)
                        Uprn = x.Uprn.ToString(),
                        Address1 = x.Address1,
                        Address2 = x.Address2,
                        City = x.City,
                        StateProvince = x.StateProvince,
                        PostalCode = x.PostalCode,
                        Country = x.Country
                    }).ToList(),
                    Organization = new org.Organization
                    {
                        Id = domain.Organization.Id,
                        Name = domain.Organization.Name,
                        Status = domain.Organization.Status
                    },
                    Referral = new Referral
                    {
                        Email = domain.Email,
                        Website = domain.Website
                    },
                    Social = new Social
                    {
                        Facebook = domain.Facebook,
                        Twitter = domain.Twitter,
                        Instagram = domain.Instagram,
                        Linkedin = domain.Linkedin
                    },
                    Status = domain.Status
            };
            return response;
        }

        public static ServiceEntity ToDomain(this Service entity)
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
    }
}
