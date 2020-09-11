using System.Collections.Generic;
using LBHFSSPublicAPI.V1.Domain;

namespace LBHFSSPublicAPI.V1.Boundary.Response
{
    public class TaxonomyResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Vocabulary { get; set; }
        public int Weight { get; set; }
        //Guidance on XML comments and response objects here (https://github.com/LBHackney-IT/lbh-base-api/wiki/Controllers-and-Response-Objects)
    }
}

