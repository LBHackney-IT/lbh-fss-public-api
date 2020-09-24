using System;
using System.Net;

namespace LBHFSSPublicAPI.V1.Infrastructure
{
    /// <summary>
    /// Addresses API Context Response. Gateway requires the status code to do decisions on,
    /// and json string it could deserialize. It should be the Context's responsibility to
    /// transform a byte stream into a string.
    /// </summary>
    public class AddressesAPIContextResponse
    {
        public int StatusCode { get; set; }
        public string JsonContent { get; set; }

        public AddressesAPIContextResponse(int statusCode, string jsonContent)
        {
            StatusCode = statusCode;
            JsonContent = jsonContent;
        }
    }
}
