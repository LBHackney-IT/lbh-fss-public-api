using System;

namespace LBHFSSPublicAPI.V1.Domain
{
    public class TaxonomyEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public string Description { get; set; }
        public string Vocabulary { get; set; }
        public int Weight { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
