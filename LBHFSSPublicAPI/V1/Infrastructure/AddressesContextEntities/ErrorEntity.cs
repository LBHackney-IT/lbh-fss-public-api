using System;
using System.Collections.Generic;

namespace LBHFSSPublicAPI.V1.Infrastructure.AddressesContextEntities
{
    public class ErrorEntity
    {
        public bool IsValid { get; set; }
        public List<ExecutionErrorEntity> Errors { get; set; }
        public List<ValidationErrorEntity> ValidationErrors { get; set; }
    }
}
