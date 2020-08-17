using System;
using System.Collections.Generic;

namespace LBHFSSPublicAPI.V1.Infrastructure
{
    public partial class Organization
    {
        public Organization()
        {
            Services = new HashSet<Service>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
