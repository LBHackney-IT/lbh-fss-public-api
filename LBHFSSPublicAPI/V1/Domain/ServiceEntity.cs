using System;

namespace LBHFSSPublicAPI.V1.Domain
{
    public class ServiceEntity
    {
        public int Id { get; set; }
        public int RevisionId { get; set; }
        public int OrganizationId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
