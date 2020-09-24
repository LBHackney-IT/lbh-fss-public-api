using Microsoft.AspNetCore.Mvc;

namespace LBHFSSPublicAPI.V1.Boundary.Request
{
    public class SearchServicesRequest
    {
        public SearchServicesRequest()
        {
            Limit = 0;
        }

        [FromQuery]
        public string Search { get; set; }

        [FromQuery]
        public string Sort { get; set; }

        [FromQuery]
        public int Offset { get; set; }

        [FromQuery]
        public int TaxonomyId { get; set; }

        [FromQuery]
        public int Limit { get; set; }

        [FromQuery]
        public string PostCode { get; set; }
    }
}
