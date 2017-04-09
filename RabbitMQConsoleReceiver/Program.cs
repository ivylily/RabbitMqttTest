using System;
using RabbitMQ.Client;
using RabbitMQCore;
using RabbitMQCore.Configuration;

namespace RabbitMQConsoleReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            IRabbitMQClient client = new RabbitMQClient(new UriRabbitMqConfiguration("GPSLocation",
                "amqp://kkxglvxw:WhGhDnh9_wSe-Jf_TTjwevZkG9A9bZhv@clam.rmq.cloudamqp.com/kkxglvxw")
                );
            client.ReceiveMessages(x => Console.WriteLine(x));
            Console.ReadKey();
        }
    }
}
