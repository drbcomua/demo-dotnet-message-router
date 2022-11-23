using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Send
{
    static class Receiver
    {
        public static void Main()
        {
            var hostName = Environment.GetEnvironmentVariable("DR_HOSTNAME");
            var userName = Environment.GetEnvironmentVariable("DR_USERNAME");
            var password = Environment.GetEnvironmentVariable("DR_PASSWORD");
            var inQueue = Environment.GetEnvironmentVariable("DR_QUEUE_IN");

            var factory = new ConnectionFactory() { HostName = hostName, UserName = userName, Password = password };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: inQueue,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            Message? message;
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var messageJson = Encoding.UTF8.GetString(body);
                message = JsonConvert.DeserializeObject<Message>(messageJson);
                Console.WriteLine(" [x] Received {0}", messageJson);

                if (message is not null)
                {
                    Enricher.Enrich(ref message);
                    Sender.Send(message);
                }
            };
            channel.BasicConsume(queue: inQueue,
                                 autoAck: true,
                                 consumer: consumer);
            Console.WriteLine(" Press [enter] to exit from Message Router.");
            Console.ReadLine();
        }
    }
}