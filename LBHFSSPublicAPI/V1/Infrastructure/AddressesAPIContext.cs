using System;
using System.Net.Http;

namespace LBHFSSPublicAPI.V1.Infrastructure
{
    public class AddressesAPIContext : IAddressesAPIContext
    {
        private readonly string _apiBaseUrl;
        private readonly string _apiKey;
        private readonly HttpClient _httpClient = new HttpClient();

        public AddressesAPIContext(AddressesAPIConnectionOptions options)
        {
            _apiBaseUrl = options.ApiBaseUrl;
            _apiKey = options.ApiKey;
        }

        public AddressesAPIContextResponse GetAddressesRequest(string postcode) // asc Block for now.
        {
            // Build the request
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            var fullUrlString = $"{_apiBaseUrl}/addresses?PostCode={postcode}&Gazetteer=Both&Format=Detailed"; //Gazeteer Both? or Local?
            request.RequestUri = new Uri(fullUrlString);
            request.Headers.Add("x-api-key", _apiKey);

            // Make the request
            var response = _httpClient.SendAsync(request).Result;

            // Structure Response
            var statusCode = (int) response.StatusCode;
            var responseJson = response.Content.ReadAsStringAsync().Result;
            var contextResponse = new AddressesAPIContextResponse(statusCode, responseJson);

            // Cleanup & Return
            request.Dispose();
            response.Dispose();
            return contextResponse;
        }
    }
}
