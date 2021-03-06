using System;

namespace LBHFSSPublicAPI.V1.Domain
{
    public class ServiceLocationEntity
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string PostalCode { get; set; }

        // #database_ef_changes_v2-17-09-2020: (probably no longer valid, service revisions removed)
        //public int RevisionId { get; set; }

        public string StateProvince { get; set; }
        public string Uprn { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
