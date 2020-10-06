using System;
using System.Collections.Generic;
using LBHFSSPublicAPI.V1.Infrastructure;

namespace LBHFSSPublicAPI.V1.Domain
{
    public class ServiceEntity
    {
        public int Id { get; set; }
        public int? OrganizationId { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Linkedin { get; set; }
        public string Keywords { get; set; }
        public string ReferralLink { get; set; }
        public string ReferralEmail { get; set; }
        public int? ImageId { get; set; }

        public File Image { get; set; }
        public Organization Organization { get; set; }
        public ICollection<ServiceLocation> ServiceLocations { get; set; }
        public ICollection<ServiceTaxonomy> ServiceTaxonomies { get; set; }
    }
}
