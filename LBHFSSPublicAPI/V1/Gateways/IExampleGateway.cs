using System.Collections.Generic;
using LBHFSSPublicAPI.V1.Domain;

namespace LBHFSSPublicAPI.V1.Gateways
{
    public interface IExampleGateway
    {
        //Entity GetEntityById(int id);

        List<Entity> GetAll();
    }
}
