using System;
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

        [TestCase(TestName = "Given search parameters when the SearchService method is called it returns records matching applied synonym group")]
        public void GivenSearchParametersWhenSearchServicesGatewayMethodIsCalledThenItReturnsMatchingSynonymGroupResults()
        {
            // arrange
            var synonymGroup1 = EntityHelpers.CreateSynonymGroupWithWords(5);
            var synonymGroup2 = EntityHelpers.CreateSynonymGroupWithWords(3);
            synonymGroup2.SynonymWords.ToList()[1].Word = synonymGroup1.SynonymWords.ToList()[1].Word;
            var services = EntityHelpers.CreateServices();
            var serviceToFind1 = EntityHelpers.CreateService();
            var serviceToFind2 = EntityHelpers.CreateService();
            var searchTerm = synonymGroup1.SynonymWords.ToList()[1].Word;
            var requestParams = new SearchServicesRequest();
            requestParams.Search = searchTerm;
            serviceToFind1.Name += synonymGroup1.SynonymWords.ToList()[4].Word;
            serviceToFind2.Name += synonymGroup2.SynonymWords.ToList()[2].Word;
            DatabaseContext.SynonymGroups.Add(synonymGroup1);
            DatabaseContext.SynonymGroups.Add(synonymGroup2);
            DatabaseContext.Services.AddRange(services);
            DatabaseContext.Services.Add(serviceToFind1);
            DatabaseContext.Services.Add(serviceToFind2);
            DatabaseContext.SaveChanges();
            var expectedData = new List<Service>();
            expectedData.Add(serviceToFind1);
            expectedData.Add(serviceToFind2);

            // act
            var gatewayResult = _classUnderTest.SearchServices(requestParams);

            // assert
            gatewayResult.Should().NotBeNull();
            gatewayResult.Count.Should().Be(expectedData.Count);
        }
        #endregion
    }
}
