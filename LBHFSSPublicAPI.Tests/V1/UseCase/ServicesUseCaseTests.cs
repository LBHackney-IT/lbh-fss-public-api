using System.Linq;
using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using LBHFSSPublicAPI.V1.Boundary.Request;
using LBHFSSPublicAPI.V1.Boundary.Response;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.V1.Factories;
using LBHFSSPublicAPI.V1.Gateways.Interfaces;
using LBHFSSPublicAPI.V1.Infrastructure;
using LBHFSSPublicAPI.V1.UseCase;
using LBHFSSPublicAPI.V1.UseCase.Interfaces;
using Moq;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.UseCase
{
    [TestFixture]
    public class ServicesUseCaseTests
    {
        private ServicesUseCase _classUnderTest;
        private Mock<IServicesGateway> _mockServicesGateway;

        [SetUp]
        public void Setup()
        {
            _mockServicesGateway = new Mock<IServicesGateway>();
            _classUnderTest = new ServicesUseCase(_mockServicesGateway.Object);
        }

        #region Get Services with/without filter

        [TestCase(TestName = "Given a valid request parameter when the use case is called the gateway will be called with the unwrapped id")]
        public void GetServicesUseCaseCallsGatewayGetServices()
        {
            // arrange
            var requestParams = Randomm.Create<GetServiceByIdRequest>();

            // act
            _classUnderTest.ExecuteGet(requestParams);

            // assert
            _mockServicesGateway.Verify(u => u.GetService(It.Is<int>(p => p == requestParams.Id)), Times.Once);
        }

        [TestCase(TestName = "Given a valid set of search parameters a ServicesResponse collection is returned")]
        public void ReturnsServicesIfSeachParamIsProvided() //Wrap up
        {
            var requestParams = Randomm.Create<SearchServicesRequest>();
            var responseData = EntityHelpers.CreateServices().ToDomain();
            _mockServicesGateway.Setup(g => g.SearchServices(It.IsAny<SearchServicesRequest>())).Returns(responseData);
            var expectedResponse = responseData.ToResponse();
            var response = _classUnderTest.ExecuteGet(requestParams);
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(expectedResponse);
        }
        #endregion

        #region Get Single Service by Id

        [TestCase(TestName = "Given a valid request parameter when the use case is called the gateway will be called with the unwrapped id")]
        public void GivenAnIdWhenGetServiceyUseCaseIsCalledThenItCallsGetServiceyGatewayMethodAndPassesInThatId()
        {
            // arrange
            var reqParams = Randomm.Create<GetServiceByIdRequest>();

            // act
            _classUnderTest.ExecuteGet(reqParams);

            // assert
            _mockServicesGateway.Verify(g => g.GetService(It.Is<int>(p => p == reqParams.Id)), Times.Once);
        }

        [TestCase(TestName = "Given a valid request parameter when the use case is called the use case should respond with a valid response object")]
        public void GivenSuccessfulGetServiceyGatewayCallWhenGatewayReturnsAValueThenTheUseCaseReturnsThatSameValue()
        {
            // arrange
            var reqParams = Randomm.Create<GetServiceByIdRequest>();
            var gatewayResult = EntityHelpers.CreateService().ToDomain();

            _mockServicesGateway.Setup(g => g.GetService(It.IsAny<int>())).Returns(gatewayResult);

            // act
            var usecaseResult = _classUnderTest.ExecuteGet(reqParams);

            // assert
            usecaseResult.Should().BeEquivalentTo(gatewayResult.ToResponse());
        }

        #endregion
    }
}
