using System.Collections.Generic;
using LBHFSSPublicAPI.V1.Controllers;
using LBHFSSPublicAPI.V1.UseCase;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Controllers
{

    [TestFixture]
    public class TaxonomiesControllerTests
    {
        private TaxonomiesController _classUnderTest;


        [SetUp]
        public void SetUp()
        {
            _classUnderTest = new TaxonomiesController();
        }

        [Test]
        public void ReturnsResponseWithStatus()
        {
            var expected = new Dictionary<string, object> { { "success", true } };
            var response = _classUnderTest.GetTaxonomies() as OkObjectResult;
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(200);
            response.Value.Should().BeEquivalentTo(expected);
        }

    }
}
