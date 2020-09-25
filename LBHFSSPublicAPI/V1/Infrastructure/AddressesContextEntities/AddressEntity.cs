using System;

namespace LBHFSSPublicAPI.V1.Infrastructure.AddressesContextEntities
{
    public class AddressEntity
    {
        public string AddressKey { get; set; }
        public int Uprn { get; set; }
        public int Usrn { get; set; }
        public int ParentUPRN { get; set; }
        public string AddressStatus { get; set; }
        public string UnitName { get; set; }
        public object UnitNumber { get; set; }
        public object BuildingName { get; set; }
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
        public int AddressStartDate { get; set; }
        public int AddressEndDate { get; set; }
        public int AddressChangeDate { get; set; }
        public int PropertyStartDate { get; set; }
        public int PropertyEndDate { get; set; }
        public int PropertyChangeDate { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string Line4 { get; set; }
    }
}
