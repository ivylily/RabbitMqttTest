using RabbitMQ.Client;

namespace RabbitMQCore.Configuration
{

    public interface IRabbitMQConfiguration
    {
        string Uri { get; }
        string ExchangeName { get; }
        IConnection CreateConnection();
    }

    public class UserRabbitMQConfiguration : IRabbitMQConfiguration
    {
        public readonly ConnectionFactory ConnectionFactory;
        public readonly string HostName;
        public readonly string User;
        public readonly string Password;
        public readonly string Port;

        public UserRabbitMQConfiguration(ConnectionFactory connectionFactory, string exchangeName)
        {
            ConnectionFactory = connectionFactory;
            ExchangeName = exchangeName;
            HostName = "172.19.0.156";
            User = "guest";
            Password = "guest";
            Port = "5672";
        }

        public UserRabbitMQConfiguration(ConnectionFactory connectionFactory, string exchangeName, string hostName, string user, string password, string port)
        {
            ConnectionFactory = connectionFactory;
            ExchangeName = exchangeName;
            HostName = hostName;
            User = user;
            Password = password;
            Port = port;
        }


        public string Uri
        {
            get
            {
                return string.Format("amqp://{0}:{1}@{2}:{3}", User, Password, HostName, Port);
            }
        }

        public string ExchangeName { get; }
        public IConnection CreateConnection()
        {
            return ConnectionFactory.CreateConnection(Uri);
        }
    }
}
