using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoFixture;
using AutoFixture.Dsl;
using AutoFixture.Kernel;
using Bogus;
using Geolocation;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.V1.Infrastructure;
using Newtonsoft.Json;

namespace LBHFSSPublicAPI.Tests.TestHelpers
{
    public static class Randomm
    {
        private static Faker _faker = new Faker("en_GB");          // Good for single values
        private static Fixture _fixture = new Fixture();           // Good for complex objects
        static Randomm()                                           // Gets called automatically by common language runtime (CLR)
        {
            IgnoreVirtualMethods();
        }
        public static int Id(int minimum = 0, int maximum = 10000)
        {
            return _faker.Random.Int(minimum, maximum);
        }
        public static string Text()
        {
            return string.Join(" ", _faker.Random.Words(5));
        }
        public static T Create<T>()
        {
            return _fixture.Create<T>();
        }
        public static IEnumerable<T> CreateMany<T>(int quantity = 3)
        {
            return _fixture.CreateMany<T>(quantity);
        }
        #region Options
        public static void IgnoreVirtualMethods()
        {
            _fixture.Customize(new IgnoreVirtualMembersCustomisation());
        }

        public static ICustomizationComposer<T> Build<T>()
        {
            return _fixture.Build<T>();
        }

        #endregion

        /// <summary>
        /// Coordinate bounds (Lon, Lat) of a Rectangle surrounding
        /// Hackey. Some parts of rectangle are outside hackney.
        /// </summary>
        /// <returns></returns>
        private static double Longitude()
        {
            return _faker.Random.Double(-0.11, 0);
        }

        private static double Latitude()
        {
            return _faker.Random.Double(51.513, 51.58);
        }

        public static Coordinate Coordinates()
        {
            return new Coordinate(Latitude(), Longitude());
        }

        public static string Postcode()
        {
            return _faker.Address.ZipCode();
        }

        #region Addresses API Fake Response

        private static List<FakeAddress> FakeAddresses()
        {
            return _fixture.Build<FakeAddress>()
                .With(a => a.Longitude, Longitude())
                .With(a => a.Latitude, Latitude())
                .CreateMany()
                .ToList();
        }

        private static FakeData FakeData(Coordinate coordinate, bool populateAddressCollection)
        {
            var listOfAddresses = _fixture.Build<FakeData>()
                .With(d => d.Address, populateAddressCollection ? FakeAddresses() : new List<FakeAddress>())
                .Create();

            if (populateAddressCollection)
            {
                listOfAddresses.Address.FirstOrDefault().Latitude = coordinate.Latitude;
                listOfAddresses.Address.FirstOrDefault().Longitude = coordinate.Longitude;
            }

            return listOfAddresses;
        }

        private static FakeError FakeError()
        {
            return _fixture.Build<FakeError>()
                .With(e => e.IsValid, false)
                .With(e => e.Errors, null)
                .With(e => e.ValidationErrors, _fixture.CreateMany<FakeValidationError>().ToList())
                .Create();
        }

        private static string FakeAddressesAPIJsonResponse(int statusCode, Coordinate coordinate, bool populateAddressCollection)
        {
            var isStatus200 = statusCode == 200;

            var fakeResponse = _fixture.Build<FakeAddressesAPIJsonResponse>()
                .With(r => r.Data, isStatus200 ? FakeData(coordinate, populateAddressCollection) : null)
                .With(r => r.Error, isStatus200 ? null : FakeError())
                .With(r => r.StatusCode, statusCode)
                .Create();

            return JsonConvert.SerializeObject(fakeResponse);
        }

        public static AddressesAPIContextResponse AddressesAPIContextResponse(int statusCode, Coordinate coordinate)
        {
            return new AddressesAPIContextResponse(
                statusCode,
                FakeAddressesAPIJsonResponse(statusCode, coordinate, populateAddressCollection: true));
        }

        public static AddressesAPIContextResponse AddressesAPIContextResponse(int statusCode)
        {
            return new AddressesAPIContextResponse(
                statusCode,
                FakeAddressesAPIJsonResponse(statusCode, new Coordinate(), populateAddressCollection: false));
        }

        #endregion
    }
    #region Autofixture Customization
    public class IgnoreVirtualMembers : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            var pi = request as PropertyInfo;
            if (pi == null)
                return new NoSpecimen();
            if (pi.GetGetMethod().IsVirtual)
                return null;
            return new NoSpecimen();
        }
    }
    public class IgnoreVirtualMembersCustomisation : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new IgnoreVirtualMembers());
        }
    }
    #endregion

    #region Addresses API response classes - required for testing.

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    /// <summary>
    /// Classes generated based of Addresses API response. Only used for testing purposes
    /// to generate random, but structured string responses.
    /// </summary>
    public class FakeAddress
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

    public class FakeData
    {
        public List<FakeAddress> Address { get; set; }

#pragma warning disable CA1707 // Identifiers should not contain underscores
        public int Page_count { get; set; }
        public int Total_count { get; set; }
#pragma warning restore CA1707 // Identifiers should not contain underscores
    }

    public class FakeAddressesAPIJsonResponse
    {
        public FakeData Data { get; set; }
        public int StatusCode { get; set; }
        public object Error { get; set; }
    }

    public class FakeValidationError
    {
        public string Message { get; set; }
        public string FieldName { get; set; }
    }

    public class FakeError
    {
        public bool IsValid { get; set; }
        public object Errors { get; set; }
        public List<FakeValidationError> ValidationErrors { get; set; }
    }

    #endregion
}
