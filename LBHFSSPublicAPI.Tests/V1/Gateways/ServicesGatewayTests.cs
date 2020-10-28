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

        [TestCase(TestName = "When SearchService Service controller method is called, Then it returns all ACTIVE services")] // ignores deleted ones // Assuming no default pagination - there's none yet
        public void SearchServiceEndpointReturnsAllActiveServices() // behaviour does not depend on filters
        {
            // arrange
            var services = EntityHelpers.CreateServices(10).ToList();
            List<ServiceEntity> expectedData = services.Select(s => s.ToDomain()).ToList();

            var deletedService = EntityHelpers.CreateService();
            deletedService.Status = "deleted";
            deletedService.OrganizationId = null;
            deletedService.Organization = null;
            services.Add(deletedService);                           // added 11th service (broken)

            DatabaseContext.Services.AddRange(services);
            DatabaseContext.SaveChanges();

            // act
            var gatewayResult = _classUnderTest.SearchServices(new SearchServicesRequest()).ToList();

            // assert
            gatewayResult.Should().NotBeNull();
            gatewayResult.Should().BeOfType<List<ServiceEntity>>();
            gatewayResult.Count.Should().Be(10);
            gatewayResult.Should().NotContain(s => s.Status == "deleted");
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

        [TestCase(TestName = "Given multiple taxonomy id search parameters when the SearchService method is called it returns records matching taxonomy ids")]
        public void GivenMultipleTaxonomyIdSearchParametersWhenSearchServicesGatewayMethodIsCalledThenItReturnsMatchingTaxonomyIdResults()
        {
            var taxonomy1 = EntityHelpers.CreateTaxonomy();
            var taxonomy2 = EntityHelpers.CreateTaxonomy();
            taxonomy1.Vocabulary = "demographic";
            taxonomy2.Vocabulary = "category";
            var services = EntityHelpers.CreateServices();
            var serviceToFind1 = EntityHelpers.CreateService();
            var serviceToFind2 = EntityHelpers.CreateService();
            var serviceTaxonomy1 = EntityHelpers.CreateServiceTaxonomy();
            var serviceTaxonomy2 = EntityHelpers.CreateServiceTaxonomy();
            var serviceTaxonomy3 = EntityHelpers.CreateServiceTaxonomy();
            var serviceTaxonomy4 = EntityHelpers.CreateServiceTaxonomy();
            serviceTaxonomy1.Service = serviceToFind1;
            serviceTaxonomy1.Taxonomy = taxonomy1;
            serviceTaxonomy2.Service = serviceToFind1;
            serviceTaxonomy2.Taxonomy = taxonomy2;
            serviceTaxonomy3.Service = serviceToFind2;
            serviceTaxonomy3.Taxonomy = taxonomy1;
            serviceTaxonomy4.Service = serviceToFind2;
            serviceTaxonomy4.Taxonomy = taxonomy2;
            DatabaseContext.Services.AddRange(services);
            DatabaseContext.Services.Add(serviceToFind1);
            DatabaseContext.Services.Add(serviceToFind2);
            DatabaseContext.ServiceTaxonomies.Add(serviceTaxonomy1);
            DatabaseContext.ServiceTaxonomies.Add(serviceTaxonomy2);
            DatabaseContext.ServiceTaxonomies.Add(serviceTaxonomy3);
            DatabaseContext.ServiceTaxonomies.Add(serviceTaxonomy4);
            DatabaseContext.SaveChanges();
            var requestParams = new SearchServicesRequest();
            requestParams.TaxonomyIds = new List<int> { taxonomy1.Id, taxonomy2.Id };
            var expectedData = new List<Service>();
            expectedData.Add(serviceToFind1);
            expectedData.Add(serviceToFind2);
            var gatewayResult = _classUnderTest.SearchServices(requestParams);
            gatewayResult.Should().NotBeNull();
            gatewayResult.Count.Should().Be(expectedData.Count);
        }

        [TestCase(TestName =
    "Given that there are services in the database, if either category or demographic taxonomy id search parameters is provided services with matching taxonomy are returned")]
        public void GivenSingleTaxonomyIdSearchParametersWhenSearchServicesGatewayMethodIsCalledThenItReturnsResults()
        {
            var taxonomy1 = EntityHelpers.CreateTaxonomy();
            var taxonomy2 = EntityHelpers.CreateTaxonomy();
            var taxonomy3 = EntityHelpers.CreateTaxonomy();
            taxonomy1.Vocabulary = "demographic";
            taxonomy2.Vocabulary = "category";
            taxonomy3.Vocabulary = "demographic";
            var services = EntityHelpers.CreateServices();
            var serviceToFind1 = EntityHelpers.CreateService();
            var serviceToFind2 = EntityHelpers.CreateService();
            var serviceTaxonomy1 = EntityHelpers.CreateServiceTaxonomy();
            var serviceTaxonomy2 = EntityHelpers.CreateServiceTaxonomy();
            serviceTaxonomy1.Service = serviceToFind1;
            serviceTaxonomy1.Taxonomy = taxonomy1;
            serviceTaxonomy2.Service = serviceToFind2;
            serviceTaxonomy2.Taxonomy = taxonomy1;
            DatabaseContext.Services.AddRange(services);
            DatabaseContext.Services.Add(serviceToFind1);
            DatabaseContext.Services.Add(serviceToFind2);
            DatabaseContext.ServiceTaxonomies.Add(serviceTaxonomy1);
            DatabaseContext.ServiceTaxonomies.Add(serviceTaxonomy2);
            DatabaseContext.Taxonomies.Add(taxonomy3);
            DatabaseContext.SaveChanges();
            var requestParams = new SearchServicesRequest();
            requestParams.TaxonomyIds = new List<int> { taxonomy1.Id };
            var expectedData = new List<Service>();
            expectedData.Add(serviceToFind1);
            expectedData.Add(serviceToFind2);
            var gatewayResult = _classUnderTest.SearchServices(requestParams);
            gatewayResult.Should().NotBeNull();
            gatewayResult.Count.Should().Be(expectedData.Count);
        }

        [TestCase(TestName =
            "Given that there are services in the database, if category and demographic taxonomy id search parameters are provided and no services match both, nothing is returned")]
        public void GivenTaxonomyIdSearchParametersWhenSearchServicesGatewayMethodIsCalledThenItReturnsNothing()
        {
            var taxonomy1 = EntityHelpers.CreateTaxonomy();
            var taxonomy2 = EntityHelpers.CreateTaxonomy();
            var taxonomy3 = EntityHelpers.CreateTaxonomy();
            taxonomy1.Vocabulary = "demographic";
            taxonomy2.Vocabulary = "category";
            taxonomy3.Vocabulary = "demographic";
            var services = EntityHelpers.CreateServices();
            var serviceToFind1 = EntityHelpers.CreateService();
            var serviceToFind2 = EntityHelpers.CreateService();
            var serviceTaxonomy1 = EntityHelpers.CreateServiceTaxonomy();
            var serviceTaxonomy2 = EntityHelpers.CreateServiceTaxonomy();
            serviceTaxonomy1.Service = serviceToFind1;
            serviceTaxonomy1.Taxonomy = taxonomy1;
            serviceTaxonomy2.Service = serviceToFind2;
            serviceTaxonomy2.Taxonomy = taxonomy2;
            DatabaseContext.Services.AddRange(services);
            DatabaseContext.Services.Add(serviceToFind1);
            DatabaseContext.Services.Add(serviceToFind2);
            DatabaseContext.ServiceTaxonomies.Add(serviceTaxonomy1);
            DatabaseContext.ServiceTaxonomies.Add(serviceTaxonomy2);
            DatabaseContext.Taxonomies.Add(taxonomy3);
            DatabaseContext.SaveChanges();
            var requestParams = new SearchServicesRequest();
            requestParams.TaxonomyIds = new List<int> { taxonomy2.Id, taxonomy3.Id };
            var gatewayResult = _classUnderTest.SearchServices(requestParams);
            gatewayResult.Should().NotBeNull();
            gatewayResult.Count.Should().Be(0);
        }
        #endregion
    }
}
