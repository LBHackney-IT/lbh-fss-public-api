using System;
using System.Collections.Generic;
using System.Linq;
using Bogus.DataSets;
using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using LBHFSSPublicAPI.V1.Boundary;
using LBHFSSPublicAPI.V1.Boundary.Request;
using LBHFSSPublicAPI.V1.Boundary.Response;
using LBHFSSPublicAPI.V1.Controllers;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.V1.Factories;
using LBHFSSPublicAPI.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using static LBHFSSPublicAPI.Tests.TestHelpers.ExceptionThrower;

namespace LBHFSSPublicAPI.Tests.V1.Controllers
{
    [TestFixture]
    public class ServicesControllerTests : DatabaseTests
    {
        private ServicesController _classUnderTest;
        private Mock<IServicesUseCase> _mockUseCase;

        [SetUp]
        public void SetUp()
        {
            _mockUseCase = new Mock<IServicesUseCase>();
            _classUnderTest = new ServicesController(_mockUseCase.Object);
        }

        #region Get Services by Id

        [TestCase(TestName = "When the services controller GetService action is called with a valid Id the ServicesUseCase ExecuteGet method is called once with the parameter specified")]
        public void GetServiceControllerActionCallsTheServicesUseCase()
        {
            var requestParams = Randomm.Create<GetServiceByIdRequest>();
            _classUnderTest.GetService(requestParams);
            _mockUseCase.Verify(uc => uc.ExecuteGet(It.Is<GetServiceByIdRequest>(p => p == requestParams)), Times.Once);
        }

        [Test]
        public void ReturnsResponseWithStatus()
        {
            var expected = Randomm.Create<GetServiceResponse>();
            var reqParams = Randomm.Create<GetServiceByIdRequest>();
            _mockUseCase.Setup(u => u.ExecuteGet(It.IsAny<GetServiceByIdRequest>())).Returns(expected);
            var response = _classUnderTest.GetService(reqParams) as OkObjectResult;
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(200);
            response.Value.Should().BeEquivalentTo(expected);
        }

        #endregion

        #region Search Services
        [TestCase(TestName = "When the services controller SearchServices action is called with a valid Id the ServicesUseCase ExecuteGet method is called once with the parameter specified")]
        public void ServiceControllerSearchServiceActionCallsTheServicesUseCase()
        {
            var searchParams = Randomm.Create<SearchServicesRequest>();
            _classUnderTest.SearchServices(searchParams);
            _mockUseCase.Verify(uc =>
                uc.ExecuteGet(It.Is<SearchServicesRequest>(p => p == searchParams)), Times.Once);
        }

        [TestCase(TestName = "When the services controller SearchServices action is called with a taxonomy id the ServicesUseCase ExecuteGet method is called once with the parameter specified")]
        public void ServiceControllerSearchServiceActionWithTaxonomyIdCallsTheServicesUseCaseWithTheCorrectTaxonomyId()
        {
            var searchParams = Randomm.Create<SearchServicesRequest>();
            _classUnderTest.SearchServices(searchParams);
            _mockUseCase.Verify(uc =>
                uc.ExecuteGet(It.Is<SearchServicesRequest>(p => p.TaxonomyIds == searchParams.TaxonomyIds)), Times.Once);
        }

        [TestCase(TestName = "Given an unexpected exception is thrown, When SearchServices Service controller method is called, Then controller returns an Error response")]
        public void UponUnexpectedExceptionTheServiceControllerSearchServicesMethodReturnsErrorResponseObject()
        {
            //arrange
            var randomExpectedException = GenerateException();

            _mockUseCase.Setup(u => u.ExecuteGet(It.IsAny<SearchServicesRequest>())).Throws(randomExpectedException);

            //act
            var controllerResponse = _classUnderTest.SearchServices(null);
            var controllerObjectResult = controllerResponse as ObjectResult;
            var returnedContent = controllerObjectResult.Value;

            //assert
            controllerResponse.Should().NotBeNull();
            controllerObjectResult.Should().NotBeNull();
            returnedContent.Should().BeOfType<ErrorResponse>();
            returnedContent.Should().NotBeNull();
        }

        [TestCase(TestName = "Given the services controller SearchServices method is called, When a simple exception during code execution is raised, Then the controller method returns custom error response with a message")]
        public void ServiceControllerSearchServiceMethodHandlesSimpleExceptionsByReturningCustomErrorObject()
        {
            // arrange
            (var randomExpectedException, var expectedExceptionMessages) =
                GenerateExceptionAndCorrespondinExceptionMessages(ExceptionType.SimpleException);

            var expectedExceptionMessage = expectedExceptionMessages[0];

            _mockUseCase.Setup(u => u.ExecuteGet(It.IsAny<SearchServicesRequest>())).Throws(randomExpectedException);

            //act
            var controllerResponse = _classUnderTest.SearchServices(null);

            //assert
            var controllerObjectResult = controllerResponse as ObjectResult;
            var returnedContent = controllerObjectResult.Value as ErrorResponse;
            var actualExceptionMessage = returnedContent.Errors[0];

            returnedContent.Should().NotBeNull();
            returnedContent.Errors.Count.Should().Be(expectedExceptionMessages.Count);
            returnedContent.Errors.Count.Should().Be(1);

            actualExceptionMessage.Should().Be(expectedExceptionMessage);
        }


        [TestCase(TestName = "Given the services controller SearchServices method is called, When a nested exception during code execution is raised, Then the controller method returns custom error response with exception messages")]
        public void ServiceControllerSearchServiceMethodHandlesNestedExceptionsByReturningCustomErrorObject()
        {
            // arrange
            (var randomExpectedException, var expectedExceptionMessages) =
                GenerateExceptionAndCorrespondinExceptionMessages(ExceptionType.InnerException);

            var expectedExceptionMessage = expectedExceptionMessages[0];
            var expectedInnerExceptionMessage = expectedExceptionMessages[1];

            _mockUseCase.Setup(u => u.ExecuteGet(It.IsAny<SearchServicesRequest>())).Throws(randomExpectedException);

            //act
            var controllerResponse = _classUnderTest.SearchServices(null);

            //assert
            var controllerObjectResult = controllerResponse as ObjectResult;
            var returnedContent = controllerObjectResult.Value as ErrorResponse;
            var actualExceptionMessage = returnedContent.Errors[0];
            var actualInnerExceptionMessage = returnedContent.Errors[1];

            returnedContent.Should().NotBeNull();
            returnedContent.Errors.Count.Should().Be(expectedExceptionMessages.Count);
            returnedContent.Errors.Count.Should().Be(2);

            actualExceptionMessage.Should().Be(expectedExceptionMessage);
            actualInnerExceptionMessage.Should().Be(expectedInnerExceptionMessage);
        }

        #endregion
    }
}
