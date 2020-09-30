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
            // arrange
            var entityType = typeof(GetServiceResponse);

            // act
            var entity = Randomm.Create<GetServiceResponse>();

            // assert
            entityType.GetProperties().Length.Should().Be(2);
            Assert.That(entity, Has.Property("Service").InstanceOf(typeof(Service)));
            Assert.That(entity, Has.Property("Metadata").InstanceOf(typeof(Metadata)));
        }
    }
}
