using System.Collections.Generic;
using LBHFSSPublicAPI.V1.Boundary.Response;

namespace LBHFSSPublicAPI.V1.UseCase.Interfaces
{
    public interface ITaxonomiesUseCase
    {
        TaxonomyResponse ExecuteGet();
    }
}
