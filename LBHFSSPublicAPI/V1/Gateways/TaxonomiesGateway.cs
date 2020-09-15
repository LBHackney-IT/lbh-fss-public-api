using System;
using System.Collections.Generic;
using System.Linq;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.V1.Factories;
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
            // add some exception handling
            return _dbContext.Taxonomies.Where(
                t => string.IsNullOrWhiteSpace(vocabulary) ||
                t.Vocabulary.ToUpper().Contains(vocabulary.ToUpper()))
                .Select(x => x.ToDomain()).ToList();
        }

        public TaxonomyEntity GetTaxonomy(int id)
        {
            // add some exception handling
            return _dbContext.Taxonomies
                .Where(t => t.Id == id)
                .Select(e => e.ToDomain()).FirstOrDefault();
        }
    }
}
