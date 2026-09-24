using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Lambda.TestUtilities;
using Amazon.S3;
using Amazon.S3.Model;
using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using LBHFSSPublicAPI.V1.Boundary.Response;
using LBHFSSPublicAPI.V1.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Lambda
{
    /// <summary>
    /// Covers AspNetCoreServer upgrade risks that ordinary WebApplicationFactory tests do not:
    /// query-string mapping (6.x double-encode fix) and binary image base64 encoding.
    /// </summary>
    [TestFixture]
    [NonParallelizable]
    public class AspNetCoreServerAdapterTests
    {
        private DatabaseContext _db;
        private Mock<IAmazonS3> _s3;

        [SetUp]
        public void SetUp()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var options = new DbContextOptionsBuilder()
                .UseNpgsql(ConnectionString.TestDatabase())
                .Options;
            _db = new DatabaseContext(options);
            _db.Database.EnsureCreated();
            DbCleardown.ClearAll(_db);
            _s3 = new Mock<IAmazonS3>();
        }

        [TearDown]
        public void TearDown()
        {
            TestAppStartup.ConfigureTestServices = null;
            DbCleardown.ClearAll(_db);
            _db.Dispose();
        }

        [Test]
        public async Task Search_WithSpacedQueryParam_DoesNotDoubleEncode()
        {
            var searchTerm = $"alpha beta-{Guid.NewGuid():N}".Substring(0, 24);
            var service = EntityHelpers.CreateService();
            service.Name = $"Service {searchTerm}";
            _db.Services.Add(service);
            await _db.SaveChangesAsync().ConfigureAwait(true);

            var entryPoint = new TestableLambdaEntryPoint();
            var request = ApiGatewayRequestFactory.Get(
                "/api/v1/services",
                new Dictionary<string, string> { ["search"] = searchTerm });

            var response = await entryPoint.FunctionHandlerAsync(request, new TestLambdaContext())
                .ConfigureAwait(true);

            response.StatusCode.Should().Be(200);
            var body = JsonConvert.DeserializeObject<GetServiceResponseList>(response.Body);
            body.Services.Should().Contain(s => s.Id == service.Id);
        }

        [Test]
        public async Task GetImage_ReturnsBase64EncodedJpeg()
        {
            var jpegBytes = new byte[] { 0xFF, 0xD8, 0xFF, 0xE0, 0x00, 0x10 };
            var service = EntityHelpers.CreateService();
            service.Image.Url = "https://assets.example.com/images/99-original.jpg;https://assets.example.com/images/99-medium.jpg";
            _db.Services.Add(service);
            await _db.SaveChangesAsync().ConfigureAwait(true);

            _s3.Setup(x => x.GetObjectAsync(It.IsAny<GetObjectRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetObjectResponse { ResponseStream = new MemoryStream(jpegBytes) });

            var entryPoint = new TestableLambdaEntryPoint(services =>
            {
                services.ReplaceSingleton<IAmazonS3>(_s3.Object);
                services.ReplaceSingleton(new ImageStoreOptions("fss-imagestore-test"));
            });

            var response = await entryPoint.FunctionHandlerAsync(
                    ApiGatewayRequestFactory.Get($"/api/v1/images/{service.Id}/medium"),
                    new TestLambdaContext())
                .ConfigureAwait(true);

            response.StatusCode.Should().Be(200);
            response.IsBase64Encoded.Should().BeTrue();
            Convert.FromBase64String(response.Body).Should().Equal(jpegBytes);
        }
    }
}
