using System;
using System.Linq;
using FluentAssertions;
using Geolocation;
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
        private Mock<IAddressesGateway> _mockAddressesGateway;

        [SetUp]
        public void Setup()
        {
            _mockServicesGateway = new Mock<IServicesGateway>();
            _mockAddressesGateway = new Mock<IAddressesGateway>();
            _classUnderTest = new ServicesUseCase(_mockServicesGateway.Object, _mockAddressesGateway.Object);
        }

        #region Get Services with/without filter

        [TestCase(TestName = "Given a valid request parameter when the use case is called the gateway will be called with the unwrapped id")]
        public void GetServicesUseCaseCallsGatewayGetServices()
        {
            // dummy setup
            var expectedService = EntityHelpers.CreateService().ToDomain();
            _mockServicesGateway.Setup(g => g.GetService(It.IsAny<int>())).Returns(expectedService);

            // arrange
            var requestParams = Randomm.Create<GetServiceByIdRequest>();
            requestParams.PostCode = null;

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
            expectedResponse.Metadata.PostCode = requestParams.PostCode;
            var response = _classUnderTest.ExecuteGet(requestParams);
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(expectedResponse);
        }
        #endregion

        #region Get Single Service by Id

        [TestCase(TestName = "Given a valid request parameter when the use case is called the gateway will be called with the unwrapped id")]
        public void GivenAnIdWhenGetServiceyUseCaseIsCalledThenItCallsGetServiceyGatewayMethodAndPassesInThatId()
        {
            // dummy setup
            var expectedService = EntityHelpers.CreateService().ToDomain();
            _mockServicesGateway.Setup(g => g.GetService(It.IsAny<int>())).Returns(expectedService);

            // arrange
            var reqParams = Randomm.Create<GetServiceByIdRequest>();
            reqParams.PostCode = null;

            // act
            _classUnderTest.ExecuteGet(reqParams);

            // assert
            _mockServicesGateway.Verify(g => g.GetService(It.Is<int>(p => p == reqParams.Id)), Times.Once);
        }

        [TestCase(TestName = "Given a valid request parameter when the use case is called the use case should respond with a valid response object")]
        public void GivenSuccessfulGetServiceyGatewayCallWhenGatewayReturnsAValueThenTheUseCaseReturnsThatSameValue()
        {
            // dummy setup
            var expectedService = EntityHelpers.CreateService().ToDomain();
            _mockServicesGateway.Setup(g => g.GetService(It.IsAny<int>())).Returns(expectedService);

            // arrange
            var reqParams = Randomm.Create<GetServiceByIdRequest>();
            reqParams.PostCode = null;

            var gatewayResult = EntityHelpers.CreateService().ToDomain();
            _mockServicesGateway.Setup(g => g.GetService(It.IsAny<int>())).Returns(gatewayResult);

            // act
            var usecaseResult = _classUnderTest.ExecuteGet(reqParams);

            // assert
            usecaseResult.Should().BeEquivalentTo(gatewayResult.ToResponse());
        }

        [TestCase(TestName = "Given a nonempty postcode, When ExecuteGet Service usecase's method is called, Then it calls the Addresses gateway GetPostcodeCoordinates method.")]
        public void GivenValidPostcodeUsecaseShouldCallAddressesGateway()
        {
            // dummy setup
            var expectedService = EntityHelpers.CreateService().ToDomain();
            _mockServicesGateway.Setup(g => g.GetService(It.IsAny<int>())).Returns(expectedService);

            // arrange
            var request = Randomm.Create<GetServiceByIdRequest>();

            // act
            _classUnderTest.ExecuteGet(request);

            // assert
            _mockAddressesGateway.Verify(g => g.GetPostcodeCoordinates(It.IsAny<string>()), Times.Once);
        }

        [TestCase(TestName = "Given a nonempty postcode, When ExecuteGet Service usecase's method is called, Then it calls the Addresses gateway GetPostcodeCoordinates method, And pass in that Postcode.")]
        public void GivenValidPostcodeUsecaseShouldCallAddressesGatewayWithThatPostcode()
        {
            // dummy setup
            var expectedService = EntityHelpers.CreateService().ToDomain();
            _mockServicesGateway.Setup(g => g.GetService(It.IsAny<int>())).Returns(expectedService);

            // arrange
            var request = Randomm.Create<GetServiceByIdRequest>();

            // act
            _classUnderTest.ExecuteGet(request);

            // assert
            _mockAddressesGateway.Verify(g => g.GetPostcodeCoordinates(It.Is<string>(p => p == request.PostCode)), Times.Once);
        }

        [TestCase("", TestName = "Given an empty postcode, When ExecuteGet Service usecase's method is called, Then it does not call the Addresses gateway GetPostcodeCoordinates method")]
        [TestCase(null, TestName = "Given a null postcode, When ExecuteGet Service usecase's method is called, Then it does not call the Addresses gateway GetPostcodeCoordinates method")]
        public void GivenNoPostcodeUsecaseShouldNotCallAddressesGateway(string postcode)
        {
            // dummy setup
            var expectedService = EntityHelpers.CreateService().ToDomain();
            _mockServicesGateway.Setup(g => g.GetService(It.IsAny<int>())).Returns(expectedService);

            // arrange
            var request = Randomm.Create<GetServiceByIdRequest>();
            request.PostCode = postcode;

            // act
            _classUnderTest.ExecuteGet(request);

            // assert
            _mockAddressesGateway.Verify(g => g.GetPostcodeCoordinates(It.IsAny<string>()), Times.Never);
        }

        // Gateway coordinates:

        [TestCase(TestName = "Given ExecuteGet Service usecase's method calls Addresses gateway GetPostcodeCoordinates method, When gateway returns related postcode's coordinates, Then usecase response includes non null distances to service locations and non null Metadata coordinate fields.")]
        public void WhenAddressesGatewayReturnsNullThenUsecasePopulatesDistanceAndMetadataFields()
        {
            // arrange
            var request = Randomm.Create<GetServiceByIdRequest>();
            request.PostCode = Randomm.Postcode();

            var expectedService = EntityHelpers.CreateService().ToDomain();
            _mockServicesGateway.Setup(g => g.GetService(It.IsAny<int>())).Returns(expectedService);

            var expectedPostcodeCoords = Randomm.Coordinates();
            _mockAddressesGateway.Setup(g => g.GetPostcodeCoordinates(It.IsAny<string>())).Returns(expectedPostcodeCoords);

            // act
            var usecaseResponse = _classUnderTest.ExecuteGet(request);

            // assert
            usecaseResponse.Metadata.PostCode.Should().Be(request.PostCode);
            usecaseResponse.Metadata.PostCodeLatitude.Should().Be(expectedPostcodeCoords.Latitude);
            usecaseResponse.Metadata.PostCodeLongitude.Should().Be(expectedPostcodeCoords.Longitude);
            usecaseResponse.Metadata.Error.Should().BeNull();
            usecaseResponse.Locations.Should().OnlyContain(l => l.Latitude.HasValue && l.Longitude.HasValue ? l.Distance != null : l.Distance == null);
        }

        [TestCase(TestName = "Given ExecuteGet Service usecase's method calls Addresses gateway GetPostcodeCoordinates method, When gateway returns NULL postcode's coordinates, Then usecase response includes null distances to service locations AND null Metadata coordinate fields.")]
        public void WhenAddressesGatewayReturnsNullThenUsecasePopulatesDistanceAndMetadataFieldsWithNull()
        {
            // arrange
            var request = Randomm.Create<GetServiceByIdRequest>();
            request.PostCode = Randomm.Postcode();

            var expectedService = EntityHelpers.CreateService().ToDomain();
            _mockServicesGateway.Setup(g => g.GetService(It.IsAny<int>())).Returns(expectedService);

            _mockAddressesGateway.Setup(g => g.GetPostcodeCoordinates(It.IsAny<string>())).Returns(null as Coordinate?);

            // act
            var usecaseResponse = _classUnderTest.ExecuteGet(request);

            // assert
            usecaseResponse.Locations.Should().OnlyContain(l => l.Distance == null);
            usecaseResponse.Metadata.PostCode.Should().Be(request.PostCode);
            usecaseResponse.Metadata.PostCodeLatitude.Should().Be(null);
            usecaseResponse.Metadata.PostCodeLongitude.Should().Be(null);
            usecaseResponse.Metadata.Error.Should().NotBeNull();
        }

        [TestCase(TestName = "Given ExecuteGet Service usecase's method calls Addresses gateway GetPostcodeCoordinates method, When gateway throws an exception, Then usecase response includes null distances to service locations AND null Metadata coordinate fields AND non null Error field.")]
        public void WhenAddressesGatewayThrowsAnExceptionThenUsecasePopulatesTheMetadataErrorField()
        {
            // arrange
            var request = Randomm.Create<GetServiceByIdRequest>();
            request.PostCode = Randomm.Postcode();

            var expectedService = EntityHelpers.CreateService().ToDomain();
            _mockServicesGateway.Setup(g => g.GetService(It.IsAny<int>())).Returns(expectedService);

            var expectedException = new Exception(Randomm.Text());
            _mockAddressesGateway.Setup(g => g.GetPostcodeCoordinates(It.IsAny<string>())).Throws(expectedException);

            // act
            var usecaseResponse = _classUnderTest.ExecuteGet(request);

            // assert
            usecaseResponse.Locations.Should().OnlyContain(l => l.Distance == null);
            usecaseResponse.Metadata.PostCode.Should().Be(request.PostCode);
            usecaseResponse.Metadata.PostCodeLatitude.Should().Be(null);
            usecaseResponse.Metadata.PostCodeLongitude.Should().Be(null);
            usecaseResponse.Metadata.Error.Should().Be(expectedException.Message);
        }

        #endregion

        #region Search Services
        [TestCase(TestName = "Given a url encoded search parameter when the use case is called the gateway will be called with the unencoded parameter")]
        public void GivenAUrlEncodedSearchTermGatewayIsCalledWithDecodedTerm()
        {
            var expectedService = EntityHelpers.CreateServices().ToDomain();
            _mockServicesGateway.Setup(g => g.SearchServices(It.IsAny<SearchServicesRequest>())).Returns(expectedService);
            var searchTerm = Randomm.Text();
            var urlencodedSearch = searchTerm.Replace(" ","%2520");
            var reqParams = new SearchServicesRequest();
            reqParams.Search = urlencodedSearch;
            _classUnderTest.ExecuteGet(reqParams);
            _mockServicesGateway.Verify(g => g.SearchServices(It.Is<SearchServicesRequest>(p => p.Search == searchTerm)), Times.Once);
        }
        #endregion
    }
}
