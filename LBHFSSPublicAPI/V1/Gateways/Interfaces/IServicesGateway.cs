using System.Collections;
using System.Collections.Generic;
using LBHFSSPublicAPI.V1.Boundary.Request;
using LBHFSSPublicAPI.V1.Domain;

namespace LBHFSSPublicAPI.V1.Gateways.Interfaces
{
    public interface IServicesGateway
    {
        ServiceEntity GetService(int id);
        ICollection<ServiceEntity> SearchServices(SearchServicesRequest requestParams);
    }
}
