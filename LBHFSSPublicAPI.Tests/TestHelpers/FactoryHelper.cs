using System;
using System.Collections.Generic;
using System.Linq;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.V1.Factories;
using Entity = LBHFSSPublicAPI.V1.Infrastructure;

namespace LBHFSSPublicAPI.Tests.TestHelpers
{
    public static class FactoryHelper
    {
        public static List<ServiceEntity> ToDomain(this List<Entity.Service> entities)
        {
            if (entities == null)
                return new List<ServiceEntity>();
            
            return entities.Select(e => e.ToDomain()).ToList();
        }
    }
}
