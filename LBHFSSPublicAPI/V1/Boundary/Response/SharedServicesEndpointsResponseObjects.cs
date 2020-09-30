using System.Collections.Generic;

namespace LBHFSSPublicAPI.V1.Boundary.Response
{
    public class Service
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
    }

    public class Contact
    {
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Website { get; set; }
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
        public string Medium { get; set; }
        public string Original { get; set; }
    }

    public class Location
    {
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Uprn { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Distance { get; set; }
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
        public string Website { get; set; }
    }

    public class Social
    {
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Linkedin { get; set; }
    }

    public class Metadata
    {
        public string PostCode { get; set; }
        public double? PostCodeLatitude { get; set; }
        public double? PostCodeLongitude { get; set; }
        public string Error { get; set; }
    }
}
