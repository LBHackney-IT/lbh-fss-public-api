using System;

namespace LBHFSSPublicAPI.V1.Infrastructure
{
    public class AnalyticsEvent
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public DateTime TimeStamp { get; set; }
        public Service Service { get; set; }
    }
}
