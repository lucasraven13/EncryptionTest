using System;
using System.Net.Http;
using APIGateway.Settings;

namespace APIGateway.Services.Implementations
{
    public interface IApiClient
    {
        HttpClient Client { get; }
    }

    public class ApiClient : IApiClient
    {
        public HttpClient Client { get; }

        public ApiClient(HttpClient client, ApiEncryptionServiceSettings apiEncryptionServiceSettings)
        {
            client.BaseAddress = new Uri(apiEncryptionServiceSettings.Host);
            client.DefaultRequestHeaders.Add("Accept",
                "application/json");

            Client = client;
        }
    }
}