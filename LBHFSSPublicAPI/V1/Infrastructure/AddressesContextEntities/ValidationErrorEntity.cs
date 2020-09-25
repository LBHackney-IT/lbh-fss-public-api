using System;

namespace LBHFSSPublicAPI.V1.Infrastructure.AddressesContextEntities
{
    public class ValidationErrorEntity
    {
        public string Message { get; set; }
        public string FieldName { get; set; }
    }
}
