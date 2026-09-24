using System.Collections.Generic;
using Amazon.Lambda.APIGatewayEvents;

namespace LBHFSSPublicAPI.Tests.TestHelpers
{
    public static class ApiGatewayRequestFactory
    {
        public static APIGatewayProxyRequest Get(string path, IDictionary<string, string> queryStringParameters = null)
        {
            var multiValueQuery = queryStringParameters == null
                ? null
                : new Dictionary<string, IList<string>>();

            if (queryStringParameters != null)
            {
                foreach (var pair in queryStringParameters)
                {
                    multiValueQuery[pair.Key] = new List<string> { pair.Value };
                }
            }

            return new APIGatewayProxyRequest
            {
                HttpMethod = "GET",
                Path = path,
                Resource = path,
                QueryStringParameters = queryStringParameters == null
                    ? null
                    : new Dictionary<string, string>(queryStringParameters),
                MultiValueQueryStringParameters = multiValueQuery,
                Headers = new Dictionary<string, string>
                {
                    ["Host"] = "localhost",
                    ["Accept"] = "*/*"
                },
                MultiValueHeaders = new Dictionary<string, IList<string>>
                {
                    ["Host"] = new List<string> { "localhost" },
                    ["Accept"] = new List<string> { "*/*" }
                },
                RequestContext = new APIGatewayProxyRequest.ProxyRequestContext
                {
                    RequestId = "test-request",
                    Stage = "test",
                    HttpMethod = "GET",
                    Path = path,
                    ResourcePath = path,
                    Identity = new APIGatewayProxyRequest.RequestIdentity
                    {
                        SourceIp = "127.0.0.1"
                    }
                }
            };
        }
    }
}
