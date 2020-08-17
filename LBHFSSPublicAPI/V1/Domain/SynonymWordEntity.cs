using System;

namespace LBHFSSPublicAPI.V1.Domain
{
    public class SynonymWordEntity
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Word { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
