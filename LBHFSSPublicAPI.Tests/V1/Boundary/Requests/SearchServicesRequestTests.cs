using System;
using System.Collections.Generic;
using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using LBHFSSPublicAPI.V1.Boundary.Request;
using LBHFSSPublicAPI.V1.Boundary.Response;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Boundary
{
    [TestFixture]
    public class SearchServicesRequestTests
    {
        [Test]
        public void GetServiceResponseObjectShouldHaveCorrectProperties()
        {
            var entityType = typeof(SearchServicesRequest);
            entityType.GetProperties().Length.Should().Be(6);
            var entity = Randomm.Create<SearchServicesRequest>();
            Assert.That(entity, Has.Property("Search").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Sort").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Offset").InstanceOf(typeof(int)));
            Assert.That(entity, Has.Property("Limit").InstanceOf(typeof(int)));
            Assert.That(entity, Has.Property("TaxonomyId").InstanceOf(typeof(int)));
            Assert.That(entity, Has.Property("PostCode").InstanceOf(typeof(string)));
        }
    }
}
