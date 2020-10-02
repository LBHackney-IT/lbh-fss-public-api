using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using LBHFSSPublicAPI.V1.Helpers;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Helper
{
    [TestFixture]
    public class UrlHelperTest
    {
        [TestCase(TestName = "Given a url encoded string a decoded string is returned")]
        public void UrlEncodedStringGetsDecoded()
        {
            var searchTerm = Randomm.Text();
            var urlencodedSearch = searchTerm.Replace(" ", "%20");
            var decodedParams = UrlHelper.DecodeParams(urlencodedSearch);
            decodedParams.Should().Be(searchTerm);
        }

        [TestCase(TestName = "Given a double url encoded string a decoded string is returned")]
        public void DoubleUrlEncodedStringGetsDecoded()
        {
            var searchTerm = Randomm.Text();
            var urlencodedSearch = searchTerm.Replace(" ", "%2520");
            var decodedParams = UrlHelper.DecodeParams(urlencodedSearch);
            decodedParams.Should().Be(searchTerm);
        }
    }
}
