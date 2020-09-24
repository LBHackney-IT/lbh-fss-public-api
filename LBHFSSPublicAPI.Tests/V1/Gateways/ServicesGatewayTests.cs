using System.Collections.Generic;
using System.Linq;
using LBHFSSPublicAPI.V1.Gateways;
using FluentAssertions;
using LBHFSSPublicAPI.V1.Infrastructure;
using NUnit.Framework;
using LBHFSSPublicAPI.Tests.TestHelpers;
using LBHFSSPublicAPI.V1.Boundary.Request;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.V1.Factories;

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

        #region Search Services
        [TestCase(TestName = "Given search parameters when the SearchService method is called it returns matching records")]
        public void GivenSearchParametersWhenSearchServicesGatewayMethodIsCalledThenItReturnsMatchingResults()
        {
            // arrange
            var services = EntityHelpers.CreateServices(10);
            var searchTerm = Randomm.Text();
            services.First().Name += searchTerm;
            var expectedData = new List<Service>();
            expectedData.Add(services.First());
            var requestParams = new SearchServicesRequest();
            requestParams.Search = searchTerm;
            DatabaseContext.Services.AddRange(services);
            DatabaseContext.SaveChanges();

            // act
            var gatewayResult = _classUnderTest.SearchServices(requestParams);

            // assert
            gatewayResult.Should().NotBeNull();
            gatewayResult.Count.Should().Be(1);
        }
        #endregion
    }
}
