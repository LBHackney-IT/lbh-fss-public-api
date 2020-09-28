using System.Linq;
using Bogus.DataSets;
using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using LBHFSSPublicAPI.V1.Boundary.Request;
using LBHFSSPublicAPI.V1.Boundary.Response;
using LBHFSSPublicAPI.V1.Controllers;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.V1.Factories;
using LBHFSSPublicAPI.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

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

        [TestCase(TestName = "When the services controller GetService action is called with a valid Id the ServicesUseCase ExecuteGet method is called once with the parameter specified")]
        public void ServiceControllerSearchServiceActionCallsTheServicesUseCase()
        {
            var searchParams = Randomm.Create<SearchServicesRequest>();
            _classUnderTest.SearchServices(searchParams);
            _mockUseCase.Verify(uc =>
                uc.ExecuteGet(It.Is<SearchServicesRequest>(p => p == searchParams)), Times.Once);
        }

        [TestCase(TestName = "When the services controller GetService action is called with a taxonomy id the ServicesUseCase ExecuteGet method is called once with the parameter specified")]
        public void ServiceControllerSearchServiceActionWithTaxonomyIdCallsTheServicesUseCaseWithTheCorrectTaxonomyId()
        {
            var searchParams = Randomm.Create<SearchServicesRequest>();
            _classUnderTest.SearchServices(searchParams);
            _mockUseCase.Verify(uc =>
                uc.ExecuteGet(It.Is<SearchServicesRequest>(p => p.TaxonomyIds == searchParams.TaxonomyIds)), Times.Once);
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
    }
}
