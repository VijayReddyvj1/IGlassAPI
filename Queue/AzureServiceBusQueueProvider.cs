using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace IGlassAPI.Queue
{
    public class AzureServiceBusQueueProvider : IQueueProvider
    {
        private readonly ServiceBusSender _sender;

        public AzureServiceBusQueueProvider(IConfiguration config)
        {
            var client = new ServiceBusClient(config["Azure:ConnectionString"]);
            _sender = client.CreateSender(config["Azure:QueueName"]);
        }

        public async Task EnqueueAsync(string payload , string clientID )
        {
            await _sender.SendMessageAsync(new ServiceBusMessage(payload));
        }
    }
}