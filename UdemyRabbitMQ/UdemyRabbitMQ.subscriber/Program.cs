using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace UdemyRabbitMQ.subscriber // consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var uri = Environment.GetEnvironmentVariable("URI", EnvironmentVariableTarget.Process);
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(uri);
            using var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.ExchangeDeclare("headers-exchange", durable: true, type: ExchangeType.Headers);
            channel.BasicQos(0, 1, false);
            var consumer = new EventingBasicConsumer(channel);

            // Kuyruk oluşturma
            var queueName = channel.QueueDeclare().QueueName;
            
            Dictionary<string,object> headers = new Dictionary<string, object>();
            headers.Add("format", "pdf");
            headers.Add("shape", "a4");
            headers.Add("x-match", "all");

            channel.QueueBind(queueName, "headers-exchange", String.Empty, headers);
            channel.BasicConsume(queueName, false, consumer);
            Console.WriteLine("Loglar Dinleniyor...");

            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Thread.Sleep(500);
                Console.WriteLine("Kuyruktan Gelen Mesaj: " + message);

                channel.BasicAck(e.DeliveryTag, false); // mesajı artık kuyruktan silebilirsin diye belirttik (yukarıyı false yaptığımız için)
            };

            Console.ReadLine();
        }

    }
}
