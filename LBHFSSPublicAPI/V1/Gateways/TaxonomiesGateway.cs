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
        public List<TaxonomyEntity> GetTaxonomies()
        {
            var gwResponse = _dbContext.Taxonomies.Select(x => new TaxonomyEntity
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
    }
}
