using System;
using System.Collections.Generic;

namespace LBHFSSPublicAPI.V1.Exceptions
{
    public class BadAPICallException : Exception
    {
        public BadAPICallException(List<string> validationErrors)
            : base(String.Join(";\n", validationErrors))
        {
        }
    }
}
