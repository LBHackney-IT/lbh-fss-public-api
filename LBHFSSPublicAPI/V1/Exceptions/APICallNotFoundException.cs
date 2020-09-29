using System;
namespace LBHFSSPublicAPI.V1.Exceptions
{
    public class APICallNotFoundException : Exception
    {
        public APICallNotFoundException(string apiName)
            : base($"A call route to {apiName} API was not found.")
        { }
    }
}
