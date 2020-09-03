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
        public void GetTaxonomiesReturnsTaxonomies()
        {
            var entity = _fixture.Create<Taxonomy>();
            DatabaseContext.Taxonomies.Add(entity);
            DatabaseContext.SaveChanges();
            var response = _classUnderTest.GetTaxonomies().ToList();
            response.First().Name.Should().Be(entity.Name);
        }

        [Test]
        public void GetTaxonomiesWhenDbIsEmptyReturnsAnEmptyList()
        {
            var response = _classUnderTest.GetTaxonomies().ToList();
            response.Count.Should().Be(0);
        }
    }
}
