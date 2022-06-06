using Microsoft.Extensions.Hosting;
using System.Threading;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using RabbitMQ.Client;
using System.Text;

namespace Producer
{
    public class RMQBackgroundService: IHostedService
    {
        private Timer _timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(PublishMessage, null, 0, 10000);
            return Task.CompletedTask;
        }

        public void PublishMessage(object state)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "task_queue",
                                    durable: true,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                var message = GetMessage();
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "",
                                    routingKey: "task_queue",
                                    basicProperties: properties,
                                    body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }

        public string GetMessage()
        {
            return "Hey! I am up and running at " + DateTime.Now.ToString();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}