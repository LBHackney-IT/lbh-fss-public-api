using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using LBHFSSPublicAPI.V1.Factories;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Factories
{
    /// <summary>
    /// Tests for image URLs when FSS_PUBLIC_API_BASE_URL enables proxy paths vs legacy Url split.
    /// </summary>
    [TestFixture]
    public class ServiceFactoryImageUrlTests
    {
        private const string EnvKey = "FSS_PUBLIC_API_BASE_URL";
        private string _previousBaseUrl;

        [SetUp]
        public void SaveEnv()
        {
            _previousBaseUrl = System.Environment.GetEnvironmentVariable(EnvKey);
        }

        [TearDown]
        public void RestoreEnv()
        {
            if (_previousBaseUrl == null)
                System.Environment.SetEnvironmentVariable(EnvKey, null);
            else
                System.Environment.SetEnvironmentVariable(EnvKey, _previousBaseUrl);
        }

        [Test]
        public void ToResponseService_WhenPublicApiBaseUrlSet_UsesProxyImagePaths()
        {
            System.Environment.SetEnvironmentVariable(EnvKey, "https://api.example.com/v1");

            var domain = EntityHelpers.CreateService().ToDomain();
            domain.Id = 42;
            domain.Image = EntityHelpers.CreateFile();
            domain.Image.Url = "should-not-be-used-when-proxy-set";

            var response = domain.ToResponseService();

            response.Images.Medium.Should().Be("https://api.example.com/v1/images/42/medium");
            response.Images.Original.Should().Be("https://api.example.com/v1/images/42/original");
        }

        [Test]
        public void ToResponseService_WhenPublicApiBaseUrlHasTrailingSlash_TrimsSlashBeforeBuildingUrls()
        {
            System.Environment.SetEnvironmentVariable(EnvKey, "https://api.example.com/v1/");

            var domain = EntityHelpers.CreateService().ToDomain();
            domain.Id = 5;
            domain.Image = EntityHelpers.CreateFile();

            var response = domain.ToResponseService();

            response.Images.Medium.Should().Be("https://api.example.com/v1/images/5/medium");
            response.Images.Original.Should().Be("https://api.example.com/v1/images/5/original");
        }

        [Test]
        public void ToResponseService_WhenPublicApiBaseUrlUnset_UsesSemicolonSplitUrls()
        {
            System.Environment.SetEnvironmentVariable(EnvKey, null);

            var domain = EntityHelpers.CreateService().ToDomain();
            domain.Id = 100;
            domain.Image = EntityHelpers.CreateFile();
            domain.Image.Url = "https://orig.example/o.jpg;https://med.example/m.jpg";

            var response = domain.ToResponseService();

            response.Images.Original.Should().Be("https://orig.example/o.jpg");
            response.Images.Medium.Should().Be("https://med.example/m.jpg");
        }
    }
}
