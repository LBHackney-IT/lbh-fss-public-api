using System;
namespace LBHFSSPublicAPI.V1.Exceptions
{
    public class ResponseSchemaNotRecognisedException : Exception
    {
        public ResponseSchemaNotRecognisedException(string message)
            : base(message)
        {
        }
    }
}
