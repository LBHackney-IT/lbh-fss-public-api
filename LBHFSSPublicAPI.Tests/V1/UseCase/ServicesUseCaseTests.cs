using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Geolocation;
using LBHFSSPublicAPI.Tests.TestHelpers;
using LBHFSSPublicAPI.V1.Boundary.Request;
using LBHFSSPublicAPI.V1.Boundary.Response;
using Response = LBHFSSPublicAPI.V1.Boundary.Response;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.V1.Factories;
using LBHFSSPublicAPI.V1.Gateways.Interfaces;
using LBHFSSPublicAPI.V1.Infrastructure;
using LBHFSSPublicAPI.V1.UseCase;
using LBHFSSPublicAPI.V1.UseCase.Interfaces;
using Moq;
using NUnit.Framework;
using LBHFSSPublicAPI.V1.Boundary.HelperWrappers;

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

        #region Get Multiple Services

        [TestCase(TestName = "Given a valid request parameter when the use case is called the gateway will be called with the unwrapped id")]
        public void GetServicesUseCaseCallsGatewayGetServices() // ???? Duplicate test. I think the intention was to test the other endpoint. TODO: change upon refactoring.
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
            var gatewayResponse = Randomm.SSGatewayResult();
            var fullMServices = gatewayResponse.FullMatchServices.ToResponseServices();     // because domain doesn't contain distance field
            var splitMServices = gatewayResponse.SplitMatchServices.ToResponseServices();
            _mockServicesGateway.Setup(g => g.SearchServices(It.IsAny<SearchServicesRequest>())).Returns(gatewayResponse);


            var expectedServices = fullMServices.Concat(splitMServices);
            var response = _classUnderTest.ExecuteGet(requestParams);
            response.Should().NotBeNull();
            response.Services.Should().BeEquivalentTo(expectedServices);
        }

        // Gateway coordinates:

        [TestCase(TestName = "Given ExecuteGet GetMultiple Service usecase's method calls Addresses gateway GetPostcodeCoordinates method, When gateway returns related postcode's coordinates, Then usecase response includes non null distances to service locations and non null Metadata coordinate fields.")]
        public void WhenAddressesGatewayReturnsNullThenGetMultipleUsecasePopulatesDistanceAndMetadataFields()
        {
            // arrange
            var request = Randomm.Create<SearchServicesRequest>();
            request.PostCode = Randomm.Postcode();

            var gatewayResponse = Randomm.SSGatewayResult();
            _mockServicesGateway.Setup(g => g.SearchServices(It.IsAny<SearchServicesRequest>())).Returns(gatewayResponse);

            var expectedPostcodeCoords = Randomm.Coordinates();
            _mockAddressesGateway.Setup(g => g.GetPostcodeCoordinates(It.IsAny<string>())).Returns(expectedPostcodeCoords);

            // act
            var usecaseResponse = _classUnderTest.ExecuteGet(request);

            // assert
            usecaseResponse.Metadata.PostCode.Should().Be(request.PostCode);
            usecaseResponse.Metadata.PostCodeLatitude.Should().Be(expectedPostcodeCoords.Latitude);
            usecaseResponse.Metadata.PostCodeLongitude.Should().Be(expectedPostcodeCoords.Longitude);
            usecaseResponse.Metadata.Error.Should().BeNull();
            usecaseResponse.Services.Should().OnlyContain(s => s.Locations.All(l => l.Latitude.HasValue && l.Longitude.HasValue ? l.Distance != null : l.Distance == null));
        }

        [TestCase(TestName = "Given ExecuteGet GetMultiple Service usecase's method calls Addresses gateway GetPostcodeCoordinates method, When gateway returns NULL postcode's coordinates, Then usecase response includes null distances to service locations AND null Metadata coordinate fields.")]
        public void WhenAddressesGatewayReturnsNullThenGetMultipleUsecasePopulatesDistanceAndMetadataFieldsWithNull()
        {
            // arrange
            var request = Randomm.Create<SearchServicesRequest>();
            request.PostCode = Randomm.Postcode();

            var gatewayResponse = Randomm.SSGatewayResult();
            _mockServicesGateway.Setup(g => g.SearchServices(It.IsAny<SearchServicesRequest>())).Returns(gatewayResponse);

            _mockAddressesGateway.Setup(g => g.GetPostcodeCoordinates(It.IsAny<string>())).Returns(null as Coordinate?);

            // act
            var usecaseResponse = _classUnderTest.ExecuteGet(request);

            // assert
            usecaseResponse.Metadata.PostCode.Should().Be(request.PostCode);
            usecaseResponse.Metadata.PostCodeLatitude.Should().Be(null);
            usecaseResponse.Metadata.PostCodeLongitude.Should().Be(null);
            usecaseResponse.Metadata.Error.Should().NotBeNull();
            usecaseResponse.Services.Should().OnlyContain(s => s.Locations.All(l => l.Distance == null));
        }

        [TestCase(TestName = "Given ExecuteGet GetMultiple Service usecase's method calls Addresses gateway GetPostcodeCoordinates method, When gateway throws an exception, Then usecase response includes null distances to service locations AND null Metadata coordinate fields AND non null Error field.")]
        public void WhenAddressesGatewayThrowsAnExceptionThenGetMultipleUsecasePopulatesTheMetadataErrorField()
        {
            // arrange
            var request = Randomm.Create<SearchServicesRequest>();
            request.PostCode = Randomm.Postcode();

            var gatewayResponse = Randomm.SSGatewayResult();
            _mockServicesGateway.Setup(g => g.SearchServices(It.IsAny<SearchServicesRequest>())).Returns(gatewayResponse);

            var expectedException = new Exception(Randomm.Text());
            _mockAddressesGateway.Setup(g => g.GetPostcodeCoordinates(It.IsAny<string>())).Throws(expectedException);

            // act
            var usecaseResponse = _classUnderTest.ExecuteGet(request);

            // assert
            usecaseResponse.Metadata.PostCode.Should().Be(request.PostCode);
            usecaseResponse.Metadata.PostCodeLatitude.Should().Be(null);
            usecaseResponse.Metadata.PostCodeLongitude.Should().Be(null);
            usecaseResponse.Metadata.Error.Should().Be(expectedException.Message);
            usecaseResponse.Services.Should().OnlyContain(s => s.Locations.All(l => l.Distance == null));
        }

        // Sorting requirement has changed.  Would we still need this or will it have to change?
        // [TestCase(TestName = "Given no postcode, When usecase's ExecuteGet method is called, Then it returns a collection of services ordered asc by the service name.")]
        // public void GivenNoPostcodeReturnedServicesAreOrderedAscByName()
        // {
        //     // arrange
        //     var request = Randomm.Create<SearchServicesRequest>();
        //     request.PostCode = null;
        //
        //     var gatewayResponse = Randomm.SSGatewayResult();
        //     var fullMLength = gatewayResponse.FullMatchServices.Count;
        //     var splitMLength = gatewayResponse.SplitMatchServices.Count;
        //
        //     _mockServicesGateway.Setup(g => g.SearchServices(It.IsAny<SearchServicesRequest>())).Returns(gatewayResponse);
        //
        //     // act
        //     var usecaseResponse = _classUnderTest.ExecuteGet(request);
        //
        //     // assert
        //     usecaseResponse.Services.Take(fullMLength).Should().BeInAscendingOrder(s => s.Name);
        //     usecaseResponse.Services.Skip(fullMLength).Should().BeInAscendingOrder(s => s.Name);
        //     usecaseResponse.Services.Should().HaveCount(fullMLength + splitMLength);
        // }

        [TestCase(TestName = "Given a postcode, When usecase's ExecuteGet method is called, Then it returns a collection of services ordered asc by the closest service location distance AND if service has no locations THEN that service will be at the end of the list.")]
        public void GivenAPostcodeReturnedServicesAreOrderedInAWayWhereIfTheyHaveNoChildLocationsTheyAreConsideredTheMostDistant()
        {
            // arrange
            var request = Randomm.Create<SearchServicesRequest>();
            request.PostCode = Randomm.Postcode();

            var addressGatewayResponse = Randomm.Coordinates();
            _mockAddressesGateway.Setup(g => g.GetPostcodeCoordinates(It.IsAny<string>())).Returns(addressGatewayResponse);

            var serviceGatewayResponse = Randomm.SSGatewayResult();
            serviceGatewayResponse.FullMatchServices.FirstOrDefault().ServiceLocations = new List<ServiceLocation>();
            serviceGatewayResponse.SplitMatchServices.FirstOrDefault().ServiceLocations = new List<ServiceLocation>();
            _mockServicesGateway.Setup(g => g.SearchServices(It.IsAny<SearchServicesRequest>())).Returns(serviceGatewayResponse);

            var fullMLength = serviceGatewayResponse.FullMatchServices.Count;
            var splitMLength = serviceGatewayResponse.SplitMatchServices.Count;
            var fullMatchServiceNoLocationsName = serviceGatewayResponse.FullMatchServices.FirstOrDefault().Name; //unique enough due to being generated as hash
            var splitMatchServiceNoLocationsName = serviceGatewayResponse.SplitMatchServices.FirstOrDefault().Name;

            // act
            var usecaseResponse = _classUnderTest.ExecuteGet(request);

            // assert
            usecaseResponse.Services.Take(fullMLength).Last().Name.Should().Be(fullMatchServiceNoLocationsName); // the service with no locations should be last
            usecaseResponse.Services.Last().Name.Should().Be(splitMatchServiceNoLocationsName);

            var fullMServicesWithLocations = usecaseResponse.Services.Take(fullMLength - 1).ToList();
            var splitMServicesWithLocations = usecaseResponse.Services.Skip(fullMLength).Take(splitMLength - 1).ToList();

            AssertThatServiceCollectionIsInAscendingDistancesOrder(fullMServicesWithLocations); // checking whether sorting works in the context of empty service location existing. essentially a check that sorting doesn't stop half way just because it encountered an exceptional service with no locations
            AssertThatServiceCollectionIsInAscendingDistancesOrder(splitMServicesWithLocations);
        }

        private static void AssertThatServiceCollectionIsInAscendingDistancesOrder(List<Response.Service> collection)
        {
            var previousDistance = "";
            var currentDistance = "";

            foreach (var service in collection)
            {
                currentDistance = service.Locations.Min(sl => sl.Distance);
                Assert.LessOrEqual(previousDistance, currentDistance);
                previousDistance = currentDistance;
            }
        }

        // I'm assuming that if the sorting works by taking the shortest sl distance and then sorting by that.
        [TestCase(TestName = "Given a postcode, When usecase's ExecuteGet method is called, Then it returns a collection of services ordered asc by the closest service location distance.")] // just as above, except this confirms that sorting works fine under normal conditions
        public void GivenAPostcodeReturnedServicesAreOrderedAscByDistance()
        {
            // arrange
            var request = Randomm.Create<SearchServicesRequest>();
            request.PostCode = Randomm.Postcode();

            var addressGatewayResponse = Randomm.Coordinates();
            _mockAddressesGateway.Setup(g => g.GetPostcodeCoordinates(It.IsAny<string>())).Returns(addressGatewayResponse);

            var serviceGatewayResponse = Randomm.SSGatewayResult();
            _mockServicesGateway.Setup(g => g.SearchServices(It.IsAny<SearchServicesRequest>())).Returns(serviceGatewayResponse);

            var fullMLength = serviceGatewayResponse.FullMatchServices.Count;

            // act
            var usecaseResponse = _classUnderTest.ExecuteGet(request);
            var fullMServicesWithLocations = usecaseResponse.Services.Take(fullMLength).ToList();
            var splitMServicesWithLocations = usecaseResponse.Services.Skip(fullMLength).ToList();

            // assert
            AssertThatServiceCollectionIsInAscendingDistancesOrder(fullMServicesWithLocations);
            AssertThatServiceCollectionIsInAscendingDistancesOrder(splitMServicesWithLocations);
        }

        [TestCase(TestName = "Given a postcode, When usecase's ExecuteGet method is called AND no services are found, Then it returns an empty collection of services.")]
        public void GivenAPostcodeIfServicesGatewayReturnsAnEmptyCollectionThenUsecaseAlsoReturnsEmptyCollection() // essentially a test to see if the sorting implementation doesn't fall over upon empty collection.
        {
            // arrange
            var request = Randomm.Create<SearchServicesRequest>();
            request.PostCode = Randomm.Postcode();

            var addressGatewayResponse = Randomm.Coordinates();
            _mockAddressesGateway.Setup(g => g.GetPostcodeCoordinates(It.IsAny<string>())).Returns(addressGatewayResponse);

            var serviceGatewayResponse = new SearchServiceGatewayResult(
                fullMatchServices: new List<ServiceEntity>(),
                splitMatchServices: new List<ServiceEntity>()
                );

            _mockServicesGateway.Setup(g => g.SearchServices(It.IsAny<SearchServicesRequest>())).Returns(serviceGatewayResponse);

            // act
            var usecaseResponse = _classUnderTest.ExecuteGet(request);

            // assert
            usecaseResponse.Services.Should().BeEmpty();
        }

        #endregion

        #region Get Single Service by Id

        [TestCase(TestName = "Given a valid request parameter when the use case is called the gateway will be called with the unwrapped id")]
        public void GivenAnIdWhenGetServiceyUseCaseIsCalledThenItCallsGetServiceGatewayMethodAndPassesInThatId()
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
            usecaseResponse.Service.Locations.Should().OnlyContain(l => l.Latitude.HasValue && l.Longitude.HasValue ? l.Distance != null : l.Distance == null);
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
            usecaseResponse.Service.Locations.Should().OnlyContain(l => l.Distance == null);
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
            usecaseResponse.Service.Locations.Should().OnlyContain(l => l.Distance == null);
            usecaseResponse.Metadata.PostCode.Should().Be(request.PostCode);
            usecaseResponse.Metadata.PostCodeLatitude.Should().Be(null);
            usecaseResponse.Metadata.PostCodeLongitude.Should().Be(null);
            usecaseResponse.Metadata.Error.Should().Be(expectedException.Message);
        }

        #endregion

        #region Search Services
        [TestCase(TestName = "Given a url encoded search parameter when the use case is called the gateway will be called with the unencoded parameter")]
        public void GivenAUrlEncodedSearchTermGatewayIsCalledWithDecodedTerm() // implementation of this test fixes a front-end bug, where front-end app accidentally encodes the url twice before calling an API - I don't we should be 'fixing' this on back-end API.
        {
            var expectedServices = Randomm.SSGatewayResult();
            _mockServicesGateway.Setup(g => g.SearchServices(It.IsAny<SearchServicesRequest>())).Returns(expectedServices); // dummy setup - irrelevant for the test
            var searchTerm = Randomm.Text();
            var urlencodedSearch = searchTerm.Replace(" ", "%2520");
            var reqParams = new SearchServicesRequest();
            reqParams.Search = urlencodedSearch;
            _classUnderTest.ExecuteGet(reqParams);
            _mockServicesGateway.Verify(g => g.SearchServices(It.Is<SearchServicesRequest>(p => p.Search == searchTerm)), Times.Once);
        }
        #endregion
    }
}
