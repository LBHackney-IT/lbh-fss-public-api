using System;
using LBHFSSPublicAPI.V1.Boundary;
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
        //[ProducesResponseType(typeof(Dictionary<string, bool>), 200)]
        public IActionResult GetTaxonomies([FromQuery] string vocabulary = null)
        {
            try
            {
                var result = _taxonomiesUseCase.ExecuteGet(vocabulary);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new ErrorResponse("There was a problem getting taxonomies."));
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetTaxonomy([FromRoute] int id) //if user doens't input anything, then it will be 0 by default!!!
        {
            try
            {
                var usecaseResult = _taxonomiesUseCase.ExecuteGet(id);

                if (usecaseResult != null)
                    return Ok(usecaseResult);

                return NotFound(new ErrorResponse($"Taxonomy with an Id: {id} was not found."));
            }
            catch (Exception)
            {
                return StatusCode(500, new ErrorResponse("There was a problem getting the taxonomy."));
            }
        }
    }
}
