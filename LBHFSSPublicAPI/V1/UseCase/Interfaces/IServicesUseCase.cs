using System.Collections;
using System.Collections.Generic;
using LBHFSSPublicAPI.V1.Boundary.Request;
using LBHFSSPublicAPI.V1.Boundary.Response;

namespace LBHFSSPublicAPI.V1.UseCase.Interfaces
{
    public interface IServicesUseCase
    {
        public GetServiceResponse ExecuteGet(GetServiceByIdRequest requestParams);
    }
}
