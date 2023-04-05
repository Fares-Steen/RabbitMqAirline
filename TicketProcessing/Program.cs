// See https://aka.ms/new-console-template for more information

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("Hello, World!");
var factory = new ConnectionFactory
{
    HostName = "localhost",
    UserName = "user",
    Password = "pass",
    VirtualHost = "/"
};

IConnection? conn = factory.CreateConnection();

using IModel? channel = conn.CreateModel();

channel.QueueDeclare("bookings2", true, false);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, basicDeliverEventArgs) =>
{
   var bytes = basicDeliverEventArgs.Body.ToArray();
   var message = Encoding.UTF8.GetString(bytes);
   Console.WriteLine(message);
};

channel.BasicConsume("bookings2",true,consumer);
Console.ReadLine();