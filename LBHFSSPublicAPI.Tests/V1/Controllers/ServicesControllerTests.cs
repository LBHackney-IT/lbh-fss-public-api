using System.Linq;
using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
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

        [Test]
        public void ReturnsResponseWithStatus()
        {
            var expected = Randomm.CreateMany<ServiceEntity>().ToList();
            // var expectedResponse = expected.ToResponse();
            // _mockUseCase.Setup(u => u.ExecuteGet(It.IsAny<int>())).Returns(expectedResponse);
            // var response = _classUnderTest.GetService() as OkObjectResult;
            // response.Should().NotBeNull();
            // response.StatusCode.Should().Be(200);
            // response.Value.Should().BeEquivalentTo(expectedResponse);
        }

        #endregion
    }
}
