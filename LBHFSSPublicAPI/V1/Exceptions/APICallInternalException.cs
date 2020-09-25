using System;
namespace LBHFSSPublicAPI.V1.Exceptions
{
    public class APICallInternalException : Exception
    {
        public APICallInternalException(string message)
            : base(message)
        {
        }
    }
}
