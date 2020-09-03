using System.Collections.Generic;
using LBHFSSPublicAPI.V1.Domain;

namespace LBHFSSPublicAPI.V1.Boundary.Response
{
    public class TaxonomyResponse
    {
        //Guidance on XML comments and response objects here (https://github.com/LBHackney-IT/lbh-base-api/wiki/Controllers-and-Response-Objects)
        public List<TaxonomyEntity> Taxonomies { get; set; }
    }
}
