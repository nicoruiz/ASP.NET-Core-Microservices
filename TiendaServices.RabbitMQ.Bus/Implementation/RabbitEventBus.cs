using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TiendaServices.RabbitMQ.Bus.Events;
using TiendaServices.RabbitMQ.Bus.RabbitBus;

namespace TiendaServices.RabbitMQ.Bus.Implement
{
    public class RabbitEventBus : IRabbitEventBus
    {       
        public void Publish<T>(T rabbitEvent) where T : Event
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq-web" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var eventName = rabbitEvent.GetType().Name;

                channel.QueueDeclare(eventName, false, false, false, null);

                var message = JsonConvert.SerializeObject(rabbitEvent);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("", eventName, null, body);
            }
        }

        public void Consume<T>() where T : Event
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq-web" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var eventName = typeof(T).Name;
            channel.QueueDeclare(eventName, false, false, false, null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var deserializedEvemt = JsonConvert.DeserializeObject<T>(message);
                deserializedEvemt.Handle();
            };

            channel.BasicConsume(eventName, true, consumer);
        }
    }
}
