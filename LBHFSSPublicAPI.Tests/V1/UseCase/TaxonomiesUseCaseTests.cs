using System.Linq;
using AutoFixture;
using FluentAssertions;
using LBHFSSPublicAPI.V1.Boundary.Response;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.V1.Gateways.Interfaces;
using LBHFSSPublicAPI.V1.Infrastructure;
using LBHFSSPublicAPI.V1.UseCase;
using LBHFSSPublicAPI.V1.UseCase.Interfaces;
using Moq;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.UseCase
{
    [TestFixture]
    public class TaxonomiesUseCaseTests
    {
        private TaxonomiesUseCase _classUnderTest;
        private Mock<ITaxonomiesGateway> _mockTaxonomiesGateway;
        private Fixture _fixture = new Fixture();

        [SetUp]
        public void Setup()
        {
            _mockTaxonomiesGateway = new Mock<ITaxonomiesGateway>();
            _classUnderTest = new TaxonomiesUseCase(_mockTaxonomiesGateway.Object);
        }

        [Test]
        public void GetTaxonomiesUseCaseCallsGatewayGetTaxonomies()
        {
            _classUnderTest.ExecuteGet();
            _mockTaxonomiesGateway.Verify(u => u.GetTaxonomies(), Times.Once);
        }

        [Test]
        public void ReturnsHelpRequests()
        {
            var responseData = _fixture.CreateMany<TaxonomyEntity>().ToList();
            _mockTaxonomiesGateway.Setup(g => g.GetTaxonomies()).Returns(responseData);
            var expectedResponse = new TaxonomyResponse {Taxonomies = responseData};
            var response = _classUnderTest.ExecuteGet();
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(expectedResponse);
        }

    }
}
