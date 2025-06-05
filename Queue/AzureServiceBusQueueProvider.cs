using Azure.Messaging.ServiceBus;
using IGlassAPI.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace IGlassAPI.Queue
{
    public class AzureServiceBusQueueProvider : IQueueProvider
    {
        private readonly ServiceBusSender _sender;
        private readonly ILogger<PayloadController> _logger;


        public AzureServiceBusQueueProvider(IConfiguration config, ILogger<PayloadController> logger)
        {
            try
            {
                var client = new ServiceBusClient(config["Azure:ConnectionString"]);
                _sender = client.CreateSender(config["Azure:QueueName"]);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Failed to AzureServiceBusQueueProvider Load.");
            }
        }

        public async Task EnqueueAsync(string payload)
        {
            await _sender.SendMessageAsync(new ServiceBusMessage(payload));
        }
    }
}