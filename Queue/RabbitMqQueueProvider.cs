using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using IGlassAPI.Controllers;

namespace IGlassAPI.Queue
{
    public class RabbitMqQueueProvider : IQueueProvider
    {
        private readonly string _queueName;
        private readonly IModel _channel;
        private readonly ILogger<PayloadController> _logger;



        public RabbitMqQueueProvider(IConfiguration config, ILogger<PayloadController> logger)
        {
            try
            {
                _queueName = config["RabbitMQ:QueueName"];
                var factory = new ConnectionFactory
                {
                    HostName = config["RabbitMQ:HostName"],
                    UserName = config["RabbitMQ:UserName"],
                    Password = config["RabbitMQ:Password"]
                };
                var connection = factory.CreateConnection();
                _channel = connection.CreateModel();
                _channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false);
            }
            catch(Exception ex)
            {
                logger.LogError(ex,"Failed to RabbitMqQueueProvider Load.");
            }
        }

        public Task EnqueueAsync(string payload,string clientID)
        {
            var body = Encoding.UTF8.GetBytes(payload);
            _channel.BasicPublish("", _queueName, null, body);
            return Task.CompletedTask;
        }
    }
}