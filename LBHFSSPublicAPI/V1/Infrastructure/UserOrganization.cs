using System;

namespace LBHFSSPublicAPI.V1.Infrastructure
{
    public partial class UserOrganization
    {
        public int? Id { get; set; }
        public int? OrganizationId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual User IdNavigation { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
