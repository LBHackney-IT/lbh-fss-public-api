using System.Collections.Generic;
using System.Linq;
using LBHFSSPublicAPI.V1.Controllers;
using LBHFSSPublicAPI.V1.UseCase;
using FluentAssertions;
using LBHFSSPublicAPI.V1.Boundary.Response;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using LBHFSSPublicAPI.Tests.TestHelpers;
using LBHFSSPublicAPI.V1.Boundary;
using LBHFSSPublicAPI.V1.Factories;

namespace LBHFSSPublicAPI.Tests.V1.Controllers
{

    [TestFixture]
    public class TaxonomiesControllerTests
    {
        private TaxonomiesController _classUnderTest;
        private Mock<ITaxonomiesUseCase> _mockUseCase;

        [SetUp]
        public void SetUp()
        {
            _mockUseCase = new Mock<ITaxonomiesUseCase>();
            _classUnderTest = new TaxonomiesController(_mockUseCase.Object);
        }

        #region Get Taxonomies with/without filter

        [Test]
        public void ReturnsResponseWithStatus()
        {
            var expected = Randomm.CreateMany<TaxonomyEntity>().ToList();
            var expectedResponse = expected.ToResponse();
            _mockUseCase.Setup(u => u.ExecuteGet(It.IsAny<string>())).Returns(expectedResponse);
            var response = _classUnderTest.GetTaxonomies() as OkObjectResult;
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(200);
            response.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public void ControllerGetRequestCallsUseCaseGetMethod()
        {
            _classUnderTest.GetTaxonomies();
            _mockUseCase.Verify(u => u.ExecuteGet(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GiveRequestWithFilterParameterWhenGetTaxonomiesControllerMethodIsCalledThenItCallsExecuteGetUsecaseMethodWithThatParameter()
        {
            // arrange
            var vocabularyFP = Randomm.Create<string>();

            // act
            _classUnderTest.GetTaxonomies(vocabularyFP);

            // assert
            _mockUseCase.Verify(u => u.ExecuteGet(It.Is<string>(p => p == vocabularyFP)), Times.Once);
        }
        #endregion

        #region Get Single Taxonomy by Id

        [Test]
        public void WhenSearchedTaxonomyIsFoundSuccessfullyThenGetTaxonomyControllerReturns200OkResponse()
        {
            // arrange
            var expectedStatusCode = 200;
            var expectedRespType = typeof(OkObjectResult);

            var id = Randomm.Id();
            _mockUseCase.Setup(u => u.ExecuteGet(It.Is<int>(p => p == id))).Returns(new TaxonomyResponse());

            // act
            var controllerResponse = _classUnderTest.GetTaxonomy(id);

            // assert
            var responseObjectResult = controllerResponse as ObjectResult;
            responseObjectResult.Should().NotBeNull();
            responseObjectResult.Should().BeOfType(expectedRespType);

            var responseStatusCode = responseObjectResult.StatusCode;
            responseStatusCode.Should().Be(expectedStatusCode);
        }

        [Test]
        public void GivenASuccessfulGetTaxonomyCallWhenUseCaseReturnsAValueThenControllerResponseWrapsUpThatValue()
        {
            // arrange
            var expectedValue = Randomm.Create<TaxonomyEntity>().ToResponse();

            _mockUseCase.Setup(u => u.ExecuteGet(It.IsAny<int>())).Returns(expectedValue);

            var id = Randomm.Id(); //irrelevant

            // act
            var controllerResponse = _classUnderTest.GetTaxonomy(id);

            // assert
            var responseValue = (controllerResponse as ObjectResult).Value;
            responseValue.Should().Be(expectedValue);
        }

        [Test]
        public void GivenAValidIdWhenGetTaxonomyControllerMethodIsCalledThenItCallsTheUseCaseGetMethod()
        {
            // arrange
            var id = Randomm.Id(); //irrelevant

            // act
            _classUnderTest.GetTaxonomy(id);

            // assert
            _mockUseCase.Verify(u => u.ExecuteGet(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void GivenAValidIdWhenGetTaxonomyControllerMethodIsCalledThenItCallsTheUseCaseGetMethodWithThatId()
        {
            // arrange
            var id = Randomm.Id(); //irrelevant

            // act
            _classUnderTest.GetTaxonomy(id);

            // assert
            _mockUseCase.Verify(u => u.ExecuteGet(It.Is<int>(p => p == id)), Times.Once);
        }

        [Test]
        public void WhenSearchedTaxonomyIsNotFoundThenGetTaxonomyControllerReturnsOk404NotFoundResponse()
        {
            // arrange
            var expectedStatusCode = 404;
            var expectedRespType = typeof(NotFoundObjectResult);

            var id = Randomm.Id(); //irrelevant
            _mockUseCase.Setup(u => u.ExecuteGet(It.Is<int>(p => p == id))).Returns(null as TaxonomyResponse);

            // act
            var controllerResponse = _classUnderTest.GetTaxonomy(id);

            // assert
            var responseObjectResult = controllerResponse as ObjectResult;
            responseObjectResult.Should().NotBeNull();
            responseObjectResult.Should().BeOfType(expectedRespType);

            var responseStatusCode = responseObjectResult.StatusCode;
            responseStatusCode.Should().Be(expectedStatusCode);
        }

        [Test]
        public void WhenSearchedTaxonomyIsNotFoundThenThenGetTaxonomyControllerReturnsWrapedUpErrorResponse()
        {
            // arrange
            var id = Randomm.Id();
            _mockUseCase.Setup(u => u.ExecuteGet(It.Is<int>(p => p == id))).Returns(null as TaxonomyResponse);

            var expectedValue = new ErrorResponse($"Taxonomy with an Id: {id} was not found.");

            // act
            var controllerResponse = _classUnderTest.GetTaxonomy(id);

            // assert
            var responseValue = (controllerResponse as ObjectResult).Value;
            responseValue.Should().BeEquivalentTo(expectedValue);
        }

        #endregion
    }
}
