using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using LBHFSSPublicAPI.V1.Boundary.Response;
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
            var serviceToFind1 = EntityHelpers.CreateService();
            var serviceToFind2 = EntityHelpers.CreateService();
            var searchTerm = Randomm.Text();
            serviceToFind1.Name += searchTerm;
            serviceToFind2.Name += searchTerm;
            expectedResponse.Services.Add(serviceToFind1.ToDomain().ToResponse());
            expectedResponse.Services.Add(serviceToFind2.ToDomain().ToResponse());
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
            "Given that there are services in the database, if a taxonomy id search parameter is provided, services with taxonomy with id")]
        public async Task GetServicesByTaxonomyIdServicesIfMatchedToTaxonomyId()
        {
            var taxonomy1 = EntityHelpers.CreateTaxonomy();
            var taxonomy2 = EntityHelpers.CreateTaxonomy();
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
            DatabaseContext.SaveChanges();
            var requestUri = new Uri($"api/v1/services?search={searchTerm}&taxonomyids={taxonomy1.Id}&taxonomyids={taxonomy2.Id}", UriKind.Relative);
            var response = Client.GetAsync(requestUri).Result;
            response.StatusCode.Should().Be(200);
            var content = response.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(false);
            var deserializedBody = JsonConvert.DeserializeObject<GetServiceResponseList>(stringContent);
            deserializedBody.Services.Count.Should().Be(2);
        }
    }
}
