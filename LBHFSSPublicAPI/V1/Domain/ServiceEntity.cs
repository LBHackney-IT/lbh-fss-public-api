using System;

namespace LBHFSSPublicAPI.V1.Domain
{
    public class ServiceEntity
    {
        public int Id { get; set; }
        // #database_ef_changes_v2-17-09-2020: (service revisions removed)
        //public int RevisionId { get; set; }
        public int OrganizationId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
