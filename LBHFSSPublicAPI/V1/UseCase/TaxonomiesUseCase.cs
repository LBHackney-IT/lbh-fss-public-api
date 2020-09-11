using System;
using System.Collections.Generic;
using System.Linq;
using LBHFSSPublicAPI.V1.Boundary.Response;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.V1.Factories;
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

        public TaxonomyResponseList ExecuteGet(string vocabulary)
        {
            var response = _gateway.GetTaxonomies(vocabulary);
            return response.ToResponse();
        }

        public TaxonomyResponse ExecuteGet(int id)
        {
            return _gateway.GetTaxonomy(id).ToResponse();
        }
    }
}
