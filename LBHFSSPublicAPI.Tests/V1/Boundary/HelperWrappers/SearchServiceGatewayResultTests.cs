using System;
using System.Collections.Generic;
using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using LBHFSSPublicAPI.V1.Boundary.HelperWrappers;
using LBHFSSPublicAPI.V1.Domain;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Boundary.HelperWrappers
{
    [TestFixture]
    public class SearchServiceGatewayResultTests
    {
        [Test]
        public void SearchServiceGatewayResultObjectShouldHaveCorrectProperties()
        {
            // arrange
            var wrapperType = typeof(SearchServiceGatewayResult);
            var expectedServicesType = typeof(List<ServiceEntity>);

            // act
            var entity = Randomm.Create<SearchServiceGatewayResult>();

            // assert
            wrapperType.GetProperties().Length.Should().Be(2);

            Assert.That(entity, Has.Property("FullMatchServices").InstanceOf(expectedServicesType));
            Assert.That(entity, Has.Property("SplitMatchServices").InstanceOf(expectedServicesType));
        }
    }
}
