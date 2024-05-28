using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory
{
    Uri = new Uri("amqps://b-6d344f1d-1f71-4e0a-8b46-4658ca551769.mq.us-east-1.amazonaws.com:5671"),
    Port = 5671,
    UserName = "adminarpit",
    Password = "Arpitadmin1@"
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "firstQueue",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null);


string? message = null;
do
{
    Console.WriteLine("Enter Message. Press [enter] to exit.");
    message = Console.ReadLine();
    if (!string.IsNullOrEmpty(message))
        SendMessage(message, channel);
} while (!string.IsNullOrEmpty(message));

void SendMessage(string s, IModel channel)
{
    var body = Encoding.UTF8.GetBytes(s);

    channel.BasicPublish(exchange: string.Empty,
        routingKey: "firstQueue",
        basicProperties: null,
        body: body);
    Console.WriteLine($" [x] Sent {s}");
}
