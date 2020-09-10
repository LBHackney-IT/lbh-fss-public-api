using System.Linq;
using AutoFixture;
using LBHFSSPublicAPI.Tests.V1.Helper;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.V1.Gateways;
using FluentAssertions;
using LBHFSSPublicAPI.V1.Infrastructure;
using NUnit.Framework;
using LBHFSSPublicAPI.Tests.TestHelpers;

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

        #region Get Taxonomies with/without filter

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

        [Test]
        public void GivenAFilterParameterAndAMatchingTaxonomyWithMultipleConcatinatedFilterValuesWhenGetTaxonomiesGatewayMethodIsCalledThenItReturnsOnlyFilteredTaxonomies() // GatewayReturnsOnlyFilteredTaxonomies
        {
            // arrange
            var vocabularyFP = _fixture.Create<string>();

            var taxonomies = _fixture.CreateMany<Taxonomy>(7).ToList();
            taxonomies[1].Vocabulary = $"{vocabularyFP} {_fixture.Create<string>()}";                               // matching value in front
            taxonomies[3].Vocabulary = $"{_fixture.Create<string>()} {vocabularyFP}";                               // matching value at the back
            taxonomies[4].Vocabulary = $"{_fixture.Create<string>()} {vocabularyFP} {_fixture.Create<string>()}";   // matching value in the middle
            DatabaseContext.Taxonomies.AddRange(taxonomies);
            DatabaseContext.SaveChanges();

            // act
            var gatewayResult = _classUnderTest.GetTaxonomies(vocabularyFP).ToList();

            // assert
            gatewayResult.Count.Should().Be(3);
            gatewayResult.Should().BeEquivalentTo(taxonomies.Where(x => x.Vocabulary.Contains(vocabularyFP, System.StringComparison.OrdinalIgnoreCase)));
        }

        [Test]
        public void GivenAFilterParameterWhenGetTaxonomiesGatewayMethodIsCalledThenItReturnsAllCorrectlyFilteredTaxonomiesRegardlesOfStringCase() // GatewayReturnsOnlyFilteredTaxonomies
        {
            // arrange
            var vocabularyFP = _fixture.Create<string>().ToLower();

            var taxonomies = _fixture.CreateMany<Taxonomy>(7).ToList();
            //DB will contain upper case, while filter param will be lower case
            taxonomies[1].Vocabulary = vocabularyFP.ToUpper();
            taxonomies[5].Vocabulary = $"{_fixture.Create<string>()} {vocabularyFP.ToUpper()} {_fixture.Create<string>()}";
            DatabaseContext.Taxonomies.AddRange(taxonomies);
            DatabaseContext.SaveChanges();

            // act
            var gatewayResult = _classUnderTest.GetTaxonomies(vocabularyFP).ToList();

            // assert
            gatewayResult.Count.Should().Be(2);
            gatewayResult.Should().BeEquivalentTo(taxonomies.Where(x => x.Vocabulary.Contains(vocabularyFP, System.StringComparison.OrdinalIgnoreCase)));
        }
        #endregion

        #region Get Single Taxonomy by Id

        [Test]
        public void GivenIdThatHasAMatchWhenGetTaxonomyGatewayMethodIsCalledThenItReturnsMatchingTaxonomyDomainObject()
        {
            // arrange
            var taxonomies = Random.CreateMany<Taxonomy>();
            DatabaseContext.Taxonomies.AddRange(taxonomies);
            DatabaseContext.SaveChanges();

            var expectedResult = DatabaseContext.Taxonomies.First();
            var expectedId = expectedResult.Id;

            // act
            var gatewayResult = _classUnderTest.GetTaxonomy(expectedId);

            // assert
            gatewayResult.Should().NotBeNull();
            gatewayResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void GivenIdThatDoesNotHaveAMatchWhenGetTaxonomyGatewayMethodIsCalledThenItReturnsNull()
        {
            // arrange
            var taxonomies = Random.CreateMany<Taxonomy>();
            DatabaseContext.Taxonomies.AddRange(taxonomies);
            DatabaseContext.SaveChanges();

            var id = Random.Id();

            // act
            var gatewayResult = _classUnderTest.GetTaxonomy(id);

            // assert
            gatewayResult.Should().BeNull();
        }
        #endregion
    }
}
