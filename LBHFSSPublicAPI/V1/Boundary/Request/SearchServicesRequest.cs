using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace LBHFSSPublicAPI.V1.Boundary.Request
{
    public class SearchServicesRequest
    {
        public SearchServicesRequest()
        {
            Limit = 0;
            TaxonomyIds = new List<int>();
        }

        [FromQuery]
        public string Search { get; set; }

        [FromQuery]
        public string Sort { get; set; }

        [FromQuery]
        public int Offset { get; set; }

        [FromQuery]
        public List<int> TaxonomyIds { get; set; }

        [FromQuery]
        public int Limit { get; set; }

        [FromQuery]
        public string PostCode { get; set; }
    }
}
