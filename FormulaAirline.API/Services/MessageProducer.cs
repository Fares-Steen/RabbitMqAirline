using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace FormulaAirline.API.Services
{
    public class MessageProducer:IMessageProducer
    {
        public void SendingMessage<T>(T message)
        {
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

            string json = JsonSerializer.Serialize(message);

            byte[] body = Encoding.UTF8.GetBytes(json);
            
            channel.BasicPublish("","bookings2", body:body);
        }
    }
}
