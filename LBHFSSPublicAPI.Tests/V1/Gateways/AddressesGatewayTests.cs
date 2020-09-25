using System;
using System.Net;
using FluentAssertions;
using Geolocation;
using LBHFSSPublicAPI.Tests.TestHelpers;
using LBHFSSPublicAPI.V1.Exceptions;
using LBHFSSPublicAPI.V1.Gateways;
using LBHFSSPublicAPI.V1.Gateways.Interfaces;
using LBHFSSPublicAPI.V1.Infrastructure;
using Moq;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Gateways
{
    [TestFixture]
    public class AddressesGatewayTests
    {
        private IAddressesGateway _classUnderTest;
        private Mock<IAddressesAPIContext> _mockAddressesAPIContext;

        [SetUp]
        public void Setup()
        {
            _mockAddressesAPIContext = new Mock<IAddressesAPIContext>();
            _classUnderTest = new AddressesGateway(_mockAddressesAPIContext.Object);
        }

        [TestCase(TestName = "Given a postcode, When Addresses gateway GetPostcodeCoordinates method is called, Then it calls Addresses API context's GetAddressesRequest method With that postcode.")]
        public void AddressesGatewayShouldCallAddressesContextWithGivenPostcode()
        {
            // rubbish arrange
            var irrelevantResponse = Randomm.AddressesAPIContextResponse(200);
            _mockAddressesAPIContext.Setup(c => c.GetAddressesRequest(It.IsAny<string>())).Returns(irrelevantResponse);

            // arrange
            var postcode = Randomm.Postcode();

            // act
            _classUnderTest.GetPostcodeCoordinates(postcode);

            // assert
            _mockAddressesAPIContext.Verify(c => c.GetAddressesRequest(It.Is<string>(p => p == postcode)), Times.Once);
        }

        [TestCase(TestName = "Given Addresses API context's GetAddressesRequest method is called, When successful response With nonempty addresses collection is returned, Then the AddressesGateway returns coordinates of the first address in that collection.")]
        public void Success()
        {
            // arrange
            var expectedCoordinates = Randomm.Coordinates();
            var contextResponse = Randomm.AddressesAPIContextResponse(200, expectedCoordinates);
            _mockAddressesAPIContext.Setup(c => c.GetAddressesRequest(It.IsAny<string>())).Returns(contextResponse);

            // act
            var gatewayResponse = _classUnderTest.GetPostcodeCoordinates(It.IsAny<string>());

            // assert
            gatewayResponse.Should().BeEquivalentTo(expectedCoordinates);

        }

        [TestCase(TestName = "Given Addresses API context's GetAddressesRequest method is called, When successful response With empty addresses collection is returned, Then the AddressesGateway returns null coordinates object.")]
        public void AddressGatewayShouldReturnNullCoordinatesWhenNoAddressesForAGivenPostcodeWereFound()
        {
            // arrange
            var contextResponse = Randomm.AddressesAPIContextResponse(200);
            _mockAddressesAPIContext.Setup(c => c.GetAddressesRequest(It.IsAny<string>())).Returns(contextResponse);

            // act
            var gatewayResponse = _classUnderTest.GetPostcodeCoordinates(It.IsAny<string>());

            // assert
            gatewayResponse.Should().Be(null);
        }

        [TestCase(TestName = "Given Addresses API context's GetAddressesRequest method is called, When bad request response With a list validation failures is returned, Then the AddressesGateway throws BadAPICallException.")]
        public void AddressesGatewatShouldThrowAnErrorWhenAPICallIsConsideredInvalid()
        {
            // arrange
            var contextResponse = Randomm.AddressesAPIContextResponse(400);
            _mockAddressesAPIContext.Setup(c => c.GetAddressesRequest(It.IsAny<string>())).Returns(contextResponse);

            // act
            Action gatewayCall = () => _classUnderTest.GetPostcodeCoordinates(It.IsAny<string>());

            // assert
            gatewayCall.Should().Throw<BadAPICallException>().And.Message.Should().NotBeEmpty();
        }

        [TestCase(TestName = "Given Addresses API context's GetAddressesRequest method is called, When forbidden response is returned, Then the AddressesGateway throws APICallNotAuthorizedException With message telling which API call failed.")]
        public void AddressesGatewayShouldThrowAnErrorWhenAPICallAuthenticationFails() //Forbidden - Gateway should tell that API key is incorrect - TODO: Integration test.
        {
            // arrange
            var statusCode = 403;
            var forbiddenResp = "{\"message\":\"Forbidden\"}";
            var contextResponse = new AddressesAPIContextResponse(statusCode, forbiddenResp);
            _mockAddressesAPIContext.Setup(c => c.GetAddressesRequest(It.IsAny<string>())).Returns(contextResponse);

            // act
            Action gatewayCall = () => _classUnderTest.GetPostcodeCoordinates(It.IsAny<string>());

            // assert
            gatewayCall.Should().Throw<APICallNotAuthorizedException>().WithMessage($"A call to {"Addresses"} API was not authorized.");
        }

        [TestCase(TestName = "Given Addresses API context's GetAddressesRequest method is called, When internal server error response is returned, Then the AddressesGateway throws APICallInternalException.")] // TODO: test exception message
        public void AddressesGatewayShouldThrowAnErrorWhenAPICallReturnInternalFailure()
        {
            // arrange
            var contextResponse = Randomm.AddressesAPIContextResponse(500);
            _mockAddressesAPIContext.Setup(c => c.GetAddressesRequest(It.IsAny<string>())).Returns(contextResponse);

            // act
            Action gatewayCall = () => _classUnderTest.GetPostcodeCoordinates(It.IsAny<string>());

            //assert
            gatewayCall.Should().Throw<APICallInternalException>();
        }

        [TestCase(200, TestName = "Given Addresses API context's GetAddressesRequest method is called, When unexpected schema response is returned With 200, Then the AddressesGateway throws ResponseSchemaNotRecognisedException.")]
        [TestCase(400, TestName = "Given Addresses API context's GetAddressesRequest method is called, When unexpected schema response is returned With 400, Then the AddressesGateway throws ResponseSchemaNotRecognisedException.")]
        [TestCase(500, TestName = "Given Addresses API context's GetAddressesRequest method is called, When unexpected schema response is returned With 500, Then the AddressesGateway throws ResponseSchemaNotRecognisedException.")]
        [TestCase(9000, TestName = "Given Addresses API context's GetAddressesRequest method is called, When unexpected schema response is returned With 400, Then the AddressesGateway throws ResponseSchemaNotRecognisedException.")]
        public void AddressesGatewayShouldThrowAnErrorWhenAPICallReturnsUnexpectedSchema(int statusCode) // if anything about the Addresses API changes, ResponseSchemaNotRecognisedException //TODO: should contain api name
        {
            // arrange
            var contextResponse = Randomm.AddressesAPIContextResponse(statusCode == 9000 ? 400 : statusCode);
            
            switch (statusCode) {
                case 200:
                    contextResponse.JsonContent =
                        contextResponse.JsonContent.Replace("Data", Randomm.Word()
                        , StringComparison.OrdinalIgnoreCase);
                    break;
                case 400:
                    contextResponse.JsonContent =
                        contextResponse.JsonContent.Replace("validationErrors", Randomm.Word()
                        , StringComparison.OrdinalIgnoreCase);
                    break;
                case 500:
                    contextResponse.JsonContent =
                        contextResponse.JsonContent.Replace("Errors", Randomm.Word()
                        , StringComparison.OrdinalIgnoreCase);
                    break;
                default:
                    contextResponse.JsonContent =
                        contextResponse.JsonContent.Replace("error", Randomm.Word()
                        , StringComparison.OrdinalIgnoreCase);
                    break;
            }

            _mockAddressesAPIContext.Setup(c => c.GetAddressesRequest(It.IsAny<string>())).Returns(contextResponse);

            // act
            Action gatewayCall = () => _classUnderTest.GetPostcodeCoordinates(It.IsAny<string>());

            // assert
            gatewayCall.Should().Throw<ResponseSchemaNotRecognisedException>();
        }

        [TestCase(TestName = "Given Addresses API context's GetAddressesRequest method is called, When an unexpected exception is thrown during its execution, Then the AddressesGateway rethrows that exception so it could be handled elsewhere.")] // it should be handled where all the other exceptions are handled. Gateway can't be the one handling it because as requested it only has to return the coordinates. TODO: improve on this.
        public void GivenAnUnexpectedExceptionIsThrownInTheGatewayThenItShouldRethrowThatException() // Like WebException (Timeout, Connect failure)
        {
            // arrange
            var expectedException = new WebException(Randomm.Word(), WebExceptionStatus.Timeout);
            _mockAddressesAPIContext.Setup(c => c.GetAddressesRequest(It.IsAny<string>()))
                .Throws(expectedException);

            // act
            Action gatewayCall = () => _classUnderTest.GetPostcodeCoordinates(It.IsAny<string>());

            // assert
            gatewayCall.Should().Throw<WebException>();
        }

        // TODO: Add NotFound - API could potentially be moved.
        // TODO: Add Other status code.
    }
}
