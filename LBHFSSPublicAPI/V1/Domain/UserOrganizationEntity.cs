using System;

namespace LBHFSSPublicAPI.V1.Domain
{
    public class UserOrganizationEntity
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
