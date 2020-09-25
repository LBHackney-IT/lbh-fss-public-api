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
using LBHFSSPublicAPI.V1.Infrastructure.AddressesContextEntities;
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
        public static string Word()
        {
            return _faker.Random.Word();
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

        private static List<AddressEntity> FakeAddresses()
        {
            return _fixture.Build<AddressEntity>()
                .With(a => a.Longitude, Longitude())
                .With(a => a.Latitude, Latitude())
                .CreateMany()
                .ToList();
        }

        private static DataEntity FakeData(Coordinate coordinate, bool populateAddressCollection)
        {
            var listOfAddresses = _fixture.Build<DataEntity>()
                .With(d => d.Address, populateAddressCollection ? FakeAddresses() : new List<AddressEntity>())
                .Create();

            if (populateAddressCollection)
            {
                listOfAddresses.Address.FirstOrDefault().Latitude = coordinate.Latitude;
                listOfAddresses.Address.FirstOrDefault().Longitude = coordinate.Longitude;
            }

            return listOfAddresses;
        }

        private static ErrorEntity FakeError(bool isServerErrorResponse)
        {
            return _fixture.Build<ErrorEntity>()
                .With(e => e.IsValid, false)
                .With(e => e.Errors,
                    isServerErrorResponse ?
                    new List<ExecutionErrorEntity> { _fixture.Create<ExecutionErrorEntity>() } :
                    null)
                .With(e => e.ValidationErrors,
                    isServerErrorResponse ?
                    null :
                    _fixture.CreateMany<ValidationErrorEntity>().ToList())
                .Create();
        }

        private static string FakeAddressesAPIJsonResponse(int statusCode, Coordinate coordinate, bool populateAddressCollection)
        {
            var isStatus200 = statusCode == 200;
            var isStatus500 = statusCode == 500;

            var fakeResponse = _fixture.Build<RootAPIResponseEntity>()
                .With(r => r.Data, isStatus200 ? FakeData(coordinate, populateAddressCollection) : null)
                .With(r => r.Error, isStatus200 ? null : FakeError(isStatus500))
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
}
