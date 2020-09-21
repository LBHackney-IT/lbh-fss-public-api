using System;
using System.Collections.Generic;

namespace LBHFSSPublicAPI.V1.Boundary.Response
{
    public class GetServiceResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Category> Categories { get; set; }
        public Contact Contact { get; set; }
        public ICollection<Demographic> Demographic { get; set; }
        public string Description { get; set; }
        public Image Images { get; set; }
        public ICollection<Location> Locations { get; set; }
        public Organization Organization { get; set; }
        public Referral Referral { get; set; }
        public Social Social { get; set; }
        public string Status { get; set; }
        public string Distance => string.Empty;
    }

    public class Contact
    {
        public string Email { get; set; }
        public string Telephone { get; set; }
        public Uri Website { get; set; }
    }

    public class Demographic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Vocabulary { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Vocabulary { get; set; }
        public int Weight { get; set; }
    }

    public class Image
    {
        public Uri Medium { get; set; }
        public Uri Original { get; set; }
    }

    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Uprn { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }

    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }

    public class Referral
    {
        public string Email { get; set; }
        public Uri Website { get; set; }
    }

    public class Social
    {
        public Uri Facebook { get; set; }
        public Uri Twitter { get; set; }
        public Uri Instagram { get; set; }
        public Uri Linkedin { get; set; }
    }
}
