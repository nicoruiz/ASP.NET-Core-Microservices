using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServices.RabbitMQ.Bus.EventQueue;
using TiendaServices.RabbitMQ.Bus.RabbitBus;

namespace TiendaServices.Api.Autor.RabbitHandler
{
    public class EmailEventHandler : IEventHandler<EmailEventQueue>
    {
        public EmailEventHandler() { }

        public Task Handle(EmailEventQueue @event)
        {
            Console.WriteLine($"Event consumed from RabbitMQ {@event.Title}");

            return Task.CompletedTask;
        }
    }
}
