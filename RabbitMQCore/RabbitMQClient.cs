using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQCore.Configuration;

namespace RabbitMQCore
{
    public interface IRabbitMQClient
    {
        void SendMessage(string message);
        void ReceiveMessages(Action<string> receiveAction);
    }

    public class RabbitMQClient : IRabbitMQClient
    {
        private readonly IRabbitMQConfiguration _configuration;

        public RabbitMQClient(IRabbitMQConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendMessage(string message)
        {
            //var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = _configuration.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: _configuration.ExchangeName, type: "fanout");

                //var message = GetMessage(args);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: _configuration.ExchangeName,
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }

        public void ReceiveMessages(Action<string> receiveAction)
        {
            {
                using (var connection = _configuration.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: _configuration.ExchangeName, type: "fanout");

                    var queueName = channel.QueueDeclare().QueueName;
                    channel.QueueBind(queue: queueName,
                                      exchange: _configuration.ExchangeName,
                                      routingKey: "");

                    Console.WriteLine(" [*] Waiting for logs.");

                    var consumer = new EventingBasicConsumer(channel);
                    
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                       // Console.WriteLine(" [x] {0}", message);
                        receiveAction(message);
                    };
                    
                    channel.BasicConsume(queue: queueName,
                                         noAck: false,
                                         consumer: consumer);
                    Console.ReadLine();
                }
            }
        }
    }
}
