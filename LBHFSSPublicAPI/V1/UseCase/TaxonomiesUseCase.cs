using System.Collections.Generic;
using LBHFSSPublicAPI.V1.Boundary.Response;
using LBHFSSPublicAPI.V1.Gateways.Interfaces;
using LBHFSSPublicAPI.V1.UseCase.Interfaces;

namespace LBHFSSPublicAPI.V1.UseCase
{
    public class TaxonomiesUseCase : ITaxonomiesUseCase
    {
        private readonly ITaxonomiesGateway _gateway;

        public TaxonomiesUseCase(ITaxonomiesGateway gateway)
        {
            _gateway = gateway;
        }
        public TaxonomyResponse ExecuteGet(string vocabulary)
        {
            var response = _gateway.GetTaxonomies(vocabulary);
            return new TaxonomyResponse { Taxonomies = response };
        }
    }
}
