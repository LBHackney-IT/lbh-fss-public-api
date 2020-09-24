using System;
using System.Collections.Generic;
using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using LBHFSSPublicAPI.V1.Boundary.Response;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Boundary
{
    [TestFixture]
    public class GetServiceResponseListTests
    {
        [TestCase(TestName = "GetServicesResponseList object should have the correct properties")]
        public void GetServiceResponseObjectShouldHaveCorrectProperties()
        {
            var entityType = typeof(GetServiceResponseList);
            entityType.GetProperties().Length.Should().Be(2);
            var entity = Randomm.Create<GetServiceResponseList>();
            Assert.That(entity, Has.Property("Services").InstanceOf(typeof(ICollection<GetServiceResponse>)));
            Assert.That(entity, Has.Property("Metadata").InstanceOf(typeof(ServicesResponseMetadata)));
        }

        [TestCase(TestName = "GetServicesResponseList object should have the correct properties")]
        public void ServicesResponseMetadataObjectShouldHaveCorrectProperties()
        {
            var entityType = typeof(ServicesResponseMetadata);
            entityType.GetProperties().Length.Should().Be(3);
            var entity = Randomm.Create<ServicesResponseMetadata>();
            Assert.That(entity, Has.Property("PostCode").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("PostCodeLatitude").InstanceOf(typeof(decimal)));
            Assert.That(entity, Has.Property("PostCodeLongitude").InstanceOf(typeof(decimal)));
        }
    }
}
