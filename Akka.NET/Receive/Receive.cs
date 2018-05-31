using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Diagnostics;
using System.Text;

namespace Receive
{
    class Receive
    {
        public static void Main()
        {
            Stopwatch sw = new Stopwatch();
            bool isStarted = false;
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    if (!isStarted)
                    {
                        sw.Start();
                        isStarted = true;
                    }
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("Received {0}", message);
                    if (message.Equals("499"))
                    {
                        sw.Stop();
                        Console.WriteLine("Time taken: {0}", sw.ElapsedMilliseconds);
                    }
                };
                
                channel.BasicConsume(queue: "hello",
                                     autoAck: true,
                                     consumer: consumer);

                Console.ReadLine();
            }
        }
    }
}
