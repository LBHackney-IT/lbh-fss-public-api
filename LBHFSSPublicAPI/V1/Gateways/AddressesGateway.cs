using System;
using LBHFSSPublicAPI.V1.Infrastructure;
using Geolocation;
using LBHFSSPublicAPI.V1.Gateways.Interfaces;
using Newtonsoft.Json;
using LBHFSSPublicAPI.V1.Exceptions;
using LBHFSSPublicAPI.V1.Infrastructure.AddressesContextEntities;
using System.Linq;
using LBHFSSPublicAPI.V1.Domain.AddressesAPIDomain;

namespace LBHFSSPublicAPI.V1.Gateways
{
    public class AddressesGateway : IAddressesGateway
    {
        private readonly IAddressesAPIContext _apiContext;

        public AddressesGateway(IAddressesAPIContext apiContext)
        {
            _apiContext = apiContext;
        }

        public Coordinate? GetPostcodeCoordinates(string postcode)
        {
            try
            {
                var apiResponse = _apiContext.GetAddressesRequest(postcode);
                var jsonContent = apiResponse.JsonContent;
                var statusCode = apiResponse.StatusCode;

                if (statusCode == 400)
                {
                    var errResp = JsonConvert.DeserializeObject<RootAPIResponseEntity>(jsonContent);
                    var errorList = errResp.Error.ValidationErrors.Select(v => v.Message).ToList();
                    throw new BadAPICallException(errorList);
                }
                else if (statusCode == 403)
                {
                    throw new APICallNotAuthorizedException("Addresses");
                }
                else if (statusCode == 500)
                {
                    var errResp = JsonConvert.DeserializeObject<RootAPIResponseEntity>(jsonContent);
                    var errorList = errResp.Error.Errors.Select(e => e.Message).ToList();
                    throw new APICallInternalException(errorList);
                }

                // 200 here, but TODO: should cover other cases to avoid potential errors. Low priority right now, though.
                var dataResp = JsonConvert.DeserializeObject<RootAPIResponseEntity>(jsonContent);
                var addressDomain = dataResp.Data.Address.Select(a => new AddressDomain(a.Latitude, a.Longitude)).FirstOrDefault(); // useful if we're going to avarage, which would be sensible, but not for MVP

                return addressDomain != null
                    ? new Coordinate(addressDomain.Latitude, addressDomain.Longitude)
                    : null as Coordinate?;
            }
            catch (NullReferenceException ex) // No point sending the stacktrace to front-end. TODO: implement logging to log the message.
            {
                throw new ResponseSchemaNotRecognisedException($"Addresses API response schema not recognized.");
            }
            catch (ArgumentNullException ex) // When Linq is called on empty collection, a different error results
            {
                throw new ResponseSchemaNotRecognisedException($"Addresses API response schema not recognized.");
            }
        }
    }
}
