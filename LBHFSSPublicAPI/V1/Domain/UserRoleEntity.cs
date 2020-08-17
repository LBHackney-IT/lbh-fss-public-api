using System;

namespace LBHFSSPublicAPI.V1.Domain
{
    public class UserRoleEntity
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
