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
        public int RevisionId { get; set; }
        public string StateProvince { get; set; }
        public int? Uprn { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
