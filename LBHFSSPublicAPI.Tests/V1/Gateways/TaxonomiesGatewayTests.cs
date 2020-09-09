using System.Linq;
using AutoFixture;
using LBHFSSPublicAPI.Tests.V1.Helper;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.V1.Gateways;
using FluentAssertions;
using LBHFSSPublicAPI.V1.Infrastructure;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Gateways
{
    [TestFixture]
    public class TaxonomiesGatewayTests : DatabaseTests
    {
        private readonly Fixture _fixture = new Fixture();
        private TaxonomiesGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new TaxonomiesGateway(DatabaseContext);
        }

        [Test]
        public void GetTaxonomiesReturnsTaxonomies() // all
        {
            var entity = _fixture.Create<Taxonomy>();
            DatabaseContext.Taxonomies.Add(entity);
            DatabaseContext.SaveChanges();
            var response = _classUnderTest.GetTaxonomies(null).ToList();
            response.First().Name.Should().Be(entity.Name);
        }

        [Test]
        public void GetTaxonomiesWhenDbIsEmptyReturnsAnEmptyList() // test independent of null/not null
        {
            // arrange
            var vocabularyFP = _fixture.Create<string>();

            var response = _classUnderTest.GetTaxonomies(vocabularyFP).ToList();
            response.Count.Should().Be(0);
        }

        [Test]
        public void GivenAFilterParameterWhenGetTaxonomiesGatewayMethodIsCalledThenItReturnsOnlyFilteredTaxonomies() // GatewayReturnsOnlyFilteredTaxonomies
        {
            // arrange
            var vocabularyFP = _fixture.Create<string>();

            var taxonomies = _fixture.CreateMany<Taxonomy>(5).ToList();
            taxonomies[1].Vocabulary = vocabularyFP;
            taxonomies[3].Vocabulary = vocabularyFP;
            DatabaseContext.Taxonomies.AddRange(taxonomies);
            DatabaseContext.SaveChanges();

            // act
            var gatewayResult = _classUnderTest.GetTaxonomies(vocabularyFP).ToList();

            // assert
            gatewayResult.Count.Should().Be(2);
            gatewayResult.Should().BeEquivalentTo(taxonomies.Where(x => x.Vocabulary == vocabularyFP));
        }

        [Test]
        public void GivenAFilterParameterAndNoMatchingResultsWhenGetTaxonomiesGatewayMethodIsCalledThenItReturnsEmptyCollection()
        {
            // arrange
            var vocabularyFP = _fixture.Create<string>();

            var taxonomies = _fixture.CreateMany<Taxonomy>().ToList();
            DatabaseContext.Taxonomies.AddRange(taxonomies);
            DatabaseContext.SaveChanges();

            // act
            var gatewayResult = _classUnderTest.GetTaxonomies(vocabularyFP).ToList();

            // assert
            gatewayResult.Should().NotBeNull();
            gatewayResult.Count.Should().Be(0);
        }
    }
}
