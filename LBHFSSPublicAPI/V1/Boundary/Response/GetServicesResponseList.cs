using System.Collections.Generic;
using LBHFSSPublicAPI.V1.Domain;

namespace LBHFSSPublicAPI.V1.Boundary.Response
{
    public class GetServiceResponseList
    {
        public GetServiceResponseList()
        {
            Services = new List<GetServiceResponse>();
            Metadata = new ServicesResponseMetadata();
        }
        public ICollection<GetServiceResponse> Services { get; set; }
        public ServicesResponseMetadata Metadata { get; set; }
    }

    public class ServicesResponseMetadata
    {
        public string PostCode { get; set; }
        public decimal PostCodeLatitude { get; set; }
        public decimal PostCodeLongitude { get; set; }
    }
}

