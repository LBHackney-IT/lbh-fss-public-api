using System.Collections.Generic;
using System.Linq;
using LBHFSSPublicAPI.V1.Boundary.Response;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.V1.Infrastructure;

namespace LBHFSSPublicAPI.V1.Factories
{
    public static class TaxonomyFactory
    {
        // More information on this can be found here https://github.com/LBHackney-IT/lbh-base-api/wiki/Factory-object-mappings
        public static TaxonomyResponse ToResponse(this TaxonomyEntity domain)
        {
            return domain == null ? null : new TaxonomyResponse
            {
                Id = domain.Id,
                Name = domain.Name,
                Description = domain.Description,
                Vocabulary = domain.Vocabulary,
                Weight = domain.Weight
            };
        }

        public static TaxonomyResponseList ToResponse(this IEnumerable<TaxonomyEntity> domainList)
        {
            return domainList == null ? new TaxonomyResponseList() : new TaxonomyResponseList
            {
                Taxonomies = domainList.Select(tx => tx.ToResponse()).ToList()
            };
        }

        public static Taxonomy ToEntity(this TaxonomyEntity domain)
        {
            return new Taxonomy
            {
                Id = domain.Id,
                Name = domain.Name,
                Description = domain.Description,
                Vocabulary = domain.Vocabulary,
                Weight = domain.Weight,
                ParentId = domain.ParentId,
                CreatedAt = domain.CreatedAt
            };
        }

        public static TaxonomyEntity ToDomain(this Taxonomy entity)
        {
            return new TaxonomyEntity
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Vocabulary = entity.Vocabulary,
                Weight = entity.Weight,
                ParentId = entity.ParentId,
                CreatedAt = entity.CreatedAt,
                ServiceTaxonomies = entity.ServiceTaxonomies
            };
        }
    }
}
