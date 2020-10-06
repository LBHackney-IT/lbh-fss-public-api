using System;

namespace LBHFSSPublicAPI.V1.Domain
{
    public class SessionEntity
    {
        public int Id { get; set; }
        public string Payload { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public int UserId { get; set; }
        public DateTime? LastAccessAt { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
