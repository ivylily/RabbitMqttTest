using RabbitMQ.Client;

namespace RabbitMQCore.Configuration
{
    public class UriRabbitMqConfiguration : IRabbitMQConfiguration
    {
        public readonly ConnectionFactory ConnectionFactory;
        public UriRabbitMqConfiguration(string exchangeName, string uri)
        {
            ConnectionFactory = new ConnectionFactory() { Uri = uri };
            ExchangeName = exchangeName;
            Uri = uri;
        }
        public string Uri { get; private set; }
        public string ExchangeName { get; }
        public IConnection CreateConnection()
        {
            return ConnectionFactory.CreateConnection();
        }
    }
}