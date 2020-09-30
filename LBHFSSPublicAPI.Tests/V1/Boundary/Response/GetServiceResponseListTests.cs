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
            // arrange
            var entityType = typeof(GetServiceResponseList);

            // act
            var entity = Randomm.Create<GetServiceResponseList>();

            // assert
            entityType.GetProperties().Length.Should().Be(2);
            Assert.That(entity, Has.Property("Services").InstanceOf(typeof(List<Service>)));
            Assert.That(entity, Has.Property("Metadata").InstanceOf(typeof(Metadata)));
        }
    }
}
