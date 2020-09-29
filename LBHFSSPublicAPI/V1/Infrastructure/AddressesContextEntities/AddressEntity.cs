using System;

namespace LBHFSSPublicAPI.V1.Infrastructure.AddressesContextEntities
{
    public class AddressEntity
    {
        public string AddressKey { get; set; }
        public string Uprn { get; set; }
        public string Usrn { get; set; }
        public string ParentUPRN { get; set; }
        public string AddressStatus { get; set; }
        public string UnitName { get; set; }
        public string UnitNumber { get; set; }
        public string BuildingName { get; set; }
        public string BuildingNumber { get; set; }
        public string Street { get; set; }
        public string Postcode { get; set; }
        public string Locality { get; set; }
        public string Town { get; set; }
        public string Gazetteer { get; set; }
        public string CommercialOccupier { get; set; }
        public string Ward { get; set; }
        public string UsageDescription { get; set; }
        public string UsagePrimary { get; set; }
        public string UsageCode { get; set; }
        public string PlanningUseClass { get; set; }
        public bool PropertyShell { get; set; }
        public bool HackneyGazetteerOutOfBoroughAddress { get; set; }
        public double Easting { get; set; }
        public double Northing { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string AddressStartDate { get; set; }
        public string AddressEndDate { get; set; }
        public string AddressChangeDate { get; set; }
        public string PropertyStartDate { get; set; }
        public string PropertyEndDate { get; set; }
        public string PropertyChangeDate { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string Line4 { get; set; }
    }
}
