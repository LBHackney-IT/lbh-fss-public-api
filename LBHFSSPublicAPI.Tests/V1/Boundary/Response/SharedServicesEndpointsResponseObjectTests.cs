using System.Collections.Generic;
using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using LBHFSSPublicAPI.V1.Boundary.Response;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Boundary.Response
{
    [TestFixture]
    public class SharedServicesEndpointsResponseObjectTests
    {
        [Test]
        public void GetServiceResponseServiceObjectShouldHaveCorrectProperties()
        {
            // arrange
            var respObjType = typeof(Service);

            // act
            var entity = Randomm.Create<Service>();

            // assert
            respObjType.GetProperties().Length.Should().Be(12);
            Assert.That(entity, Has.Property("Id").InstanceOf(typeof(int)));
            Assert.That(entity, Has.Property("Name").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Categories").InstanceOf(typeof(List<Category>)));
            Assert.That(entity, Has.Property("Contact").InstanceOf(typeof(Contact)));
            Assert.That(entity, Has.Property("Demographic").InstanceOf(typeof(List<Demographic>)));
            Assert.That(entity, Has.Property("Description").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Images").InstanceOf(typeof(Image)));
            Assert.That(entity, Has.Property("Locations").InstanceOf(typeof(List<Location>)));
            Assert.That(entity, Has.Property("Organization").InstanceOf(typeof(Organization)));
            Assert.That(entity, Has.Property("Referral").InstanceOf(typeof(Referral)));
            Assert.That(entity, Has.Property("Social").InstanceOf(typeof(Social)));
            Assert.That(entity, Has.Property("Status").InstanceOf(typeof(string)));
        }

        [Test]
        public void GetServiceResponseCategoryObjectShouldHaveCorrectProperties()
        {
            // arrange
            var entityType = typeof(Category);

            // act
            var entity = Randomm.Create<Category>();

            // assert
            entityType.GetProperties().Length.Should().Be(5);
            Assert.That(entity, Has.Property("Id").InstanceOf(typeof(int)));
            Assert.That(entity, Has.Property("Name").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Description").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Vocabulary").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Weight").InstanceOf(typeof(int)));
        }

        [Test]
        public void GetServiceResponseContactObjectShouldHaveCorrectProperties()
        {
            // arrange
            var entityType = typeof(Contact);

            // act
            var entity = Randomm.Create<Contact>();

            // assert
            entityType.GetProperties().Length.Should().Be(3);
            Assert.That(entity, Has.Property("Email").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Telephone").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Website").InstanceOf(typeof(string)));
        }

        [Test]
        public void GetServiceResponseDemographicObjectShouldHaveCorrectProperties()
        {
            // arrange
            var entityType = typeof(Demographic);

            // act
            var entity = Randomm.Create<Demographic>();

            // assert
            entityType.GetProperties().Length.Should().Be(3);
            Assert.That(entity, Has.Property("Id").InstanceOf(typeof(int)));
            Assert.That(entity, Has.Property("Name").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Vocabulary").InstanceOf(typeof(string)));
        }

        [Test]
        public void GetServiceResponseImageObjectShouldHaveCorrectProperties()
        {
            // arrange
            var entityType = typeof(Image);

            // act
            var entity = Randomm.Create<Image>();

            // assert
            entityType.GetProperties().Length.Should().Be(2);
            Assert.That(entity, Has.Property("Medium").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Original").InstanceOf(typeof(string)));
        }

        [Test]
        public void GetServiceResponseLocationObjectShouldHaveCorrectProperties()
        {
            // arrange
            var entityType = typeof(Location);

            // act
            var entity = Randomm.Create<Location>();

            // assert
            entityType.GetProperties().Length.Should().Be(10);
            Assert.That(entity, Has.Property("Latitude").InstanceOf(typeof(double)));
            Assert.That(entity, Has.Property("Longitude").InstanceOf(typeof(double)));
            Assert.That(entity, Has.Property("Uprn").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Address1").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Address2").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("City").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("StateProvince").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("PostalCode").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Country").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Distance").InstanceOf(typeof(string)));
        }

        [Test]
        public void GetServiceResponseOrganizationObjectShouldHaveCorrectProperties()
        {
            // arrange
            var entityType = typeof(Organization);

            // act
            var entity = Randomm.Create<Organization>();

            // assert
            entityType.GetProperties().Length.Should().Be(3);
            Assert.That(entity, Has.Property("Id").InstanceOf(typeof(int)));
            Assert.That(entity, Has.Property("Name").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Status").InstanceOf(typeof(string)));
        }

        [Test]
        public void GetServiceResponseReferralObjectShouldHaveCorrectProperties()
        {
            // arrange
            var entityType = typeof(Referral);

            // act
            var entity = Randomm.Create<Referral>();

            // assert
            entityType.GetProperties().Length.Should().Be(2);
            Assert.That(entity, Has.Property("Email").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Website").InstanceOf(typeof(string)));
        }

        [Test]
        public void GetServiceResponseSocialObjectShouldHaveCorrectProperties()
        {
            // arrange
            var entityType = typeof(Social);

            // act
            var entity = Randomm.Create<Social>();

            // assert
            entityType.GetProperties().Length.Should().Be(4);
            Assert.That(entity, Has.Property("Facebook").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Twitter").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Instagram").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Linkedin").InstanceOf(typeof(string)));
        }

        [Test]
        public void GetServiceResponseMetadataObjectShouldHaveCorrectProperties()
        {
            // arrange
            var entityType = typeof(Metadata);

            // act
            var entity = Randomm.Create<Metadata>();

            // assert
            entityType.GetProperties().Length.Should().Be(4);
            Assert.That(entity, Has.Property("PostCode").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("PostCodeLatitude").InstanceOf(typeof(double?)));
            Assert.That(entity, Has.Property("PostCodeLongitude").InstanceOf(typeof(double?)));
            Assert.That(entity, Has.Property("Error").InstanceOf(typeof(string)));
        }
    }
}
