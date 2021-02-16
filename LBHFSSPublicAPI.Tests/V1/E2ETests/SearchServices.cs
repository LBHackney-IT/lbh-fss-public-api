using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using LBHFSSPublicAPI.V1.Boundary.Response;
using R = LBHFSSPublicAPI.V1.Boundary.Response;
using LBHFSSPublicAPI.V1.Factories;
using LBHFSSPublicAPI.V1.Infrastructure;
using Newtonsoft.Json;
using NUnit.Framework;
namespace LBHFSSPublicAPI.Tests.V1.E2ETests
{
    [TestFixture]
    public class SearchServices : IntegrationTests<Startup>
    {
        [TestCase(TestName =
            "Given that there are services in the database, if a search parameter is provided, services that match are returned")]
        public async Task GetServicesBySearchParamsReturnServicesIfMatched()
        {
            var services = EntityHelpers.CreateServices().ToList();
            var expectedResponse = new GetServiceResponseList();
            expectedResponse.Services = new List<R.Service>();
            var serviceToFind1 = EntityHelpers.CreateService();
            var serviceToFind2 = EntityHelpers.CreateService();
            var searchTerm = Randomm.Text();
            serviceToFind1.Name += searchTerm;
            serviceToFind2.Name += searchTerm;
            expectedResponse.Services.Add(serviceToFind1.ToDomain().ToResponse().Service);
            expectedResponse.Services.Add(serviceToFind2.ToDomain().ToResponse().Service);
            await DatabaseContext.Services.AddRangeAsync(services).ConfigureAwait(true);
            await DatabaseContext.Services.AddAsync(serviceToFind1).ConfigureAwait(true);
            await DatabaseContext.Services.AddAsync(serviceToFind2).ConfigureAwait(true);
            await DatabaseContext.SaveChangesAsync().ConfigureAwait(true);
            var requestUri = new Uri($"api/v1/services?search={searchTerm}", UriKind.Relative);
            var response = Client.GetAsync(requestUri).Result;
            response.StatusCode.Should().Be(200);
            var content = response.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(false);
            var deserializedBody = JsonConvert.DeserializeObject<GetServiceResponseList>(stringContent);
            deserializedBody.Services.Count.Should().Be(2);
        }

        [TestCase(TestName =
            "Given that there are services in the database, if a search parameter is provided, services that match all synoynyms in synonym group are returned")]
        public async Task GetServicesBySearchParamsReturnServicesIfMatchedToSynonymGroup()
        {
            var synonymGroup1 = EntityHelpers.CreateSynonymGroupWithWords(5);
            var synonymGroup2 = EntityHelpers.CreateSynonymGroupWithWords(3);
            synonymGroup2.SynonymWords.ToList()[1].Word = synonymGroup1.SynonymWords.ToList()[1].Word;
            var services = EntityHelpers.CreateServices();
            var serviceToFind1 = EntityHelpers.CreateService();
            var serviceToFind2 = EntityHelpers.CreateService();
            var searchTerm = synonymGroup1.SynonymWords.ToList()[1].Word;
            serviceToFind1.Name += synonymGroup1.SynonymWords.ToList()[4].Word;
            serviceToFind2.Name += synonymGroup2.SynonymWords.ToList()[2].Word;
            DatabaseContext.SynonymGroups.Add(synonymGroup1);
            DatabaseContext.SynonymGroups.Add(synonymGroup2);
            DatabaseContext.Services.AddRange(services);
            DatabaseContext.Services.Add(serviceToFind1);
            DatabaseContext.Services.Add(serviceToFind2);
            DatabaseContext.SaveChanges();
            var requestUri = new Uri($"api/v1/services?search={searchTerm}", UriKind.Relative);
            var response = Client.GetAsync(requestUri).Result;
            response.StatusCode.Should().Be(200);
            var content = response.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(false);
            var deserializedBody = JsonConvert.DeserializeObject<GetServiceResponseList>(stringContent);
            deserializedBody.Services.Count.Should().Be(2);
        }

