namespace LBHFSSPublicAPI.V1.Infrastructure
{
    public class AddressesAPIConnectionOptions
    {
        public string ApiBaseUrl { get; set; }
        public string ApiKey { get; set; }
        public string ApiToken { get; set; }

        public AddressesAPIConnectionOptions(string apiBaseUrl, string apiKey, string apiToken)
        {
            ApiBaseUrl = apiBaseUrl;
            ApiKey = apiKey;
            ApiToken = apiToken;
        }
    }
}
