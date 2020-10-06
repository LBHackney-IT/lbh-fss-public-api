namespace LBHFSSPublicAPI.V1.Infrastructure
{
    public class AddressesAPIConnectionOptions
    {
        public string ApiBaseUrl { get; set; }
        public string ApiKey { get; set; }

        public AddressesAPIConnectionOptions(string apiBaseUrl, string apiKey)
        {
            ApiBaseUrl = apiBaseUrl;
            ApiKey = apiKey;
        }
    }
}
