using System;
using System.Collections.Generic;
using System.Linq;
using Geolocation;
using LBHFSSPublicAPI.V1.Boundary.Request;
using LBHFSSPublicAPI.V1.Boundary.Response;
using LBHFSSPublicAPI.V1.Factories;
using LBHFSSPublicAPI.V1.Gateways.Interfaces;
using LBHFSSPublicAPI.V1.Helpers;
using LBHFSSPublicAPI.V1.Infrastructure;
using LBHFSSPublicAPI.V1.UseCase.Interfaces;

namespace LBHFSSPublicAPI.V1.UseCase
{
    public class ServicesUseCase : IServicesUseCase
    {
        private readonly IServicesGateway _servicesGateway;
        private readonly IAddressesGateway _addressesGateway;

        public ServicesUseCase(IServicesGateway servicesGateway, IAddressesGateway addressesGateway)
        {
            _servicesGateway = servicesGateway;
            _addressesGateway = addressesGateway;
        }

        public GetServiceResponse ExecuteGet(GetServiceByIdRequest requestParams)
        {
            var servicesGatewayResponse = _servicesGateway.GetService(requestParams.Id);

            var usecaseResponse = servicesGatewayResponse.ToResponse();

            usecaseResponse.Metadata.PostCode = requestParams.PostCode;

            if (!string.IsNullOrEmpty(requestParams.PostCode))
                try
                {
                    Coordinate? postcodeCoord = _addressesGateway.GetPostcodeCoordinates(requestParams.PostCode);

                    if (postcodeCoord.HasValue)
                    {
                        usecaseResponse.Metadata.PostCodeLatitude = postcodeCoord.Value.Latitude;
                        usecaseResponse.Metadata.PostCodeLongitude = postcodeCoord.Value.Longitude;

                        foreach (var location in usecaseResponse.Locations)
                            if (location.Latitude.HasValue && location.Longitude.HasValue)
                                location.Distance =
                                    GeoCalculator.GetDistance(
                                        postcodeCoord.Value,
                                        new Coordinate(location.Latitude.Value, location.Longitude.Value),
                                        decimalPlaces: 1,
                                        DistanceUnit.Miles
                                    ) + " miles";

                    }
                    else
                    {
                        usecaseResponse.Metadata.Error = "Postcode coordinates not found.";
                    }
                }
                catch (Exception ex)
                {
                    usecaseResponse.Metadata.Error = ex.Message;
                }

            return usecaseResponse;
        }

        public GetServiceResponseList ExecuteGet(SearchServicesRequest searchParams)
        {
            searchParams.Search = UrlHelper.DecodeParams(searchParams.Search);
            var gatewayResponse = _servicesGateway.SearchServices(searchParams);
            var response = gatewayResponse.ToResponse();
            response.Metadata.PostCode = searchParams.PostCode;
            return response;
        }
    }
}
