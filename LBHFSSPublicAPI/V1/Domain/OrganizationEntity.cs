using System;

namespace LBHFSSPublicAPI.V1.Domain
{
    public class OrganizationEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
