using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using LBHFSSPublicAPI.V1.Boundary.Response;
using LBHFSSPublicAPI.V1.Infrastructure;
using Newtonsoft.Json;
using NUnit.Framework;
namespace LBHFSSPublicAPI.Tests.V1.E2ETests
{
    [TestFixture]
    public class GetServices : IntegrationTests<Startup>
    {
        [Test]
        public async Task ReturnsThatMatchingService()
        {
            // arrange
            var service = EntityHelpers.CreateService();
            DatabaseContext.Services.Add(service);
            DatabaseContext.SaveChanges();
            var expectedService = DatabaseContext.Services.FirstOrDefault();
            var searchSearviceId = expectedService.Id;
            // act
            var requestUri = new Uri($"api/v1/services/{searchSearviceId}?postcode={Randomm.Postcode()}", UriKind.Relative);
            var response = Client.GetAsync(requestUri).Result;
            var content = response.Content;
            var stringResponse = await content.ReadAsStringAsync().ConfigureAwait(true);
            var actualService = JsonConvert.DeserializeObject<GetServiceResponse>(stringResponse);
            // assert
            response.StatusCode.Should().Be(200);
            actualService.Service.Id.Should().Be(expectedService.Id);
            actualService.Service.Status.Should().Be(service.Status);
        }

        [Test]
        public async Task Returns404IfNoMatchingService()
        {
            // act
            var requestUri = new Uri($"api/v1/services/{Randomm.Id(1, 10)}", UriKind.Relative);
            var response = Client.GetAsync(requestUri).Result;
            var content = response.Content;
            var stringResponse = await content.ReadAsStringAsync().ConfigureAwait(true);
            var actualService = JsonConvert.DeserializeObject<GetServiceResponse>(stringResponse);
            // assert
            response.StatusCode.Should().Be(404);
        }
    }
}
