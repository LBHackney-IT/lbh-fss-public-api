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
            var formulas = EntityHelpers.CreateServices().ToList();
            var expectedResponse = new GetServiceResponseList();
            var serviceToFind1 = EntityHelpers.CreateService();
            var serviceToFind2 = EntityHelpers.CreateService();
            var searchTerm = Randomm.Text();
            serviceToFind1.Name += searchTerm;
            serviceToFind2.Name += searchTerm;
            expectedResponse.Services.Add(serviceToFind1.ToDomain().ToResponse());
            expectedResponse.Services.Add(serviceToFind2.ToDomain().ToResponse());
            await DatabaseContext.Services.AddRangeAsync(formulas).ConfigureAwait(true);
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
    }
}
