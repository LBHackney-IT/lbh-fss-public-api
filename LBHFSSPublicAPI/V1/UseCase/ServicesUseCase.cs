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

                        foreach (var location in usecaseResponse.Service.Locations)
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

        public GetServiceResponseList ExecuteGet(SearchServicesRequest requestParams)
        {
            requestParams.Search = UrlHelper.DecodeParams(requestParams.Search);
            var gatewayResponse = _servicesGateway.SearchServices(requestParams);
            var usecaseResponse = gatewayResponse.FullMatchServices.ToResponse(); //TODO: need to change factory and implementation
            usecaseResponse.Metadata.PostCode = string.IsNullOrEmpty(requestParams.PostCode) ? null : requestParams.PostCode;

            if (!string.IsNullOrEmpty(requestParams.PostCode))
                try
                {
                    Coordinate? postcodeCoord = _addressesGateway.GetPostcodeCoordinates(requestParams.PostCode);

                    if (postcodeCoord.HasValue)
                    {
                        usecaseResponse.Metadata.PostCodeLatitude = postcodeCoord.Value.Latitude;
                        usecaseResponse.Metadata.PostCodeLongitude = postcodeCoord.Value.Longitude;

                        foreach (var service in usecaseResponse.Services)
                            foreach (var location in service.Locations)
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

            if (!string.IsNullOrEmpty(requestParams.PostCode))
                usecaseResponse.Services.Sort();
            else
                usecaseResponse.Services.Sort(
                    (s1, s2) => string.Compare(s1.Name, s2.Name)
                  );

            return usecaseResponse;
        }
    }
}
