using Microsoft.AspNetCore.Mvc;

namespace LBHFSSPublicAPI.V1.Boundary.Request
{
    public class GetServiceByIdRequest
    {
        [FromRoute(Name = "id")]
        public int Id { get; set; }

        [FromQuery(Name = "postCode")]
        public string PostCode { get; set; }
    }
}
