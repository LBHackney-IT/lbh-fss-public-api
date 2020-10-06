using System;

namespace LBHFSSPublicAPI.V1.Domain
{
    public class ServiceRevisionEntity
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Description { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Linkedin { get; set; }
        public string Name { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public string ReviewerMessage { get; set; }
        public int ReviewerUid { get; set; }
        public int ServiceId { get; set; }
        public string Status { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public string Telephone { get; set; }
        public string Twitter { get; set; }
        public string Website { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
