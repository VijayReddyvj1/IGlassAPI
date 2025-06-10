
using IGlassAPI.Queue;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RefitQueueWorkerHybrid.RefitClients;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RefitQueueWorkerHybrid.Worker
{
    public class QueueWorker : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<QueueWorker> _logger;

        public QueueWorker(IServiceProvider services, ILogger<QueueWorker> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (InMemoryQueueProvider.Queue.TryDequeue(out var item))
                {
                    var (json, clientId) = item;
                    using var scope = _services.CreateScope();
                    var apiClient = scope.ServiceProvider.GetRequiredService<IApiClient>();

                    try
                    {
                        await apiClient.PostPayloadAsync(new { clientId, payload = json });
                        _logger.LogInformation("Processed payload for client {clientId}", clientId);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to call external API.");
                    }
                }

                await Task.Delay(1000);
            }
        }
    }
}