        [TestCase(TestName =
            "Given that there are services in the database, if category and demographic taxonomy id search parameters are provided, services with matching category and demographics are returned")]
        public async Task GetServicesByTaxonomyIdServicesIfMatchedToCategoryAndDemographic()
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
            var searchTerm = Randomm.Create<string>();
            serviceToFind1.Name += searchTerm;
            serviceToFind2.Name += searchTerm;
            serviceTaxonomy1.Service = serviceToFind1;
            serviceTaxonomy1.Taxonomy = taxonomy1;
            serviceTaxonomy2.Service = serviceToFind1;
            serviceTaxonomy2.Taxonomy = taxonomy2;
            serviceTaxonomy3.Service = services.First();
            serviceTaxonomy3.Taxonomy = taxonomy2;
            DatabaseContext.Services.AddRange(services);
            DatabaseContext.Services.Add(serviceToFind1);
            DatabaseContext.Services.Add(serviceToFind2);
            DatabaseContext.ServiceTaxonomies.Add(serviceTaxonomy1);
            DatabaseContext.ServiceTaxonomies.Add(serviceTaxonomy2);
            DatabaseContext.ServiceTaxonomies.Add(serviceTaxonomy3);
            DatabaseContext.SaveChanges();
            var requestUri = new Uri($"api/v1/services?search={searchTerm}&taxonomyids={taxonomy1.Id}&taxonomyids={taxonomy2.Id}", UriKind.Relative);
            var response = Client.GetAsync(requestUri).Result;
            response.StatusCode.Should().Be(200);
            var content = response.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(false);
            var deserializedBody = JsonConvert.DeserializeObject<GetServiceResponseList>(stringContent);
            deserializedBody.Services.Count.Should().Be(1);
        }

