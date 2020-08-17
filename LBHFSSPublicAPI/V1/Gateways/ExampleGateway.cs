using System.Collections.Generic;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.V1.Factories;
using LBHFSSPublicAPI.V1.Infrastructure;

namespace LBHFSSPublicAPI.V1.Gateways
{
    //TODO: Rename to match the data source that is being accessed in the gateway eg. MosaicGateway
    public class ExampleGateway : IExampleGateway
    {
        private readonly DatabaseContext _databaseContext;

        public ExampleGateway(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        // public Entity GetEntityById(int id)
        // {
        //     var result = _databaseContext.Services.Find(id);
        //
        //     return result?.ToDomain();
        // }

        public List<Entity> GetAll()
        {
            return new List<Entity>();
        }
    }
}
