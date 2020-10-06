using System;

namespace LBHFSSPublicAPI.V1.Infrastructure.AddressesContextEntities
{
    public class RootAPIResponseEntity
    {
        public DataEntity Data { get; set; }
        public int StatusCode { get; set; }
        public ErrorEntity Error { get; set; }
    }
}
