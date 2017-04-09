using RabbitMQ.Client;

namespace RabbitMQCore.Configuration
{
    public class DefaultUserRabbitMqConfiguration : UserRabbitMQConfiguration
    {
        public DefaultUserRabbitMqConfiguration() 
            : base(new ConnectionFactory() { HostName = "localhost" }, "lilychat")
        {

        }
    }
}