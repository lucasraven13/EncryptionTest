using System;
using System.Threading;
using System.Threading.Tasks;
using APIGateway.Services.Implementations;
using Microsoft.Extensions.Hosting;

namespace APIGateway.Utils
{
    internal class KeyRotationHostedService : IHostedService, IDisposable
    {
        private readonly IApiClient _apiClient;
        private Timer _timer;

        public KeyRotationHostedService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(600));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            await _apiClient.Client.PutAsync("key", null);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}