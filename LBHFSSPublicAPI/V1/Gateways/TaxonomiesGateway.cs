using System;
using System.Collections.Generic;
using System.Linq;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.V1.Gateways.Interfaces;
using LBHFSSPublicAPI.V1.Infrastructure;

namespace LBHFSSPublicAPI.V1.Gateways
{
    public class TaxonomiesGateway : ITaxonomiesGateway
    {
        private readonly DatabaseContext _dbContext;

        public TaxonomiesGateway(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TaxonomyEntity> GetTaxonomies(string vocabulary)
        {
            var gwResponse = _dbContext.Taxonomies.Where(
                t => string.IsNullOrWhiteSpace(vocabulary) ||
                t.Vocabulary.ToUpper().Contains(vocabulary.ToUpper()))
                .Select(x => new TaxonomyEntity
                {
                    Id = x.Id,
                    CreatedAt = x.CreatedAt,
                    Name = x.Name,
                    ParentId = x.ParentId.Value,
                    Vocabulary = x.Vocabulary,
                    Weight = x.Weight
                });
            return gwResponse.ToList();
        }

        public TaxonomyEntity GetTaxonomy(int id)
        {
            var gatewayResult = _dbContext.Taxonomies
                .Where(t => t.Id == id)
                .Select(e => new TaxonomyEntity
                {
                    Id = e.Id,
                    CreatedAt = e.CreatedAt,
                    Name = e.Name,
                    ParentId = e.ParentId.Value,
                    Vocabulary = e.Vocabulary,
                    Weight = e.Weight
                }).FirstOrDefault();

            return gatewayResult;
        }
    }
}
