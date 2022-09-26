using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UdemyRabbitMQ.publisher
{
    public enum LogNames
    {
        Critical=1,
        Error=2,
        Warning=3,
        Info=4,
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var uri = Environment.GetEnvironmentVariable("URI", EnvironmentVariableTarget.Process);
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(uri);
            using var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.ExchangeDeclare("headers-exchange", durable:true, type:ExchangeType.Headers);
            
            Dictionary<string,object> headers = new Dictionary<string, object>();
            headers.Add("format", "pdf");
            headers.Add("shape", "a4");

            var properties = channel.CreateBasicProperties();
            properties.Headers = headers;

            channel.BasicPublish("headers-exchange",
                                 String.Empty, 
                                 properties,
                                 Encoding.UTF8.GetBytes("header mesajı"));
            
            Console.WriteLine("Mesaj Gönderilmiştir");
            Console.ReadLine();
        }
    }
}
