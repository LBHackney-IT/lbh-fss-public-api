using System.Collections.Generic;
using LBHFSSPublicAPI.V1.UseCase;
using LBHFSSPublicAPI.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LBHFSSPublicAPI.V1.Controllers
{
    [Route("api/v1/taxonomies")]
    [ApiController]
    [Produces("application/json")]
    public class TaxonomiesController : BaseController
    {
        private ITaxonomiesUseCase _taxonomiesUseCase;

        public TaxonomiesController(ITaxonomiesUseCase taxonomiesUseCase)
        {
            _taxonomiesUseCase = taxonomiesUseCase;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Dictionary<string, bool>), 200)]
        public IActionResult GetTaxonomies()
        {
            var result = _taxonomiesUseCase.ExecuteGet();
            return Ok(result);
        }
    }
}
