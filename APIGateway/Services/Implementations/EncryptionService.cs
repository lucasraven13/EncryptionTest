using System;
using System.Threading.Tasks;
using APIGateway.Models;
using APIGateway.Services.Interfaces;
using System.Net.Http;
using Newtonsoft.Json;

namespace APIGateway.Services.Implementations
{
    public class EncryptionService : IEncryptionService
    {
        private readonly IApiClient _apiClient;
        public EncryptionService(IApiClient client)
        {
            _apiClient = client;
        }
        public async Task<string> DecryptAsync(DecryptionModel model)
        {
            try
            {
                var response = await _apiClient.Client.PostAsJsonAsync("decrypt", model);
                var result = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<string>(result);
                }
                else
                {
                    throw new ApiException(JsonConvert.DeserializeObject<ApiErrorResult>(result));
                }
            }
            catch (Exception)
            {
                throw new ApiException("Encryption service is unavailable");
            }
        }

        public async Task<string> EncryptAsync(EncryptionModel model)
        {
            try
            {
                var response = await _apiClient.Client.PostAsJsonAsync("encrypt", model);
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<string>(result);
            }
            catch (Exception)
            {
                throw new ApiException("Encryption service is unavailable");
            }
        }
    }
}