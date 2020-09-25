using System;

namespace LBHFSSPublicAPI.V1.Exceptions
{
    public class APICallNotAuthorizedException : Exception
    {
        public APICallNotAuthorizedException(string apiName)
            : base($"A call to {apiName} API was not authorized.")
        { }
    }
}
