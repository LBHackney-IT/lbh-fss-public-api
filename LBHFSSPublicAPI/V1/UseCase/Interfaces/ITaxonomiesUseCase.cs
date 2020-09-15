using System.Collections.Generic;
using LBHFSSPublicAPI.V1.Boundary.Response;
using LBHFSSPublicAPI.V1.Domain;

namespace LBHFSSPublicAPI.V1.UseCase.Interfaces
{
    public interface ITaxonomiesUseCase
    {
        TaxonomyResponseList ExecuteGet(string vocabulary);
        TaxonomyResponse ExecuteGet(int id);
    }
}
