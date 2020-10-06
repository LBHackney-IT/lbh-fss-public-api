using System.Collections.Generic;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.V1.Infrastructure;

namespace LBHFSSPublicAPI.V1.Gateways.Interfaces
{
    public interface ITaxonomiesGateway
    {
        List<TaxonomyEntity> GetTaxonomies(string vocabulary);
        TaxonomyEntity GetTaxonomy(int id);
    }
}
