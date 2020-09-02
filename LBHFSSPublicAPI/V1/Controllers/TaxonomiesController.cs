using System.Collections.Generic;
using LBHFSSPublicAPI.V1.UseCase;
using Microsoft.AspNetCore.Mvc;

namespace LBHFSSPublicAPI.V1.Controllers
{
    [Route("api/v1/taxonomies")]
    [ApiController]
    [Produces("application/json")]
    public class TaxonomiesController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(Dictionary<string, bool>), 200)]
        public IActionResult GetTaxonomies()
        {
            var result = new Dictionary<string, bool> { { "success", true } };

            return Ok(result);
        }
    }
}
