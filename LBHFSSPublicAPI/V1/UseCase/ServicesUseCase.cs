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
        private readonly IServicesGateway _gateway;

        public ServicesUseCase(IServicesGateway servicesGateway)
        {
            _gateway = servicesGateway;
        }
        public GetServiceResponse ExecuteGet(GetServiceByIdRequest requestParams)
        {
            var gatewayResponse = _gateway.GetService(requestParams.Id);
            return gatewayResponse.ToResponse();
        }

        public GetServiceResponseList ExecuteGet(SearchServicesRequest searchParams)
        {
            var gatewayResponse = _gateway.SearchServices(searchParams);
            var response = gatewayResponse.ToResponse();
            response.Metadata.PostCode = searchParams.PostCode;
            return response;
        }
    }
}
