using System;
using System.Collections.Generic;
using System.Linq;
using Geolocation;
using LBHFSSPublicAPI.V1.Boundary.Request;
using LBHFSSPublicAPI.V1.Boundary.Response;
using Response = LBHFSSPublicAPI.V1.Boundary.Response;
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
            if (usecaseResponse == null)
                return null;
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
            var postcodeIsGiven = !string.IsNullOrEmpty(requestParams.PostCode);

            var gatewayResponse = _servicesGateway.SearchServices(requestParams);
            var fullMServices = gatewayResponse.FullMatchServices.ToResponseServices();
            var splitMServices = gatewayResponse.SplitMatchServices.ToResponseServices();


            var metadata = new Metadata();
            metadata.PostCode = postcodeIsGiven ? requestParams.PostCode : null;

            if (postcodeIsGiven)
                try
                {
                    //Get the postcode's coordinates
                    Coordinate? postcodeCoord = _addressesGateway.GetPostcodeCoordinates(requestParams.PostCode);

                    if (postcodeCoord.HasValue)
                    {
                        metadata.PostCodeLatitude = postcodeCoord.Value.Latitude;
                        metadata.PostCodeLongitude = postcodeCoord.Value.Longitude;

                        fullMServices.CalculateServiceLocationDistances(postcodeCoord.Value);
                        splitMServices.CalculateServiceLocationDistances(postcodeCoord.Value);
                    }
                    else
                    {
                        metadata.Error = "Postcode coordinates not found.";
                    }
                }
                catch (Exception ex)
                {
                    metadata.Error = ex.Message;
                }

            if (postcodeIsGiven) // And metadata error is empty
            {
                fullMServices.Sort();       // Sort by minimum service location's distance
                splitMServices.Sort();      // IComparator<Service> is defined on the object iteself
            }
            else
            {
                Comparison<Response.Service> byName = (s1, s2) => string.Compare(s1.Name, s2.Name);

                fullMServices.Sort(byName);
                splitMServices.Sort(byName);
            }

            var usecaseResponse = ServiceFactory.SearchServiceUsecaseResponse(fullMServices, splitMServices, metadata);
            return usecaseResponse;
        }
    }

    public static class UtilityHelper
    {
        public static void CalculateServiceLocationDistances(this List<Response.Service> serviceCollection, Coordinate originPoint)
        {
            foreach (var service in serviceCollection)
                foreach (var location in service.Locations)
                    if (location.Latitude.HasValue && location.Longitude.HasValue)
                        location.Distance =
                            GeoCalculator.GetDistance(
                                originPoint,
                                new Coordinate(location.Latitude.Value, location.Longitude.Value),
                                decimalPlaces: 1,
                                DistanceUnit.Miles
                            ) + " miles";
        }
    }
}
