using LBHFSSPublicAPI.V1.Boundary.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LBHFSSPublicAPI.V1.Controllers
{
    [ApiController]
    //TODO: Rename to match the APIs endpoint
    [Route("api/v1/residents")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    //TODO: rename class to match the API name
    public class LBHFSSPublicAPIController : BaseController
    {
        public LBHFSSPublicAPIController()
        {

        }
    }
}
