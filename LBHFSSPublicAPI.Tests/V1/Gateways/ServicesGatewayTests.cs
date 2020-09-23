using System.Linq;
using LBHFSSPublicAPI.V1.Gateways;
using FluentAssertions;
using LBHFSSPublicAPI.V1.Infrastructure;
using NUnit.Framework;
using LBHFSSPublicAPI.Tests.TestHelpers;

namespace LBHFSSPublicAPI.Tests.V1.Gateways
{
    [TestFixture]
    public class ServicesGatewayTests : DatabaseTests
    {
        private ServicesGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new ServicesGateway(DatabaseContext);
        }

        #region Get Single Service by Id

        [TestCase(TestName = "Given a valid id that has a match when the gateway is called the gateway will return a service")]
        public void GivenIdThatHasAMatchWhenGatewayMethodIsCalledThenItReturnsMatchingServiceDomainObject()
        {
            // arrange
            var services = EntityHelpers.CreateServices();
            DatabaseContext.Services.AddRange(services);
            DatabaseContext.SaveChanges();

            var expectedResult = DatabaseContext.Services.First();
            var expectedId = expectedResult.Id;

            // act
            var gatewayResult = _classUnderTest.GetService(expectedId);

            // assert
            gatewayResult.Should().NotBeNull();
            gatewayResult.Should().BeEquivalentTo(expectedResult);
        }

        [TestCase(TestName = "Given an id that does not exist in the database when the GetService method is called it returns null")]
        public void GivenIdThatDoesNotHaveAMatchWhenGetServiceGatewayMethodIsCalledThenItReturnsNull()
        {
            // arrange
            var services = EntityHelpers.CreateServices();
            DatabaseContext.Services.AddRange(services);
            DatabaseContext.SaveChanges();

            var id = Randomm.Id();

            // act
            var gatewayResult = _classUnderTest.GetService(id);

            // assert
            gatewayResult.Should().BeNull();
        }
        #endregion
    }
}