        [TestCase(TestName =
            "Given that there are services in the database, if category and demographic taxonomy id search parameters are provided and no services match both, nothing is returned")]
        public async Task GetServicesByTaxonomyIdServicesIfMatchedToTaxonomyId()
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
            var serviceTaxonomy3 = EntityHelpers.CreateServiceTaxonomy();
            var searchTerm = Randomm.Create<string>();
            serviceToFind1.Name += searchTerm;
            serviceToFind2.Name += searchTerm;
            serviceTaxonomy1.Service = serviceToFind1;
            serviceTaxonomy1.Taxonomy = taxonomy1;
            serviceTaxonomy2.Service = serviceToFind2;
            serviceTaxonomy2.Taxonomy = taxonomy2;
            serviceTaxonomy3.Service = services.First();
            serviceTaxonomy3.Taxonomy = taxonomy2;
            DatabaseContext.Services.AddRange(services);
            DatabaseContext.Services.Add(serviceToFind1);
            DatabaseContext.Services.Add(serviceToFind2);
            DatabaseContext.ServiceTaxonomies.Add(serviceTaxonomy1);
            DatabaseContext.ServiceTaxonomies.Add(serviceTaxonomy2);
            DatabaseContext.ServiceTaxonomies.Add(serviceTaxonomy3);
            DatabaseContext.Taxonomies.Add(taxonomy3);
            DatabaseContext.SaveChanges();
            var requestUri = new Uri($"api/v1/services?search={searchTerm}&taxonomyids={taxonomy2.Id}&taxonomyids={taxonomy3.Id}", UriKind.Relative);
            var response = Client.GetAsync(requestUri).Result;
            response.StatusCode.Should().Be(200);
            var content = response.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(false);
            var deserializedBody = JsonConvert.DeserializeObject<GetServiceResponseList>(stringContent);
            deserializedBody.Services.Count.Should().Be(0);
        }

        [TestCase(TestName =
            "Given that there are services in the database, if a url encoded search parameter is provided, services that match unencoded search term are returned")]
        public async Task GetServicesByUrlencodedSearchParamsReturnServicesIfMatched()
        {
            var services = EntityHelpers.CreateServices().ToList();
            var expectedResponse = new GetServiceResponseList();
            expectedResponse.Services = new List<R.Service>();
            var serviceToFind1 = EntityHelpers.CreateService();
            var serviceToFind2 = EntityHelpers.CreateService();
            var searchTerm = Randomm.Text();
            var urlencodedSearch = searchTerm.Replace(" ", "%2520");
            serviceToFind1.Name += searchTerm;
            serviceToFind2.Name += searchTerm;
            expectedResponse.Services.Add(serviceToFind1.ToDomain().ToResponseService());
            expectedResponse.Services.Add(serviceToFind2.ToDomain().ToResponseService());
            await DatabaseContext.Services.AddRangeAsync(services).ConfigureAwait(true);
            await DatabaseContext.Services.AddAsync(serviceToFind1).ConfigureAwait(true);
            await DatabaseContext.Services.AddAsync(serviceToFind2).ConfigureAwait(true);
            await DatabaseContext.SaveChangesAsync().ConfigureAwait(true);
            var requestUri = new Uri($"api/v1/services?search={urlencodedSearch}", UriKind.Relative);
            var response = Client.GetAsync(requestUri).Result;
            response.StatusCode.Should().Be(200);
            var content = response.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(false);
            var deserializedBody = JsonConvert.DeserializeObject<GetServiceResponseList>(stringContent);
            deserializedBody.Services.Count.Should().Be(2);
        }

        [TestCase(TestName =
            "Given that there are services in the database, if a taxonomy only params are provided, services that match are returned")]
        public async Task GetServicesByTaxonomyParamsReturnServicesIfMatched()
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
            var searchTerm = Randomm.Create<string>();
            serviceToFind1.Name += searchTerm;
            serviceToFind2.Name += searchTerm;
            serviceTaxonomy1.Service = serviceToFind1;
            serviceTaxonomy1.Taxonomy = taxonomy1;
            serviceTaxonomy2.Service = serviceToFind1;
            serviceTaxonomy2.Taxonomy = taxonomy2;
            serviceTaxonomy3.Service = services.First();
            serviceTaxonomy3.Taxonomy = taxonomy2;
            DatabaseContext.Services.AddRange(services);
            DatabaseContext.Services.Add(serviceToFind1);
            DatabaseContext.Services.Add(serviceToFind2);
            DatabaseContext.ServiceTaxonomies.Add(serviceTaxonomy1);
            DatabaseContext.ServiceTaxonomies.Add(serviceTaxonomy2);
            DatabaseContext.ServiceTaxonomies.Add(serviceTaxonomy3);
            DatabaseContext.SaveChanges();
            var requestUri = new Uri($"api/v1/services?taxonomyids={taxonomy1.Id}&taxonomyids={taxonomy2.Id}", UriKind.Relative);
            var response = Client.GetAsync(requestUri).Result;
            response.StatusCode.Should().Be(200);
            var content = response.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(false);
            var deserializedBody = JsonConvert.DeserializeObject<GetServiceResponseList>(stringContent);
            deserializedBody.Services.Count.Should().Be(1);
        }

        [TestCase(TestName =
            "Given that there are services in the database, if a taxonomy only params are provided, the matches are returned in the appropriate rank.")]
        public async Task SearchServicesReturnServicesAccordingToRankIfMatched()
        {
            var searchWord1 = Randomm.Word();
            var searchWord2 = Randomm.Word();
            var irrelevantWord = Randomm.Word();
            var bridgeSyn1Word = Utility.SuperSetOfString(searchWord1);
            var bridgeSyn2Word = Utility.SuperSetOfString(searchWord2);
            var synWord1 = Randomm.Word();
            var synWord2 = Randomm.Word();
            var synWord3 = Randomm.Word();
            var synonymGroup1 = EntityHelpers.CreateSynonymGroupWithWords();
            var synonymGroup2 = EntityHelpers.CreateSynonymGroupWithWords();
            var dummySynGroup = EntityHelpers.CreateSynonymGroupWithWords();
            var bridgeSynonym1 = EntityHelpers.SynWord(synonymGroup1, bridgeSyn1Word);
            var bridgeSynonym2 = EntityHelpers.SynWord(synonymGroup2, bridgeSyn2Word);
            var matchSynonym1 = EntityHelpers.SynWord(synonymGroup1, synWord1);
            var matchSynonym2 = EntityHelpers.SynWord(synonymGroup1, synWord2);
            var matchSynonym3 = EntityHelpers.SynWord(synonymGroup2, synWord3);
            synonymGroup1.SynonymWords.Add(bridgeSynonym1);
            synonymGroup2.SynonymWords.Add(bridgeSynonym2);
            synonymGroup1.SynonymWords.Add(matchSynonym1);
            synonymGroup1.SynonymWords.Add(matchSynonym2);
            synonymGroup2.SynonymWords.Add(matchSynonym3);
            var services = EntityHelpers.CreateServices(5);
            var matchService1 = EntityHelpers.CreateService();
            var matchService2 = EntityHelpers.CreateService();
            var matchService3 = EntityHelpers.CreateService();
            var matchService4 = EntityHelpers.CreateService();
            matchService1.Name += searchWord2;
            //matchService2.Description += synWord2;
            matchService2.Description += " " + synWord2; //15 Feb 2021 - Change made so we only search for whole words in the service description! - So we add a space.

            matchService3.Organization.Name += synWord3;
            matchService4.Organization.Name += searchWord1;
            services.AddMany(matchService1, matchService2, matchService3, matchService4);
            DatabaseContext.SynonymGroups.AddRange(synonymGroup1);
            DatabaseContext.SynonymGroups.AddRange(synonymGroup2);
            DatabaseContext.SynonymGroups.AddRange(dummySynGroup);
            DatabaseContext.Services.AddRange(services);
            DatabaseContext.SaveChanges();

            var requestUri = new Uri($"api/v1/services?search={searchWord1} {searchWord2} {irrelevantWord}", UriKind.Relative);
            var response = Client.GetAsync(requestUri).Result;
            response.StatusCode.Should().Be(200);
            var content = response.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(false);
            var deserializedBody = JsonConvert.DeserializeObject<GetServiceResponseList>(stringContent);
            deserializedBody.Services.Count.Should().Be(4);
            deserializedBody.Services[0].Name.Should().Be(matchService4.Name);
            deserializedBody.Services[1].Name.Should().Be(matchService1.Name);
            deserializedBody.Services[2].Name.Should().Be(matchService3.Name);
            deserializedBody.Services[3].Name.Should().Be(matchService2.Name);
        }
    }
}
