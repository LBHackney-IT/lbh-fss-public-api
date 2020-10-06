using System.Collections.Generic;

namespace LBHFSSPublicAPI.V1.Boundary.Response
{
    public class GetServiceResponseList
    {
        public List<Service> Services { get; set; }
        public Metadata Metadata { get; set; }
    }
}
