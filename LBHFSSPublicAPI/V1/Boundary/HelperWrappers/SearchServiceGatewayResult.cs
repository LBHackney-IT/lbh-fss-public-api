using System;
using System.Collections.Generic;
using LBHFSSPublicAPI.V1.Domain;

namespace LBHFSSPublicAPI.V1.Boundary.HelperWrappers
{
    public class SearchServiceGatewayResult
    {
        public List<ServiceEntity> FullMatchServices { get; set; }  // higher relevance
        public List<ServiceEntity> SplitMatchServices { get; set; } // lower relevance

        public SearchServiceGatewayResult(List<ServiceEntity> fullMatchServices, List<ServiceEntity> splitMatchServices)
        {
            FullMatchServices = fullMatchServices;
            SplitMatchServices = splitMatchServices;
        }
    }
}
