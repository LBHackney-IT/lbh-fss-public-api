using System;
using System.Net.Http;

namespace LBHFSSPublicAPI.V1.Infrastructure
{
    public interface IAddressesAPIContext
    {
        AddressesAPIContextResponse GetAddressesRequest(string postcode);
    }
}
