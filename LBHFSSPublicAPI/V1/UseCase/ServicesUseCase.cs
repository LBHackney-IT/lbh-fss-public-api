using LBHFSSPublicAPI.V1.Boundary.Request;
using LBHFSSPublicAPI.V1.Boundary.Response;
using LBHFSSPublicAPI.V1.Factories;
using LBHFSSPublicAPI.V1.Gateways.Interfaces;
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
            var gatewayResponse = _servicesGateway.GetService(requestParams.Id);

            if(!string.IsNullOrEmpty(requestParams.PostCode))
                _addressesGateway.GetPostcodeCoordinates(requestParams.PostCode);

            return gatewayResponse.ToResponse();
        }

        public GetServiceResponseList ExecuteGet(SearchServicesRequest searchParams)
        {
            var gatewayResponse = _servicesGateway.SearchServices(searchParams);
            var response = gatewayResponse.ToResponse();
            response.Metadata.PostCode = searchParams.PostCode;
            return response;
        }
    }
}
