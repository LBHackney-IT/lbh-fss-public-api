using System;
using Geolocation;

namespace LBHFSSPublicAPI.V1.Gateways.Interfaces
{
    public interface IAddressesGateway
    {
        Coordinate GetPostcodeCoordinates(string postcode);
    }
}
