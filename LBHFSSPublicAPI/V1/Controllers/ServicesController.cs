using System.Collections.Generic;
using LBHFSSPublicAPI.V1.Boundary;
using LBHFSSPublicAPI.V1.UseCase;
using LBHFSSPublicAPI.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LBHFSSPublicAPI.V1.Controllers
{
    [Route("api/v1/services")]
    [ApiController]
    [Produces("application/json")]
    public class ServicesController : BaseController
    {
        private IServicesUseCase _servicesUseCase;

        public ServicesController(IServicesUseCase servicesUseCase)
        {
            _servicesUseCase = servicesUseCase;
        }

        // [HttpGet]
        // [Route("{id}")]
        // public IActionResult GetTaxonomy([FromRoute] int id) //if user doens't input anything, then it will be 0 by default!!!
        // {
        //     var usecaseResult = _servicesUseCase.ExecuteGet(id);
        //
        //     if (usecaseResult != null)
        //         return Ok(usecaseResult);
        //
        //     return NotFound(new ErrorResponse($"Service with an Id: {id} was not found."));
        // }
    }
}
