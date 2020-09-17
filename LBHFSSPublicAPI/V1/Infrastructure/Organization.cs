using System;
using System.Collections.Generic;

namespace LBHFSSPublicAPI.V1.Infrastructure
{
    public partial class Organization
    {
        public Organization()
        {
            Services = new HashSet<Service>();
            UserOrganizations = new HashSet<UserOrganization>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ReviewerUid { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public string ReviewerMessage { get; set; }
        public string Status { get; set; }
        public bool? IsHackneyBased { get; set; }
        public bool? IsRegisteredCharity { get; set; }
        public string CharityNumber { get; set; }
        public bool? HasHcOrColGrant { get; set; }
        public bool? HasHcvsOrHgOrAelGrant { get; set; }
        public bool? IsTraRegistered { get; set; }
        public string RslOrHaAssociation { get; set; }
        public bool? IsLotteryFunded { get; set; }
        public string LotteryFundedProject { get; set; }
        public string FundingOther { get; set; }
        public bool? HasChildSupport { get; set; }
        public bool? HasChildSafeguardingLead { get; set; }
        public string ChildSafeguardingLeadFirstName { get; set; }
        public string ChildSafeguardingLeadLastName { get; set; }
        public string ChildSafeguardingLeadTrainingMonth { get; set; }
        public string ChildSafeguardingLeadTrainingYear { get; set; }
        public bool? HasAdultSupport { get; set; }
        public bool? HasAdultSafeguardingLead { get; set; }
        public string AdultSafeguardingLeadFirstName { get; set; }
        public string AdultSafeguardingLeadLastName { get; set; }
        public string AdultSafeguardingLeadTrainingMonth { get; set; }
        public string AdultSafeguardingLeadTrainingYear { get; set; }
        public bool? HasEnhancedSupport { get; set; }
        public bool? IsLocalOfferListed { get; set; }

        public virtual User ReviewerU { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<UserOrganization> UserOrganizations { get; set; }
    }
}
