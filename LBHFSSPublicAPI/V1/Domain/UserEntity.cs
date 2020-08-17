using System;

namespace LBHFSSPublicAPI.V1.Domain
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string SubId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Status { get; set; }
    }
}
