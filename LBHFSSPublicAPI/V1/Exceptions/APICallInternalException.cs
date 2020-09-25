using System;
using System.Collections.Generic;

namespace LBHFSSPublicAPI.V1.Exceptions
{
    public class APICallInternalException : Exception
    {
        public APICallInternalException(List<string> errors)
            : base(String.Join(";\n", errors))
        {
        }
    }
}
