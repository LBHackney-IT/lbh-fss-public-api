using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using LBHFSSPublicAPI.V1.Infrastructure;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.E2ETests
{
    [TestFixture]
    public class GetTaxonomies : IntegrationTests<Startup>
    {
        private Fixture _fixture = new Fixture();

        [Test]
        public void GetTaxonomiesReturnsTaxonomies()
        {
            var taxonomies = _fixture.CreateMany<Taxonomy>().ToList();
            DatabaseContext.Taxonomies.AddRange(taxonomies);
            var requestUri = new Uri("api/v1/taxonomies", UriKind.Relative);
            var response = Client.GetAsync(requestUri).Result;
            response.StatusCode.Should().Be(200);
        }
    }
}
