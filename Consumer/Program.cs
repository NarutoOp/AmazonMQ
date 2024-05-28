using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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


Console.WriteLine(" [*] Waiting for messages.");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Received {message}");
};
channel.BasicConsume(queue: "firstQueue",
    autoAck: true,
    consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
