using System;
using System.Collections.Generic;

namespace LBHFSSPublicAPI.V1.Infrastructure.AddressesContextEntities
{
    public class DataEntity
    {
        public List<AddressEntity> Address { get; set; }

#pragma warning disable CA1707 // Identifiers should not contain underscores
        public int Page_count { get; set; }
        public int Total_count { get; set; }
#pragma warning restore CA1707 // Identifiers should not contain underscores
    }
}
