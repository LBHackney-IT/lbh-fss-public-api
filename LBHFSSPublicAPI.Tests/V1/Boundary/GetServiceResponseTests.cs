using System;
using System.Collections.Generic;
using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using LBHFSSPublicAPI.V1.Boundary.Response;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Boundary
{
    [TestFixture]
    public class GetServiceResponseTests
    {
        [Test]
        public void GetServiceResponseObjectShouldHaveCorrectProperties()
        {
            var entityType = typeof(GetServiceResponse);
            entityType.GetProperties().Length.Should().Be(13);
            var entity = Randomm.Create<GetServiceResponse>();
            Assert.That(entity, Has.Property("Id").InstanceOf(typeof(int)));
            Assert.That(entity, Has.Property("Name").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Categories").InstanceOf(typeof(ICollection<Category>)));
            Assert.That(entity, Has.Property("Contact").InstanceOf(typeof(Contact)));
            Assert.That(entity, Has.Property("Demographic").InstanceOf(typeof(ICollection<Demographic>)));
            Assert.That(entity, Has.Property("Description").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Images").InstanceOf(typeof(Image)));
            Assert.That(entity, Has.Property("Locations").InstanceOf(typeof(ICollection<Location>)));
            Assert.That(entity, Has.Property("Organization").InstanceOf(typeof(Organization)));
            Assert.That(entity, Has.Property("Referral").InstanceOf(typeof(Referral)));
            Assert.That(entity, Has.Property("Social").InstanceOf(typeof(Social)));
            Assert.That(entity, Has.Property("Status").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Distance").InstanceOf(typeof(string)));
        }

        [Test]
        public void GetServiceResponseCategoryObjectShouldHaveCorrectProperties()
        {
            var entityType = typeof(Category);
            entityType.GetProperties().Length.Should().Be(5);
            var entity = Randomm.Create<Category>();
            Assert.That(entity, Has.Property("Id").InstanceOf(typeof(int)));
            Assert.That(entity, Has.Property("Name").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Description").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Vocabulary").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Weight").InstanceOf(typeof(int)));
        }

        [Test]
        public void GetServiceResponseContactObjectShouldHaveCorrectProperties()
        {
            var entityType = typeof(Contact);
            entityType.GetProperties().Length.Should().Be(3);
            var entity = Randomm.Create<Contact>();
            Assert.That(entity, Has.Property("Email").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Telephone").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Website").InstanceOf(typeof(string)));
        }

        [Test]
        public void GetServiceResponseDemographicObjectShouldHaveCorrectProperties()
        {
            var entityType = typeof(Demographic);
            entityType.GetProperties().Length.Should().Be(3);
            var entity = Randomm.Create<Demographic>();
            Assert.That(entity, Has.Property("Id").InstanceOf(typeof(int)));
            Assert.That(entity, Has.Property("Name").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Vocabulary").InstanceOf(typeof(string)));
        }

        [Test]
        public void GetServiceResponseImageObjectShouldHaveCorrectProperties()
        {
            var entityType = typeof(Image);
            entityType.GetProperties().Length.Should().Be(2);
            var entity = Randomm.Create<Image>();
            Assert.That(entity, Has.Property("Medium").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Original").InstanceOf(typeof(string)));
        }

        [Test]
        public void GetServiceResponseLocationObjectShouldHaveCorrectProperties()
        {
            var entityType = typeof(Location);
            entityType.GetProperties().Length.Should().Be(9);
            var entity = Randomm.Create<Location>();
            Assert.That(entity, Has.Property("Latitude").InstanceOf(typeof(decimal)));
            Assert.That(entity, Has.Property("Longitude").InstanceOf(typeof(decimal)));
            Assert.That(entity, Has.Property("Uprn").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Address1").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Address2").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("City").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("StateProvince").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("PostalCode").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Country").InstanceOf(typeof(string)));
        }

        [Test]
        public void GetServiceResponseOrganizationObjectShouldHaveCorrectProperties()
        {
            var entityType = typeof(Organization);
            entityType.GetProperties().Length.Should().Be(3);
            var entity = Randomm.Create<Organization>();
            Assert.That(entity, Has.Property("Id").InstanceOf(typeof(int)));
            Assert.That(entity, Has.Property("Name").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Status").InstanceOf(typeof(string)));
        }

        [Test]
        public void GetServiceResponseReferralObjectShouldHaveCorrectProperties()
        {
            var entityType = typeof(Referral);
            entityType.GetProperties().Length.Should().Be(2);
            var entity = Randomm.Create<Referral>();
            Assert.That(entity, Has.Property("Email").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Website").InstanceOf(typeof(string)));
        }

        [Test]
        public void GetServiceResponseSocialObjectShouldHaveCorrectProperties()
        {
            var entityType = typeof(Social);
            entityType.GetProperties().Length.Should().Be(4);
            var entity = Randomm.Create<Social>();
            Assert.That(entity, Has.Property("Facebook").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Twitter").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Instagram").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Linkedin").InstanceOf(typeof(string)));
        }

    }
}
