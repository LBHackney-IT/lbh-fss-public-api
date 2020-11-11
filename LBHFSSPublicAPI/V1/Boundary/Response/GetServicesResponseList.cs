using System.Collections.Generic;

namespace LBHFSSPublicAPI.V1.Boundary.Response
{
    public class GetServiceResponseList
    {
        public List<Service> Services { get; set; }
        public Metadata Metadata { get; set; }

        public GetServiceResponseList() { }

        public GetServiceResponseList(List<Service> services, Metadata metadata)
        {
            Services = services;
            Metadata = metadata;
        }
    }
}
