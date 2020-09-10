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
using System.Reflection;
using NUnit.Framework.Internal;
using NUnit.Framework.Interfaces;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.Tests.TestHelpers;
using LBHFSSPublicAPI.V1.Boundary;

namespace LBHFSSPublicAPI.Tests.V1.E2ETests
{
    [TestFixture]
    public class GetTaxonomies : IntegrationTests<Startup>
    {
        private Fixture _fixture = new Fixture();

        #region Get Taxonomies with/without filter

        [Test]
        public async Task GivenRequestWithNoFilterParamterWhenGetTaxonomiesEndpointIsCalledThenItReturnsAllTaxonomies()
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

        // An experiment to give tests more readable names: (It works, but bit hacky)
        //[TestCase("", TestName = "Given request with filter parameter, When get taxonomies endpoint is called, Then it returns only filtered taxonomies.")]
        [Test]
        public async Task GivenRequestWithFilterParameterWhenGetTaxonomiesEndpointIsCalledThenItReturnsOnlyFilteredTaxonomies()
        {
            // arrange
            var vocabularyFP = _fixture.Create<string>();

            var taxonomies = _fixture.CreateMany<Taxonomy>(5).ToList();
            taxonomies[1].Vocabulary = vocabularyFP;
            taxonomies[3].Vocabulary = vocabularyFP;
            DatabaseContext.Taxonomies.AddRange(taxonomies);
            DatabaseContext.SaveChanges();

            // act
            var requestUri = new Uri($"api/v1/taxonomies?vocabulary={vocabularyFP}", UriKind.Relative);
            var response = Client.GetAsync(requestUri).Result;

            var content = response.Content;
            var stringResponse = await content.ReadAsStringAsync().ConfigureAwait(true);
            var deserializedBody = JsonConvert.DeserializeObject<TaxonomyResponse>(stringResponse);

            // assert
            response.StatusCode.Should().Be(200);
            deserializedBody.Taxonomies.Count.Should().Be(2);
        }

        #endregion

        #region Get Single Taxonomy by Id

        [Test]
        public async Task GivenRequestWithIdWhenGetTaxonomyEndpointIsCalledThenItReturnsSingleMatchingTaxonomy()
        {
            // arrange
            var taxonomies = _fixture.CreateMany<Taxonomy>().ToList();
            DatabaseContext.Taxonomies.AddRange(taxonomies);
            DatabaseContext.SaveChanges();

            var matchTaxonomy = DatabaseContext.Taxonomies.FirstOrDefault();
            var expectedId = matchTaxonomy.Id;

            // act
            var requestUri = new Uri($"api/v1/taxonomies/{expectedId}", UriKind.Relative);
            var response = Client.GetAsync(requestUri).Result;

            var content = response.Content;
            var stringResponse = await content.ReadAsStringAsync().ConfigureAwait(true);
            var deserializedBody = JsonConvert.DeserializeObject<TaxonomyEntity>(stringResponse);

            // assert
            response.StatusCode.Should().Be(200);
            deserializedBody.Should().BeEquivalentTo(matchTaxonomy);
        }

        [Test]
        public async Task GivenRequestWithIdThatDoesNotHaveAMatchWhenGetTaxonomyEndpointIsCalledThenItReturnsA404Response()
        {
            // arrange
            var taxonomies = _fixture.CreateMany<Taxonomy>().ToList();
            DatabaseContext.Taxonomies.AddRange(taxonomies);
            DatabaseContext.SaveChanges();

            var nonMatchingId = Randomm.Id();
            var expectedValue = new ErrorResponse($"Taxonomy with an Id: {nonMatchingId} was not found.");

            // act
            var requestUri = new Uri($"api/v1/taxonomies/{nonMatchingId}", UriKind.Relative);
            var response = Client.GetAsync(requestUri).Result;

            var content = response.Content;
            var stringResponse = await content.ReadAsStringAsync().ConfigureAwait(true);
            var deserializedBody = JsonConvert.DeserializeObject<ErrorResponse>(stringResponse);

            // assert
            response.StatusCode.Should().Be(404);
            deserializedBody.Should().BeEquivalentTo(expectedValue);
        }

        #endregion
    }
}
