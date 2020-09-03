using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using LBHFSSPublicAPI.V1.Boundary.Response;
using LBHFSSPublicAPI.V1.Infrastructure;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.E2ETests
{
    [TestFixture]
    public class GetTaxonomies : IntegrationTests<Startup>
    {
        private Fixture _fixture = new Fixture();

        [Test]
        public async Task GetTaxonomiesReturnsTaxonomies()
        {
            var taxonomies = _fixture.CreateMany<Taxonomy>().ToList();
            DatabaseContext.Taxonomies.AddRange(taxonomies);
            DatabaseContext.SaveChanges();
            var requestUri = new Uri("api/v1/taxonomies", UriKind.Relative);
            var response = Client.GetAsync(requestUri).Result;
            response.StatusCode.Should().Be(200);
            var content = response.Content;
            var stringResponse = await content.ReadAsStringAsync().ConfigureAwait(true);
            var deserializedBody = JsonConvert.DeserializeObject<TaxonomyResponse>(stringResponse);
            deserializedBody.Taxonomies.Count.Should().Be(taxonomies.Count);
        }
    }
}
