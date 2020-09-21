using System;
using LBHFSSPublicAPI.V1.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Domain
{
    [TestFixture]
    public class ServiceLocationEntityTests
    {
        [Test]
        public void ServiceEntitiesHaveAnId()
        {
            var entity = new ServiceLocationEntity();
            entity.Id.Should().BeGreaterOrEqualTo(0);
        }

        [Test]
        public void ServiceRevisionEntitiesHaveAnAddress1()
        {
            var entity = new ServiceLocationEntity();
            var address1 = "Test";
            entity.Address1 = address1;
            entity.Address1.Should().BeSameAs(address1);
        }

        [Test]
        public void ServiceRevisionEntitiesHaveACity()
        {
            var entity = new ServiceLocationEntity();
            var city = "Test";
            entity.City = city;
            entity.City.Should().BeSameAs(city);
        }

        [Test]
        public void ServiceRevisionEntitiesHaveACountry()
        {
            var entity = new ServiceLocationEntity();
            var country = "Test";
            entity.Country = country;
            entity.Country.Should().BeSameAs(country);
        }

        [Test]
        public void ServiceRevisionEntitiesHaveALatitude()
        {
            var entity = new ServiceLocationEntity();
            var latitude = 0;
            entity.Latitude = latitude;
            entity.Latitude.Should().Be(latitude);
        }

        [Test]
        public void ServiceRevisionEntitiesHaveALongitude()
        {
            var entity = new ServiceLocationEntity();
            var longitude = 5225;
            entity.Longitude = longitude;
            entity.Longitude.Should().Be(longitude);
        }

        [Test]
        public void ServiceRevisionEntitiesHaveAPostalCode()
        {
            var entity = new ServiceLocationEntity();
            var postalCode = "Test";
            entity.PostalCode = postalCode;
            entity.PostalCode.Should().BeSameAs(postalCode);
        }

        // #database_ef_changes_v2-17-09-2020: Is this test still valid? (service revisions removed)
        //[Test]
        //public void ServiceRevisionEntitiesHaveARevisionId()
        //{
        //    var entity = new ServiceLocationEntity();
        //    entity.RevisionId = 1;
        //    entity.RevisionId.Should().Be(1);
        //}

        [Test]
        public void ServiceRevisionEntitiesHaveAStateProvince()
        {
            var entity = new ServiceLocationEntity();
            var stateProvince = "Test";
            entity.StateProvince = stateProvince;
            entity.StateProvince.Should().BeSameAs(stateProvince);
        }

        [Test]
        public void ServiceRevisionEntitiesHaveAUprn()
        {
            var entity = new ServiceLocationEntity();
            var uprn = 100054225;
            entity.Uprn = uprn;
            entity.Uprn.Should().Be(uprn);
        }

        [Test]
        public void ServiceEntitiesHaveACreatedAt()
        {
            var entity = new ServiceLocationEntity();
            var date = new DateTime(2019, 02, 21);
            entity.CreatedAt = date;
            entity.CreatedAt.Should().BeSameDateAs(date);
        }
    }
}
