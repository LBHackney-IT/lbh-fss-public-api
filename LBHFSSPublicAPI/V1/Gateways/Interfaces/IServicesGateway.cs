using LBHFSSPublicAPI.V1.Domain;

namespace LBHFSSPublicAPI.V1.Gateways.Interfaces
{
    public interface IServicesGateway
    {
        ServiceEntity GetService(int id);
    }
}
