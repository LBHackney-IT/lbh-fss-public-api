using System.Linq;
using AutoFixture;
using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
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

        #region Get Taxonomies with/without filter

        [Test]
        public void GetTaxonomiesUseCaseCallsGatewayGetTaxonomies()
        {
            // arrange
            var vocabularyFP = _fixture.Create<string>();

            // act
            _classUnderTest.ExecuteGet(vocabularyFP);

            // assert
            _mockTaxonomiesGateway.Verify(u => u.GetTaxonomies(It.Is<string>(p => p == vocabularyFP)), Times.Once);
        }

        [Test]
        public void ReturnsHelpRequests() //Wrap up
        {
            var responseData = _fixture.CreateMany<TaxonomyEntity>().ToList();
            _mockTaxonomiesGateway.Setup(g => g.GetTaxonomies(It.IsAny<string>())).Returns(responseData);
            var expectedResponse = new TaxonomyResponse { Taxonomies = responseData };
            var response = _classUnderTest.ExecuteGet(null);
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(expectedResponse);
        }
        #endregion

        #region Get Single Taxonomy by Id

        [Test]
        public void GivenAnIdWhenGetTaxonomyUseCaseIsCalledThenItCallsGetTaxonomyGatewayMethodAndPassesInThatId()
        {
            // arrange
            var id = Randomm.Id();

            // act
            _classUnderTest.ExecuteGet(id);

            // assert
            _mockTaxonomiesGateway.Verify(g => g.GetTaxonomy(It.Is<int>(p => p == id)), Times.Once);
        }

        [Test]
        public void GivenSuccessfulGetTaxonomyGatewayCallWhenGatewayReturnsAValueThenTheUseCaseReturnsThatSameValue()
        {
            // arrange
            var id = Randomm.Id(); //irrelevant
            var gatewayResult = Randomm.Create<TaxonomyEntity>();

            _mockTaxonomiesGateway.Setup(g => g.GetTaxonomy(It.IsAny<int>())).Returns(gatewayResult);

            // act
            var usecaseResult = _classUnderTest.ExecuteGet(id);

            // assert
            usecaseResult.Should().Be(gatewayResult);
        }

        #endregion
    }
}
