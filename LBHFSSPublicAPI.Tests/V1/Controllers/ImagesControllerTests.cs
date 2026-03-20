using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using FluentAssertions;
using LBHFSSPublicAPI.V1.Controllers;
using LBHFSSPublicAPI.V1.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Controllers
{
    [TestFixture]
    public class ImagesControllerTests
    {
        private Mock<IAmazonS3> _s3;
        private ImagesController _controller;

        [SetUp]
        public void SetUp()
        {
            _s3 = new Mock<IAmazonS3>();
            var options = new ImageStoreOptions("fss-imagestore-test");
            _controller = new ImagesController(_s3.Object, options, NullLogger<ImagesController>.Instance)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };
        }

        [Test]
        public async Task GetImage_ValidMedium_ReturnsJpegFile()
        {
            var stream = new MemoryStream(new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 });
            _s3.Setup(x => x.GetObjectAsync(It.IsAny<GetObjectRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetObjectResponse { ResponseStream = stream });

            var result = await _controller.GetImage(99, "medium") as FileStreamResult;

            result.Should().NotBeNull();
            result!.ContentType.Should().Be("image/jpeg");
            result.FileStream.Should().NotBeNull();
            _s3.Verify(x => x.GetObjectAsync(
                It.Is<GetObjectRequest>(r => r.BucketName == "fss-imagestore-test" && r.Key == "images/99-medium.jpg"),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task GetImage_ValidOriginal_UsesOriginalKey()
        {
            var stream = new MemoryStream(new byte[] { 1, 2, 3 });
            _s3.Setup(x => x.GetObjectAsync(It.IsAny<GetObjectRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetObjectResponse { ResponseStream = stream });

            var result = await _controller.GetImage(7, "original");

            result.Should().BeOfType<FileStreamResult>();
            _s3.Verify(x => x.GetObjectAsync(
                It.Is<GetObjectRequest>(r => r.Key == "images/7-original.jpg"),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task GetImage_SizeIsCaseInsensitive()
        {
            var stream = new MemoryStream();
            _s3.Setup(x => x.GetObjectAsync(It.IsAny<GetObjectRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetObjectResponse { ResponseStream = stream });

            await _controller.GetImage(1, "MEDIUM");

            _s3.Verify(x => x.GetObjectAsync(
                It.Is<GetObjectRequest>(r => r.Key == "images/1-medium.jpg"),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task GetImage_InvalidSize_ReturnsBadRequest()
        {
            var result = await _controller.GetImage(1, "large");

            result.Should().BeOfType<BadRequestObjectResult>();
            _s3.Verify(x => x.GetObjectAsync(It.IsAny<GetObjectRequest>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Test]
        public async Task GetImage_BucketNotConfigured_Returns500()
        {
            var controller = new ImagesController(
                _s3.Object,
                new ImageStoreOptions(string.Empty),
                NullLogger<ImagesController>.Instance)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };

            var result = await controller.GetImage(1, "medium");

            var status = result as ObjectResult;
            status.Should().NotBeNull();
            status!.StatusCode.Should().Be(500);
            _s3.Verify(x => x.GetObjectAsync(It.IsAny<GetObjectRequest>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Test]
        public async Task GetImage_S3ObjectNotFound_ReturnsNotFound()
        {
            _s3.Setup(x => x.GetObjectAsync(It.IsAny<GetObjectRequest>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new AmazonS3Exception("NoSuchKey") { StatusCode = HttpStatusCode.NotFound });

            var result = await _controller.GetImage(404, "medium");

            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task GetImage_OtherS3Error_Returns502()
        {
            _s3.Setup(x => x.GetObjectAsync(It.IsAny<GetObjectRequest>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new AmazonS3Exception("AccessDenied") { StatusCode = HttpStatusCode.Forbidden });

            var result = await _controller.GetImage(1, "medium");

            var status = result as ObjectResult;
            status.Should().NotBeNull();
            status!.StatusCode.Should().Be(502);
        }
    }
}
