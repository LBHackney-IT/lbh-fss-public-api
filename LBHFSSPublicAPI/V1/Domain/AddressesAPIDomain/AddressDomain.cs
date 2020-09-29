using System;
namespace LBHFSSPublicAPI.V1.Domain.AddressesAPIDomain
{
    public class AddressDomain
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public AddressDomain(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
